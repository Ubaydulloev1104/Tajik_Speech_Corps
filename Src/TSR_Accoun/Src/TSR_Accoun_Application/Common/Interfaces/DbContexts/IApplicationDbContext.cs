using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Common.Interfaces.DbContexts
{
	public interface IApplicationDbContext
	{
		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		public DbSet<ApplicationUser> Users { get; set; }
		public DbSet<ApplicationUserClaim> UserClaims { get; set; }
		public DbSet<ApplicationUserRole> UserRoles { get; set; }
		public DbSet<ApplicationRole> Roles { get; set; }
		public DbSet<IdentityRoleClaim<Guid>> RoleClaims { get; set; }
		public DbSet<EducationDetail> Educations { get; set; }
	}
}
