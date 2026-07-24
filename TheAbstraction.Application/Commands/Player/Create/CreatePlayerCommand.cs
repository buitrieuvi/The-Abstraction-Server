using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TheAbstraction.Application.Common.Interfaces;

namespace TheAbstraction.Application.Commands.Player.Create
{
    public class CreatePlayerCommand : IRequest<int>
    {
        public string PlayerName { get; set; }
    }

    public class CreatePlayerCommandHandler(IPlayerService playerService, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreatePlayerCommand, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IPlayerService _playerService = playerService;
        public Task<int> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return _playerService.Create(userId, request.PlayerName);
        }
    }
}
