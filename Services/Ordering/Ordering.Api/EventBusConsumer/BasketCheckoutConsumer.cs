﻿using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
// Important : be careful Using MediatR not other Mediator Like MassTransit.Mediator , ...

namespace Ordering.Api.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await _mediator.Send(command);
            _logger.LogInformation($"order consumed Successfully and order id  is : {result} ");
        }
    }
}
