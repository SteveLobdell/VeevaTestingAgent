using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

/// <summary>
/// The Messages namespace.
/// </summary>
namespace ReachOutAuth.Models.Messages
{
    /// <summary>
    /// A response containing success or error messages for submitted orders
    /// </summary>
    public class ProductResponseMessage : ResponseMessage
    {
        /// <summary>
        /// Gets or sets the product sku.
        /// </summary>
        /// <value>The product sku.</value>
        [Required]
        public string ProductSku { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ProductResponseMessage"/> is gated.
        /// </summary>
        /// <value><c>true</c> if gated; otherwise, <c>false</c>.</value>
        public bool Gated { get; set; }
    }
}
