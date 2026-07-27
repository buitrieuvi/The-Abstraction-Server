using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Common.Exceptions;
using TheAbstraction.Application.Common.Interfaces;

namespace TheAbstraction.Application.Commands.User
{
    public class DeleteUserCommand : IRequest<int>
    {
        public string UserId { get; set; }
    }

    public class DeleteUserCommandHandler(IIdentityService identityService, IHttpContextAccessor httpContextAccessor) : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IIdentityService _identityService = identityService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userIdJWT = _httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdJWT))
            {
                throw new ForbiddenAccessException("Bạn cần đăng nhập để xoá tài khoản");
            }

            var rolesJWT = await _identityService.GetUserRolesAsync(userIdJWT);


            if (userIdJWT == request.UserId || rolesJWT.Any(role => string.Equals(role, "admin", StringComparison.OrdinalIgnoreCase)))
            {
                var result = await _identityService.DeleteUserAsync(request.UserId);
                return result ? 1 : 0;
            }

            return 0;
        }
    }
}
