using MediatR;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Common.Exceptions;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Application.Contracts.Claim.Commands;

namespace TSR_Accoun_Application.Features.Claims.Commands
{
	public class DeleteClaimCommandHandler : IRequestHandler<DeleteClaimCommand, Unit>
	{
		private readonly IApplicationDbContext _context;

		public DeleteClaimCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
		{
			var claim = await _context.UserClaims.SingleOrDefaultAsync(s => String.Equals(s.Slug.Trim(), request.Slug.Trim(), StringComparison.CurrentCultureIgnoreCase), cancellationToken: cancellationToken);
			_ = claim ?? throw new NotFoundException($"claim with slug {request.Slug} not found");

			_context.UserClaims.Remove(claim);

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
