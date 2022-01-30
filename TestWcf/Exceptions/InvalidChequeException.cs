using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWcf.Exceptions
{
    public class InvalidChequeException : Exception
    {
        public InvalidChequeException() : base(){}
    }
}