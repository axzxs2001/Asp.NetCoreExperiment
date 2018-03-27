using Demo.Domain.AggregatesModel.OrderAggregate;
using Demo.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Application.Application.DomainEventHandlers
{
    public class OrderStatusChangedToPaidDomainEventHandler
                   : INotificationHandler<OrderStatusChangedToPaidDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILoggerFactory _logger;
        //private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

        public OrderStatusChangedToPaidDomainEventHandler(
            IOrderRepository orderRepository, ILoggerFactory logger//,
            //IOrderingIntegrationEventService orderingIntegrationEventService
            )
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_orderingIntegrationEventService = orderingIntegrationEventService;
        }

        public async Task Handle(OrderStatusChangedToPaidDomainEvent orderStatusChangedToPaidDomainEvent, CancellationToken cancellationToken)
        {
            _logger.CreateLogger(nameof(OrderStatusChangedToPaidDomainEventHandler))
             .LogTrace($"Order with Id: {orderStatusChangedToPaidDomainEvent.OrderId} has been successfully updated with " +
                       $"a status order id: {OrderStatus.Paid.Id}");

            var orderStockList = orderStatusChangedToPaidDomainEvent.OrderItems
                .Select(orderItem => new { orderItem.ProductId,U= orderItem.GetUnits() });

           // var orderStatusChangedToPaidIntegrationEvent = new OrderStatusChangedToPaidIntegrationEvent(orderStatusChangedToPaidDomainEvent.OrderId,  orderStockList);
          //  await _orderingIntegrationEventService.PublishThroughEventBusAsync(orderStatusChangedToPaidIntegrationEvent);
        }
    }
}
