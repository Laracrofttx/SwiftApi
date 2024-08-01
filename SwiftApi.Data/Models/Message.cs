namespace SwiftApi.Data.Models
{
	public class Message
	{
		public int Id { get; set; }
		public string MessageType { get; set; }
		public string SenderBIC { get; set; }
		public string TransactionReferenceNumber { get; set; }
		public string RelatedReference { get; set; }
		public string NarrativeText { get; set; }
		public string MAC { get; set; }
		public string CHK { get; set; }
	}
}
