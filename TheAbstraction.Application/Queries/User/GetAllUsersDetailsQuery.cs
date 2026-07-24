using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAbstraction.Application.Queries.User
{
    public class GetAllUsersDetailsQuery : IRequest<List<UserDetailsResponseDTO>>
    {

    }

    public class GetAllUsersDetailsQueryHandler(IIdentityService identityService) : IRequestHandler<GetAllUsersDetailsQuery, List<UserDetailsResponseDTO>>
    {
        private readonly IIdentityService _identityService = identityService;

        public async Task<List<UserDetailsResponseDTO>> Handle(GetAllUsersDetailsQuery request, CancellationToken cancellationToken)
        {
            var users = await _identityService.GetAllUsersAsync();

            var result = new List<UserDetailsResponseDTO>();

            foreach (var user in users)
            {
                result.Add(new() 
                {
                    Id = user.id,
                    FullName = user.fullName,
                    UserName = user.userName,
                    Email = user.email,
                    Roles = await _identityService.GetUserRolesAsync(user.id),
                });

            }

            return result;
        }
    }
}
