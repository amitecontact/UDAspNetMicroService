using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    interface IOrderRepository : IAsynRepository<Order>
    {
        Task<IReadOnlyList<Order>> GetOrderByUserName(string userName);
    }
}
