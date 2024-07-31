using SwiftApi.Data.Models;

namespace SwiftApi.Data.Repositories
{
	public interface IMessageRepository
	{
		Task<ResponseMessage> InsertSwiftMessage(Message message);
	}
}
