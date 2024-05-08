using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;
[Index(nameof(Slug))]
public class Audio : BaseAuditableEntity
{
    public Guid Id { get; set; }
    public string Slug { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string Format { get; set; }
    public Words Word { get; set; }

}
}
