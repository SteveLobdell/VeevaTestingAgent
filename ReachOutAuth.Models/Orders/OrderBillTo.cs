using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReachOutAuth.Models.Orders
{
    /// <summary>
    /// The billing information for the order.  
    /// </summary>
    public class OrderBillTo : OrderAddress
    {
        /// <summary>
        /// Billing GL Account
        /// </summary>
        [JsonProperty]
        public string GLAccount { get; set; }

        /// <summary>
        /// Billing account classification
        /// </summary>
        [JsonProperty]
        public string AccountClass { get; set; }

        /// <summary>
        /// Billing department
        /// </summary>
        [JsonProperty]
        public string Department { get; set; }
    }
}
