using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// The Products namespace.
/// </summary>
namespace ReachOutAuth.Models.Products
{
    /// <summary>
    /// Class ProductUsageData.
    /// </summary>
    public class ProductUsageData
    {
        /// <summary>
        /// Gets or sets the name of the month.
        /// </summary>
        /// <value>The name of the month.</value>
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the month amount.
        /// </summary>
        /// <value>The month amount.</value>
        public int Quanity { get; set; }

        /// <summary>
        /// Gets or sets the pieces.
        /// </summary>
        /// <value>The pieces.</value>
        public int LineItems { get; set; }
    }
}
