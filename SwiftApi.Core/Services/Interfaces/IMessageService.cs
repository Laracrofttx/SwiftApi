using SwiftApi.Data.Models;

namespace SwiftApi.Core.Services.Interfaces
{
	public interface IMessageService
	{
		public Task<Message> TakeAllSwiftMessages(string message);
		public Task<Message> PargeSwiftMessage(string message);
		public IList<Message> GetAllSwiftMessages();
	}
}
