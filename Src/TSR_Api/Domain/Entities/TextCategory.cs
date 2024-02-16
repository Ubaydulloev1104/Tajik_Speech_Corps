using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
	[Index(nameof(Slug), IsUnique = true)]
	public class TextCategory : BaseEntity
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public ICollection<Text> Texts { get; set; }
	}
}
