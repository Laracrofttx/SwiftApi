using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using SwiftApi.Core.Services;
using SwiftApi.Core.Services.Interfaces;
using SwiftApi.Data.GlobalConstants;
using SwiftApi.Data.Models;
using System.Transactions;

namespace SwiftApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FileUploadController : Controller
	{
		private readonly IMessageService messageService;
		private readonly ILogger<FileUploadController> logger;

		public FileUploadController(IMessageService messageService, ILogger<FileUploadController> logger)
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
			using (var reader = new StreamReader(file.OpenReadStream()))
			{
				var swiftMessage = await reader.ReadToEndAsync();
				var message = messageService.InsertSwiftMessage(swiftMessage);
				if (message != null)
				{
					logger.LogInformation("Message Saved via Uploaded File: => {@message}", message);
					return StatusCode(StatusCodes.Status200OK, message);
				}

				logger.LogError("Incorrect file content");
				return StatusCode(StatusCodes.Status400BadRequest, "Incorrect file content");


			}
		}
	}
}
