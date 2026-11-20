using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Common.Interfaces.Auth;
using TheAbstraction.Application.DTOs.Role;

namespace TheAbstraction.Application.Queries.Role
{
    public class GetRoleQuery : IRequest<IList<RoleResponseDTO>>
    {

    }

    public class GetRoleQueryHandler(IIdentityService identityService) : IRequestHandler<GetRoleQuery, IList<RoleResponseDTO>>
    {
        private readonly IIdentityService _identityService = identityService;

        public async Task<IList<RoleResponseDTO>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _identityService.GetRolesAsync();
            return roles.Select(role => new RoleResponseDTO() { Id = role.id, RoleName = role.roleName }).ToList();
        }
    }
}
