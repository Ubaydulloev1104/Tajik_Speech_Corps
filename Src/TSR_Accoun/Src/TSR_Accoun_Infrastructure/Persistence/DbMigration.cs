using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSR_Accoun_Infrastructure.Persistence
{
	public class DbMigration
	{
		private readonly ApplicationDbContext _context;

		public DbMigration(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task InitialiseAsync()
		{
			if (_context.Database.IsSqlServer())
			{
				await _context.Database.MigrateAsync();
			}
		}
	}
}
