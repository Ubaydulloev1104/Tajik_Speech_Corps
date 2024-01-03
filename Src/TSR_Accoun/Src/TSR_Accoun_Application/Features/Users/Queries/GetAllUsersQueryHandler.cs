using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSR_Accoun_Application.Contracts.User.Queries;
using TSR_Accoun_Application.Contracts.User.Responses;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Users.Queries
{
	public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;

		public GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}
		public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
		{
			var users = await _userManager.Users.ToListAsync(cancellationToken);
			var result = _mapper.Map<List<UserResponse>>(users);

			return result;
		}
	}
}
