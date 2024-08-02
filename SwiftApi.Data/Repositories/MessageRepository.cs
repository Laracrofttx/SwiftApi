using Microsoft.Data.Sqlite;
using SwiftApi.Data.Models;
using System.Data.Common;

namespace SwiftApi.Data.Repositories
{
	public class MessageRepository : IMessageRepository
	{
		private readonly SwiftApiDbContext swiftApiDbContext;

        public MessageRepository(SwiftApiDbContext swiftApiDbContext)
        {
		   this.swiftApiDbContext = swiftApiDbContext;
        }
        public async Task<ResponseMessage<string>> InsertSwiftMessageAsync(Message message)
		{
			var response = new ResponseMessage<string>();

			try
			{
				 await this.swiftApiDbContext.InsertDataAsync(message);
				response.Data = GlobalConstants.GlobalConstants.SuccessfulInsert;
			}
			catch (Exception ex)
			{
				response.IsSuccessful = false;
				throw new InvalidOperationException(ex.Message);
			}
			return response;
		}
	}
}
