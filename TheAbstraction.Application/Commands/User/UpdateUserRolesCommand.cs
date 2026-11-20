using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Common.Interfaces.Auth;

namespace TheAbstraction.Application.Commands.User
{
    public class UpdateUserRolesCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class UpdateUserRolesCommandHandler(IIdentityService identityService) : IRequestHandler<UpdateUserRolesCommand, int>
    {
        private readonly IIdentityService _identityService = identityService;

        public async Task<int> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var userName = await _identityService.GetUserNameAsync(request.UserId);
            var result = await _identityService.UpdateUsersRole(userName, request.Roles);
            return result ? 1 : 0;
        }
    }
}
