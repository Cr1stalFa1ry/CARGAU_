using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Cars.Commands.DeleteCar;
using Application.Cars.Commands.CreateCar;
using Application.Cars.Queries.GetServicesByCar;
using Application.Cars.Queries.GetCar;
using Application.Cars.Queries.GetCars;
using Application.Cars.Commands.UpdatePriceCar;

namespace CARGAU.Controllers;

[ApiController]
[Route("cars")]
public class CarsController : ControllerBase
{
    private readonly IMediator _mediator;
    public CarsController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("/add-car")]
    public async Task<IResult> CreateCar(
        [FromBody] CreateCarCommand request,
        CancellationToken cancellationToken)
    {
        var carId = await _mediator.Send(request, cancellationToken);
        return Results.CreatedAtRoute(
            routeName: "GetCarById",
            routeValues: new { id = carId },
            value: new { id = carId, message = "Car created successfully" }
        );
    }

    [HttpGet("{id:guid}", Name = "GetCarById")]
    public async Task<IResult> GetCarById(Guid id, CancellationToken cancellationToken)
    {
        var car = await _mediator.Send(new GetCarByIdQuery(id), cancellationToken);
        return Results.Ok(car);
    }

    [HttpGet("/list-cars")]
    public async Task<IResult> GetCars(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        var car = await _mediator.Send(new GetCarsQuery(page, pageSize), cancellationToken);
        return Results.Ok(car);
    }

    [HttpGet("get-services-by-car/{id:guid}")]
    public async Task<IResult> GetServicesByCarId(Guid id, CancellationToken cancellationToken)
    {
        var listServices = await _mediator.Send(new GetServicesByCarQuery(id), cancellationToken);

        return Results.Ok(listServices);
    }

    [HttpPatch("update-price-car")]
    public async Task<IResult> UpdatePriceCar(
        UpdatePriceCarCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Results.NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IResult> RemoveCarById(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCarCommand(id), cancellationToken);
        return Results.NoContent();
    }
}
