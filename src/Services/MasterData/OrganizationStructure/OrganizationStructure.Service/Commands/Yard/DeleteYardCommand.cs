using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationManagementService.Service.Commands.Yard
{
    public class DeleteYardCommand : IRequest<int?>
    {
        public int BusinessEntityId { get; set; }
        public DeleteYardCommand(int _businessEntityId)
        {
            BusinessEntityId = _businessEntityId;
        }
    }
}