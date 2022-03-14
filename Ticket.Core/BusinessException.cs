using System;

namespace Ticket.Core
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public string ErrorCode { get; set; }
    }
}
