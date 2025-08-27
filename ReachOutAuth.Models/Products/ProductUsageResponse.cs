using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// The Products namespace.
/// </summary>
namespace ReachOutAuth.Models.Products
{
    /// <summary>
    /// Class ProductUsageResponse.
    /// </summary>
    public class ProductUsageResponse
    {
        /// <summary>
        /// Gets or sets the document number.
        /// </summary>
        /// <value>The document number.</value>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets the product usages.
        /// </summary>
        /// <value>The product usages.</value>
        public List<ProductUsage> Products { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUsageResponse"/> class.
        /// </summary>
        public ProductUsageResponse() 
        { 
            this.Products = new List<ProductUsage>();
        }
    }
}
