using Application.Services.Commands.CreateService;
using Application.Services.Commands.UpdateService;
using Application.Services.Queries.GetServiceById;
using Application.Services.Queries.GetServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CARGAU.Controllers;

[ApiController]
[Route("/services")]
public class ServicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServicesController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet("get")]
    public async Task<IResult> GetServices([FromQuery] int page, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var services = await _mediator.Send(new GetServicesQuery(page, pageSize), cancellationToken);
        return Results.Ok(services);
    }

    [HttpGet("{id:int}", Name = "GetServiceById")]
    public async Task<IResult> GetServiceById(int id, CancellationToken cancellationToken)
    {
        var service = await _mediator.Send(new GetServiceByIdQuery(id), cancellationToken);

        return service != null ? Results.Ok(service) : Results.NotFound("Услуга не найдена");
    }

    [HttpPost("add")]
    public async Task<IResult> AddService([FromBody] CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var serviceId = await _mediator.Send(request, cancellationToken);

        return Results.CreatedAtRoute(
            routeName: nameof(GetServiceById),
            routeValues: new { id = serviceId },
            value: new { ServiceId = serviceId, Message = "Услуга успешно создана" }
        );
    }

    [HttpPut("/update-service")]
    public async Task<IResult> UpdateService([FromBody] UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Results.NoContent();
    }
}