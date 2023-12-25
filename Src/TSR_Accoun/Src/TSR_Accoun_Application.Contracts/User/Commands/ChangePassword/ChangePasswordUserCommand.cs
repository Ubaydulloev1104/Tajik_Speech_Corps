using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSR_Accoun_Application.Contracts.User.Commands.ChangePassword
{
	public class ChangePasswordUserCommand : IRequest<bool>
	{
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
