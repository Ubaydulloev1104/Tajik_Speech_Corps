using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSR_Accoun_Application.Common.Exceptions;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Application.Contracts.UserRoles.Commands;

namespace TSR_Accoun_Application.Features.UserRoles.Commands
{
	public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand, bool>
	{
		private readonly IApplicationDbContext _context;

		public DeleteUserRoleCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
		{
			var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.Slug == request.Slug);
			_ = userRole ?? throw new NotFoundException("not found");
			_context.UserRoles.Remove(userRole);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
