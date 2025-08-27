using ReachOutAuth.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReachOutAuth.Models.Messages
{
    /// <summary>
    /// Class ProductRequest.
    /// </summary>
    public class ProductRequest
    {
        /// <summary>
        /// Enum RequestType
        /// </summary>
        public enum RequestType
        {
            /// <summary>
            /// The add or update
            /// </summary>
            AddOrUpdate,

            /// <summary>
            /// The add
            /// </summary>
            Add,

            /// <summary>
            /// The update
            /// </summary>
            Update
        }

        /// <summary>
        /// The ReachOut.cloud supplied Client identifier used for this integration
        /// </summary>
        /// <value>The reach out client identifier.</value>
        [Required]
        public string ReachOutClientId { get; set; }

        /// <summary>
        /// Gets or sets the type of the product request.
        /// </summary>
        /// <value>The type of the product request.</value>
        public RequestType ProductRequestType { get; set; }

        /// <summary>
        /// The products
        /// </summary>
        public List<ReachOutProduct> Products;

        /// <summary>
        /// Initializes a new instance of the ProductRequest /&gt; class.
        /// </summary>
        public ProductRequest()
        {
            this.Products = new List<ReachOutProduct>();
        }

        /// <summary>
        /// Shoulds the add new product.
        /// </summary>
        /// <returns><c>true</c> if should add the product, <c>false</c> otherwise.</returns>
        public bool ShouldAddNewProduct()
        {
            return this.ProductRequestType == RequestType.AddOrUpdate || this.ProductRequestType == RequestType.Add;
        }
    }
}
