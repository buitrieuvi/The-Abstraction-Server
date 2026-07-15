using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Commands.Product.Create;
using TheAbstraction.Application.Common.Interfaces;

namespace TheAbstraction.Application.Commands.Player.Create
{
    public class CreatePlayerCommand : IRequest<int>
    {
        public string PlayerName { get; set; }
    }

    public class CreatePlayerCommandHandler(IPlayerService playerService) : IRequestHandler<CreatePlayerCommand, int>
    {
        private readonly IPlayerService _playerService = playerService;
        public Task<int> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            return _playerService.Create(request.PlayerName);
        }
    }
}
