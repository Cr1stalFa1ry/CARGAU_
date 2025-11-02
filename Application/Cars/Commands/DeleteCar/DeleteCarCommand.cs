using MediatR;

namespace Application.Cars.Commands.DeleteCar;

public record class DeleteCarCommand(Guid id) : IRequest;