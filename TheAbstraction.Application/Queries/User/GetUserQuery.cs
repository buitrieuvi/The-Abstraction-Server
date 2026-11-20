using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Common.Interfaces.Auth;
using TheAbstraction.Application.DTOs.User;

namespace TheAbstraction.Application.Queries.User
{
    public class GetUserQuery : IRequest<List<UserResponseDTO>>
    {
    }

    public class GetUserQueryHandler(IIdentityService identityService) : IRequestHandler<GetUserQuery, List<UserResponseDTO>>
    {
        private readonly IIdentityService _identityService = identityService;

        public async Task<List<UserResponseDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _identityService.GetAllUsersAsync();
            return users.Select(x => new UserResponseDTO()
            {
                Id = x.id,
                FullName = x.fullName,
                UserName = x.userName,
                Email = x.email
            }).ToList();
        }
    }
}
