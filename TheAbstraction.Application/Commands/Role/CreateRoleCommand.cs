using MediatR;
using TheAbstraction.Application.Common.Interfaces.Auth;

namespace TheAbstraction.Application.Commands.Role;

public class CreateRoleCommand : IRequest<int>
{
    public string RoleName { get; set; }
}

public class CreateRoleCommandHandler(IIdentityService identityService) : IRequestHandler<CreateRoleCommand, int>
{
    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var created = await identityService.CreateRoleAsync(request.RoleName);
        return created ? 1 : 0;
    }
}
