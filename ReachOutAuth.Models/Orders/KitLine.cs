using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReachOutAuth.Models.Orders
{
    /// <summary>
    /// Represents a kit component line on an order
    /// </summary>
    public class KitLine
    {
        /// <summary>
        /// Child SKU represents a component inside a kit
        /// </summary>
        [Required]
        [JsonProperty]
        public string Sku { get; set; }

        /// <summary>
        /// Quantity of this component for this kit
        /// </summary>
        [Range(1, 10000)]
        [JsonProperty]
        public int Quantity { get; set; }

        /// <summary>
        /// Sequence to place the component inside the kit
        /// </summary>
        [JsonProperty]
        public int Sequence { get; set; }
    }
}
