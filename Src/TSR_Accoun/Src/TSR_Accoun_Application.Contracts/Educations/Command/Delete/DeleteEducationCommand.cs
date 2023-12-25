using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSR_Accoun_Application.Contracts.Educations.Command.Delete
{
	public class DeleteEducationCommand : IRequest<bool>
	{
		public Guid Id { get; set; }
	}
}
