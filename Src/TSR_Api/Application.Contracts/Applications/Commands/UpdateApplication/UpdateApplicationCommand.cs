using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Applications.Commands.UpdateApplication
{
    public class UpdateApplicationCommand : IRequest<Guid>
    {
        public string Slug { get; set; }
    }
}
