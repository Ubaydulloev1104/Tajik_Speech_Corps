using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
	[Index(nameof(Slug))]
	public class Audio
	{
		public string Slug { get; set; }
		public string FileName { get; set; }
		public Text Text { get; set; }


	}
}
