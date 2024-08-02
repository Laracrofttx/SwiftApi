using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using SwiftApi.Core.Services;
using SwiftApi.Data.GlobalConstants;
using SwiftApi.Data.Models;
using System.Transactions;

namespace SwiftApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FileUploadController : Controller
	{
		private readonly MessageService messageService;
		private readonly ILogger<FileUploadController> logger;

		public FileUploadController(MessageService messageService, ILogger<FileUploadController> logger)
		{
			this.messageService = messageService;
			this.logger = logger;
		}

		//<summary>
		// Reveives file with a Swift message, read it, parse it and save it in the database
		//</summary>

		[HttpPost("upload")]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			logger.LogInformation(GlobalConstants.ImportProcessStarted);
			using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

			try
			{
				using var reader = new StreamReader(file.OpenReadStream());
				var content = await reader.ReadToEndAsync();

				logger.LogInformation(GlobalConstants.ParseProcessStarted);
				var swiftDataResponse = await this.messageService.PargeSwiftMessageAsync(content);

				if (!swiftDataResponse.IsSuccessful)
				{
					transactionScope.Dispose();
					return BadRequest(swiftDataResponse.Message);
				}

				logger.LogInformation(GlobalConstants.InsertDataProcessStarted);
				var insertionResponse = await this.messageService.PargeSwiftMessageAsync(swiftDataResponse.Message);

				if (!insertionResponse.IsSuccessful)
				{
					transactionScope.Dispose();
					return BadRequest(insertionResponse.Message);
				}

				transactionScope.Complete();
				return Ok(GlobalConstants.SuccessfulDataInsertion);
			}

			catch (ArgumentNullException ex)
			{
				logger.LogInformation(ex.Message);
				transactionScope.Dispose();
				return BadRequest(new ResponseMessage<string> { IsSuccessful = false, Message = GlobalConstants.ArgumentNullMessage });
			}
			catch (IOException ex)
			{
				logger.LogInformation(ex.Message);
				transactionScope.Dispose();
				return BadRequest(new ResponseMessage<string> { IsSuccessful = false, Message = GlobalConstants.IOExceptionMessage });
			}
			catch (OutOfMemoryException ex)
			{
				logger.LogInformation(ex.Message);
				transactionScope.Dispose();
				return BadRequest(new ResponseMessage<string> { IsSuccessful = false, Message = GlobalConstants.OutOfMemoryMessage });
			}
			catch (SqliteException ex)
			{
				logger.LogInformation(ex.Message);
				transactionScope.Dispose();
				return BadRequest(new ResponseMessage<string> { IsSuccessful = false, Message = ex.Message });
			}
			catch (Exception ex)
			{
				logger.LogInformation(ex.Message);
				transactionScope.Dispose();
				return BadRequest(new ResponseMessage<string> { IsSuccessful = false, Message = ex.Message });
			}



		}
	}
}
