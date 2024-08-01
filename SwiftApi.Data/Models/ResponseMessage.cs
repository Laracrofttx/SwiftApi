namespace SwiftApi.Data.Models
{
	public class ResponseMessage<T>
	{
		public bool IsSuccessful { get; set; } = true;
		public string Message { get; set; }
		public T Data { get; set; }
	}
}
