using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(Slug))]
    public class Text : BaseAuditableEntity
    {
        public string Slug { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
		public Guid CategoryId { get; set; }
		public TextCategory Category { get; set; }
		public ICollection<Application> Applications { get; set; }
		public ICollection<TextTimelineEvent> History { get; set; }
		public DateTime CreateDate { get; set; } 
		public DateTime? UpdatedDate { get; set; }
	}
}
