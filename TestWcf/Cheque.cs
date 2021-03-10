using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWcf
{
    public class Cheque : ICheque
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public decimal Summ { get; set; }

        public decimal Discount { get; set; }

        public string[] Articles { get; set; }
    }
}