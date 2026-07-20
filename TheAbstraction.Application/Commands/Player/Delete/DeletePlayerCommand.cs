using MediatR;
using TheAbstraction.Application.Common.Interfaces;

namespace TheAbstraction.Application.Commands.Player.Delete;

public class DeletePlayerCommand : IRequest<int>
{
    public string PlayerId { get; set; }
}

public class DeletePlayerCommandHandler(IPlayerService playerService) : IRequestHandler<DeletePlayerCommand, int>
{
    private readonly IPlayerService _playerService = playerService;
    public Task<int> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        return _playerService.Delete(request.PlayerId);
    }
}
