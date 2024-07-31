namespace SwiftApi.Data.Models
{
	public class Message
	{
		public int Id { get; set; }
		public string BasicHeader { get; set; }
		public string AppHeader { get; set; }
		public string UserHeader { get; set; }
		public TextBlock TextBlock { get; set; }
		public TrailersBlock TrailesBlock { get; set; }
	}
}
