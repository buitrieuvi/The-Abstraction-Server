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
    public class GetUserDetailsQuery : IRequest<UserDetailsResponseDTO>
    {
        public string UserId { get; set; }
    }

    public class GetUserDetailsQueryHandler(IIdentityService identityService) : IRequestHandler<GetUserDetailsQuery, UserDetailsResponseDTO>
    {
        private readonly IIdentityService _identityService = identityService;

        public async Task<UserDetailsResponseDTO> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(request.UserId);
            return new UserDetailsResponseDTO() { Id = userId, FullName = fullName, UserName = userName, Email = email, Roles = roles };
        }
    }
}
