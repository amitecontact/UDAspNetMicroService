using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, IList<OrderDTO>>
    {

        private IOrderRepository _OrderRepository;
        private IMapper _Mapper;

        public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _OrderRepository = orderRepository;
            _Mapper = mapper;
        }

        public async Task<IList<OrderDTO>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _OrderRepository.GetOrdersByUserName(request.UserName);
            return _Mapper.Map<List<OrderDTO>>(orderList);
        }
    }
}
