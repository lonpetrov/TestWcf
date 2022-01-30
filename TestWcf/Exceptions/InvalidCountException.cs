//-----------------------------------------------------------------------
// <copyright file="InvalidCountException.cs" company="Manzana">
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
    /// InvalidCountException сlass
    /// </summary>
    public class InvalidCountException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCountException"/> class.
        /// </summary>
        /// <param name="count">Size of cheques</param>
        public InvalidCountException(int count) : base()
        {
            this.Count = count;
        }

        /// <summary>
        /// Gets or sets Count property.
        /// </summary>
        public int Count { get; set; }
    }
}