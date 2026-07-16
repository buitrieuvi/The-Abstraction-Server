using System.Security.Claims;
using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace TheAbstraction.Application.Commands.Player.Create
{
    public class CreatePlayerCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string PlayerName { get; set; }
    }

    public class CreatePlayerCommandHandler(IPlayerService playerService) : IRequestHandler<CreatePlayerCommand, int>
    {
        private readonly IPlayerService _playerService = playerService;
        public Task<int> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            return _playerService.Create(request.UserId, request.PlayerName);
        }
    }
}
