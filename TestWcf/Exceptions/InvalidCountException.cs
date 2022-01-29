using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWcf.Exceptions
{
    public class InvalidCountException:Exception
    {
        public int Count { get; set; }
        public InvalidCountException(int count) : base()
        {
            Count = count;
        }
    }
}