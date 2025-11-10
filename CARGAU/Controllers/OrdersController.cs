using Application.Orders.Commands.CreateOrder;
using Application.Orders.Commands.DeleteOrder;
using Application.Orders.Commands.UpdateStatusOrder;
using Application.Orders.Queries.GetOrder;
using Application.Orders.Queries.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CARGAU.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrdersController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> CreateOrder([FromBody] CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = await _mediator.Send(request);
        return Results.CreatedAtRoute(
            routeName: nameof(GetOrderById),
            routeValues: new { id = orderId },
            value: new { OrderId = orderId, Message = "Order created successfully" }
        );
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> GetOrders([FromQuery] GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _mediator.Send(request, cancellationToken);

        return Results.Ok(new
        {
            Orders = orders
        });
    }

    [HttpGet("{id:guid}", Name = nameof(GetOrderById))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetOrderById(Guid id, CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(new GetOrderQuery { OrderId = id }, cancellationToken);

        return Results.Ok(new
        {
            Order = order
        });
    }

    [HttpPatch("/update-status")]
    public async Task<IResult> UpdateStatusOrder(UpdateStatusOrderCommand request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken)
            ? Results.NoContent() : Results.NotFound("order is not found");
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> DeleteOrderById(Guid id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new DeleteOrderByIdCommand {OrderId = id}, cancellationToken)
            ? Results.NoContent() : Results.NotFound("order is not found");
    }
}
