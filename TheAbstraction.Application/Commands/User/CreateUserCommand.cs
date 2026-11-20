using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Common.Interfaces.Auth;

namespace TheAbstraction.Application.Commands.User
{
    public class CreateUserCommand : IRequest<string>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
        public List<string> Roles { get; set; }
    }

    public class CreateUserCommandHandler(IIdentityService identityService) : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IIdentityService _identityService = identityService;

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmationPassword)
            {
                throw new Common.Exceptions.BadRequestException("Mật khẩu xác nhận không khớp");
            }

            var result = await _identityService.CreateUserAsync(request.UserName, request.Password, request.Email, request.FullName, request.Roles);
            return result.userId;
        }
    }
}
