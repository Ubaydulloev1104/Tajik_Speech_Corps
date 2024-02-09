using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(Slug))]
    public class Text : BaseAuditableEntity
    {
        public DateTime PublishDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Slug { get; set; }

    }
}
