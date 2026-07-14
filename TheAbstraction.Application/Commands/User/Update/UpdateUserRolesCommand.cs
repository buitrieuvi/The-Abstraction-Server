using TheAbstraction.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAbstraction.Application.Commands.User.Update
{
    public class UpdateUserRolesCommand : IRequest<int>
    {
        public string userName { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class UpdateUserRolesCommandHandler(IIdentityService identityService) : IRequestHandler<UpdateUserRolesCommand, int>
    {
        private readonly IIdentityService _identityService = identityService;

        public async Task<int> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.UpdateUsersRole(request.userName, request.Roles);
            return result ? 1 : 0;
        }
    }
}
