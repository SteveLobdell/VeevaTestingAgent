using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReachOutAuth.Models.Orders
{
    /// <summary>
    /// The shipping information for an order
    /// </summary>
    public class OrderShipTo : OrderAddress
    {
        /// <summary>
        /// Desired shipping method
        /// </summary>
        [JsonProperty]
        public string ShippingMethod { get; set; }
    }
}
