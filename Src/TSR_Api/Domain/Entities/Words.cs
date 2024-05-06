using Microsoft.EntityFrameworkCore;
using Sieve.Attributes;

namespace Domain.Entities
{
    [Index(nameof(Slug))]
    public class Words : BaseAuditableEntity
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public string Slug { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
		public Guid CategoryId { get; set; }
		public WordCategory Category { get; set; }
		public ICollection<Application> Applications { get; set; }
		public ICollection<WordTimelineEvent> History { get; set; }
		public DateTime CreateDate { get; set; } 
		public DateTime? UpdatedDate { get; set; }

	}
}
