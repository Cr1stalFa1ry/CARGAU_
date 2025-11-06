using MediatR;

namespace Application.Cars.Commands.ChangeOwnerCar;

public record class ChangeOwnerCarCommand(Guid carId, Guid newOwnerId) : IRequest;
