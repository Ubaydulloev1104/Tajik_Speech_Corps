using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Domain.Entities.Application> Applications { get; }
    public DbSet<ApplicantSocialMedia> ApplicantSocialMedias { get; }
    public DbSet<ApplicationTimelineEvent> ApplicationTimelineEvents { get; }
    public DbSet<Words> Words { get; }
    public DbSet<WordTimelineEvent> WordTimelineEvents { get; }
    public DbSet<WordCategory> Categories { get; }
    public DbSet<UserTimelineEvent> UserTimelineEvents { get; }
    public DbSet<TimelineEvent> TimelineEvents { get; }
    public DbSet<EducationDetail> EducationDetails { get; set; }
    public DbSet<Audio> Audios { get; set; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}
