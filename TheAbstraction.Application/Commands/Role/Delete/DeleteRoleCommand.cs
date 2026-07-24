using TheAbstraction.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAbstraction.Application.Commands.Role.Delete
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
    }

    public class DeleteRoleCommandHandler(IIdentityService identityService) : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IIdentityService _identityService = identityService;

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.DeleteRoleAsync(request.RoleId);
            return result;
        }
    }
}
