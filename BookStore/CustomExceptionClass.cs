using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class CustomExceptionClass : Exception
    {
        public enum ExceptionType
        {
            EMPTY_PARAMETER
        }
        private readonly ExceptionType type;
        public CustomExceptionClass(ExceptionType Type,string Message) : base(Message)
        {
            this.type = Type;
        }
    }
}
