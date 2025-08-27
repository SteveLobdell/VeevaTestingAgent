using System;
using System.Collections.Generic;
using System.Text;

namespace ReachOutAuth.Models.Messages
{
    public class ProductResponse
    {
        /// <summary>
        /// A list of OrderResponseMessage objects are returned for each order submitted during the create operation
        /// </summary>
        public List<ProductResponseMessage> ProductResponses { get; set; }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("Product response");
            foreach(ProductResponseMessage message in ProductResponses)
            {
                builder.AppendLine(string.Format("{0}, {1}, {2}, {3}", message.MsgCode, message.Success, message.ProductSku, message.Message));
            }

            return builder.ToString();
        }
    }
}
