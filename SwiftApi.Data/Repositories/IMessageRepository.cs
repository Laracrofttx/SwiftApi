using SwiftApi.Data.Models;

namespace SwiftApi.Data.Repositories
{
	public interface IMessageRepository
	{
		Task<ResponseMessage<string>> InsertSwiftMessageAsync(Message message);
	}
}
