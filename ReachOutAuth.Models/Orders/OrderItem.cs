using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReachOutAuth.Models.Orders
{
    /// <summary>
    /// Represents a line item on a ReachOutOrder
    /// </summary>
    public class OrderItem
    {
        public OrderItem()
        {
            KitLines = new List<KitLine>();

        }
        /// <summary>
        /// SKU (or item code) used in the ReachOut.cloud partner warehouse
        /// </summary>
        [Required]
        [JsonProperty]
        public string Sku { get; set; }

        /// <summary>
        /// Optional name of the product
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// Optional price for the line
        /// </summary>
        [UIHint("LC")]
        [JsonProperty]
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity for this line item
        /// </summary>
        [Range(1, 10000)]
        [JsonProperty]
        public int Quantity { get; set; }

        /// <summary>
        /// Type of item on this order (0=Product,2=Shipping,3=Tax)
        /// </summary>
        [Range(0, 3)]
        [JsonProperty]
        public int OrderItemTypeId { get; set; }

        [UIHint("LC")]
        /// <summary>
        /// Total price for this line item
        /// </summary>
        [JsonProperty]
        public decimal Total { get; set; }
        [UIHint("LC")]
        /// <summary>
        /// Tax Amount for this line item
        /// </summary>
        [JsonProperty]
        public decimal TaxAmount { get; set; }
        /// <summary>
        /// Kit lines for this item if type is Kit
        /// </summary>
        [JsonProperty]
        public List<KitLine> KitLines { get; set; }

        /// <summary>
        /// Gets or sets the entity attribute values.
        /// </summary>
        /// <value>The entity attribute values.</value>
        public Dictionary<string, string> EntityAttributeValues { get; set; }
    }
}
