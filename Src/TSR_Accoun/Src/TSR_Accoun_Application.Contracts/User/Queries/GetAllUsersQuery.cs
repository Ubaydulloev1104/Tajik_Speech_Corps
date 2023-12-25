using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSR_Accoun_Application.Contracts.User.Responses;

namespace TSR_Accoun_Application.Contracts.User.Queries
{
	public class GetAllUsersQuery : IRequest<List<UserResponse>>
	{
	}
}
