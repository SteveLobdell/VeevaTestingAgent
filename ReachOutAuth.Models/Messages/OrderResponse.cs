using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReachOutAuth.Models.Messages
{
    public class OrderResponse
    {
        /// <summary>
        /// A list of OrderResponseMessage objects are returned for each order submitted during the create operation
        /// </summary>
        public List<OrderResponseMessage> OrderResponses { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("Order response");
            foreach (OrderResponseMessage message in OrderResponses)
            {
                builder.AppendLine(string.Format("{0}, {1}, {2}, {3}", message.MsgCode, message.Success, message.ExternalOrderId, message.Message));
            }

            return builder.ToString();
        }
    }
    public class MarketoOrderResponse
    {
        /// <summary>
        /// A single order number is returned after creating the order
        /// </summary>
        [JsonProperty("OrderNumber")]
        public string OrderNumber { get; set; }
        /// <summary>
        /// A single order number is returned after creating the order
        /// </summary>
        [JsonProperty("Errors")]
        public string Errors { get; set; }
    }
}
