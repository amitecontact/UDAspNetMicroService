using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Common
{
    public abstract class TransactionBase : EntityBase
    {
        public string DocName { get; set; }
        public DateTime DocDate { get; set; }
    }
}
