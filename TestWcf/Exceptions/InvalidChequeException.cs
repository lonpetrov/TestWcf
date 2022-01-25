//-----------------------------------------------------------------------
// <copyright file="InvalidChequeException.cs" company="Manzana">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TestWcf.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// InvalidChequeException сlass
    /// </summary>
    public class InvalidChequeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidChequeException"/> class
        /// </summary>
        public InvalidChequeException() : base()
        {
        }
    }
}