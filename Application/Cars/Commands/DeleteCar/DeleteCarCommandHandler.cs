using System;
using Domain.Interfaces.Repositoties;
using MediatR;

namespace Application.Cars.Commands.DeleteCar;

public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand>
{
    private readonly ICarRepository _repository;
    public DeleteCarCommandHandler(ICarRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.id, cancellationToken);
    }
}
