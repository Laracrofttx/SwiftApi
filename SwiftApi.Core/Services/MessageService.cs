using SwiftApi.Core.Services.Interfaces;
using SwiftApi.Data.Models;
using SwiftApi.Data.Repositories;

namespace SwiftApi.Core.Services
{
	public class MessageService : IMessageService
	{
		private readonly IMessageRepository messageRepository;

		public MessageService(IMessageRepository messageRepository)
		{
			this.messageRepository = messageRepository;
		}

		public Task<Message> InsertAllSwiftMessagesAsync(string message)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseMessage<Message>> PargeSwiftMessageAsync(string message)
		{
			throw new NotImplementedException();
		}

		public IList<Message> GetAllSwiftMessages()
		{
			throw new NotImplementedException();
		}

	}
}
