//-----------------------------------------------------------------------
// <copyright file="SqlCheque.cs" company="Manzana">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TestWcf
{
    using System;

    /// <summary>
    /// Cheque class.
    /// </summary>
    public class SqlCheque
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary> 
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets Number.
        /// </summary> 
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets Sum.
        /// </summary> 
        public decimal? Summ { get; set; }

        /// <summary>
        /// Gets or sets Discount.
        /// </summary> 
        public decimal? Discount { get; set; }

        /// <summary>
        /// Gets or sets Articles.
        /// </summary> 
        public string Articles { get; set; }
    }
}