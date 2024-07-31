using Microsoft.Data.Sqlite;
using SwiftApi.Data.Models;
using System.Data.Common;

namespace SwiftApi.Data.Repositories
{
	public class MessageRepository : IMessageRepository
	{
		public Task<ResponseMessage> InsertSwiftMessage(Message message)
		{
			throw new NotImplementedException();
		}
	}
}
