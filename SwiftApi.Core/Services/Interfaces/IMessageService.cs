using SwiftApi.Data.Models;

namespace SwiftApi.Core.Services.Interfaces
{
	public interface IMessageService
	{
		public Task<Message> InsertAllSwiftMessagesAsync(string message);
		public Task<ResponseMessage<Message>> PargeSwiftMessageAsync(string message);
		public IList<Message> GetAllSwiftMessages();
	}
}
