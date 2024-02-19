using FluentValidation.Results;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Eexceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; set; }
        public ValidationException()
            : base("one or more validation Failure have occured ")
        {
            Errors = new ConcurrentDictionary<string, string[]>();
        }
        // Get the Errors from fluentValidation Errors and return it 
        public ValidationException(IEnumerable<ValidationFailure> failures)
            :this()
        {
            Errors = failures.GroupBy(s => s.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key,
                failureGroup => failureGroup.ToArray());
        }
    }
}
