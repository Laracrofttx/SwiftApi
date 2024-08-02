using SwiftApi.Data.Models;

namespace SwiftApi.Core.Services.Interfaces
{
	public interface IMessageService
	{
		public Task<ResponseMessage<string>> InsertAllSwiftMessagesAsync(Message message);
		public Task<ResponseMessage<Message>> PargeSwiftMessageAsync(string content);
	}
}
