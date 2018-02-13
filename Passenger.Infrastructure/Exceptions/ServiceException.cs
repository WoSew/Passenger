using System;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Exceptions
{
    public class ServiceException : PassengerException
    {
        protected ServiceException()
        {       
        }

        public ServiceException(string code) : base(code)
        {
        }

        public ServiceException(string message, params object[] args) : base(string.Empty, message, args)
        {   
        }

        public ServiceException(string code, string message, params object[] args) : base(null, code, message, args)
        {
        }

        public ServiceException(Exception innerException, string message,  params object[] args) : base(innerException, string.Empty, message, args)
        {   
        }

        public ServiceException(Exception innerException, string code,  string message,  params object[] args) : base(code, string.Format(message, args), innerException)
        {
        }      
    }
}