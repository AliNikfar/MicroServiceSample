using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForDelete = _orderRepository.GetByIdAsync(request.Id);
            if (orderForDelete is null)
            {
                _logger.LogError(" Order Is Not Exists");
            }
            _mapper.Map(request, orderForDelete, typeof(DeleteOrderCommand), typeof(Order));
            var result = await _orderRepository.DeleteAsync(orderForDelete);
            _logger.LogInformation($"Order {orderForDelete.Id} is succesfuly updated");
            return Unit.Value;

        }
    }
}
