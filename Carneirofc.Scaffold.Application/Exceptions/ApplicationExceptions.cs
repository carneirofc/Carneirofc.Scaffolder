using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carneirofc.Scaffold.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ValidationException : ApplicationException
    {
        public ValidationException() { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class UnauthorizedException : ApplicationException
    {
        public UnauthorizedException() { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }


    public class BadRequestException : ApplicationException
    {
        public BadRequestException() { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InternalErrorException : ApplicationException
    {
        public InternalErrorException() { }

        public InternalErrorException(string message) : base(message) { }

        public InternalErrorException(string message, Exception innerException) : base(message, innerException) { }
    }
}
