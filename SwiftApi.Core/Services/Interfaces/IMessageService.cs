using SwiftApi.Data.Models;

namespace SwiftApi.Core.Services.Interfaces
{
	public interface IMessageService
	{
		public Message InsertSwiftMessage(string swiftMessage);
		public Message ParseSwiftMessage(string swiftMessage);
	}
}
