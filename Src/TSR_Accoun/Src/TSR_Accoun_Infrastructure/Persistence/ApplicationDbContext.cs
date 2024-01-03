using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Domain.Entities;
using TSR_Accoun_Infrastructure.Persistence.TableConfigurations;

namespace TSR_Accoun_Infrastructure.Persistence
{
	public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
	ApplicationUserClaim, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
	IdentityUserToken<Guid>>, IApplicationDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
			base(options)
		{
		}
		public DbSet<EducationDetail> Educations { get; set; }
		

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfiguration(new ApplicationRoleConfiguration());
			
		}
	}
}
