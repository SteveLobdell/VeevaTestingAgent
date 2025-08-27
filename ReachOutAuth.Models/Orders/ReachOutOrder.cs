 using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ReachOutAuth.Models.Orders
{
    /// <summary>
    /// The schema required to create an order in the platform
    /// </summary>
    [DataContract]
    [Serializable]
    public class ReachOutOrder
    {
        /// <summary>
        /// The Administrator supplied Client identifier used for this integration
        /// </summary>
        [Required]
        [JsonProperty]
        public string ReachOutClientId { get; set; }

        /// <summary>
        /// Your order number
        /// </summary>
        [Required]
        [JsonProperty]
        public string ExternalOrderId { get; set; }

        /// <summary>
        /// Acknowlegement e-mail address (not common)
        /// </summary>
        [JsonProperty]
        public string AckEmail { get; set; }

        /// <summary>
        /// Additional order reference
        /// </summary>
        [JsonProperty]
        public string OrderReference { get; set; }

        /// <summary>
        /// PO Number for the order
        /// </summary>
        [JsonProperty]
        public string PONumber { get; set; }

        /// <summary>
        /// Special comments for the order
        /// </summary>
        [JsonProperty]
        public string Comments { get; set; }

        /// <summary>
        /// Optional date/time of order in source system
        /// </summary>
        [JsonProperty]
        public DateTime? OrderDateTime { get; set; }
        [Required]
        [JsonProperty]
        public OrderShipTo ShipTo { get; set; }

        /// <summary>Gets or sets the bill to.</summary>
        /// <value>The bill to.</value>
        [JsonProperty]
        public OrderBillTo BillTo { get; set; }


        /// <summary>Gets or sets a value indicating whether [create skus].</summary>
        /// <value>true if sku should be created if not exists</value>
        [JsonProperty]
        public bool CreateSkus { get; set; }

        /// <summary>Gets or sets the items.</summary>
        /// <value>The items.</value>
        [Required]
        [JsonProperty]
        public List<OrderItem> Items { get; set; }
        [JsonProperty]
        [UIHint("LC")]
        public decimal ProductSubtotal { get; set; }
        [JsonProperty]
        [UIHint("LC")]
        public decimal TotalCharges { get; set; }
        [JsonProperty]
        [UIHint("LC")]
        public decimal TotalPayments { get; set; }
        [JsonProperty]
        [UIHint("LC")]
        public decimal TotalTax { get; set; }
        [JsonProperty]
        [UIHint("LC")]
        public decimal TotalShipping { get; set; }
        [JsonProperty]
        [UIHint("LC")]
        public decimal BalanceDue { get; set; }

        /// <summary>Initializes a new instance of ReachOutOrder /> class.</summary>
        public ReachOutOrder()
        {
            this.ShipTo = new OrderShipTo();
            this.BillTo = new OrderBillTo();
            this.Items = new List<OrderItem>();
        }
    }
}
