using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
        }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
            : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public DbSet<ApplicantSocialMedia> ApplicantSocialMedias { get; set; }
        public DbSet<UserTimelineEvent> UserTimelineEvents { get; set; }
        public DbSet<TimelineEvent> TimelineEvents { get; set; }
        public DbSet<Domain.Entities.Application> Applications { get; set; }
        public DbSet<ApplicationTimelineEvent> ApplicationTimelineEvents { get; set; }
        public DbSet<EducationDetail> EducationDetails { get; set; }
        public DbSet<Words> Words { get; }
        public DbSet<WordTimelineEvent> WordTimelineEvents { get; }
        public DbSet<WordCategory> Categories { get; }
        public DbSet<Audio> Audios { get; set; }


        #region override

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // builder.Entity<BaseEntity>().HasQueryFilter(e => !e.IsDeleted);
            builder.Ignore<BaseEntity>();
            builder.Ignore<BaseAuditableEntity>();
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //await _mediator.DispatchDomainEvents(this); //Right now we do not need domain events.
            foreach (EntityEntry entry in ChangeTracker.Entries()
                         .Where(e => e.State == EntityState.Deleted && e.Entity is ISoftDelete))
            {
                entry.State = EntityState.Modified;
                ((ISoftDelete)entry.Entity).IsDeleted = true;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
