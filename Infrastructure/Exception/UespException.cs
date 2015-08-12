using System;

namespace Infrastructure.Exception
{
    public class UespException : ApplicationException
    {
        public UespException(string message) : base(message)
        {
            
        }
    }
}
