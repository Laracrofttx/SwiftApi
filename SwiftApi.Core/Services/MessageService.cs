using SwiftApi.Core.Services.Interfaces;
using SwiftApi.Data.Models;

namespace SwiftApi.Core.Services
{
	public class MessageService : IMessageService
	{
		public IList<Message> GetAllSwiftMessages()
		{
			throw new NotImplementedException();
		}

		public Task<Message> PargeSwiftMessage(string message)
		{
			throw new NotImplementedException();
		}

		public Task<Message> TakeAllSwiftMessages(string message)
		{
			throw new NotImplementedException();
		}
	}
}
