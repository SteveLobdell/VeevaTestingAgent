using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReachOutAuth.Models.Messages
{
    /// <summary>
    /// A response containing success or error messages for submitted orders  
    /// </summary>
    public class OrderResponseMessage : ResponseMessage
    {
        /// <summary>
        /// The originating ExternalOrderId
        /// </summary>
        [Required]
        public string ExternalOrderId { get; set; }

        /// <summary>
        /// The OrderId created in ReachOut.cloud (returns zero on failure)
        /// </summary>
        public int OrderId { get; set; }
    }
}
