using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmslib.Exceptions
{
    public class ValidationException : System.Exception
    {
        public ValidationException() : base()
        { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
