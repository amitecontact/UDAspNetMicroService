using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException()
        {
            Error = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Error = failures
                        .GroupBy(f => f.PropertyName, f => f.ErrorMessage)
                        .ToDictionary(fg => fg.Key, fg => fg.ToArray());
        }

        public IDictionary<string, string[]> Error { get; }
    }
}
