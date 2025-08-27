using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReachOutVeevaPromoMats.Models.API
{
    /// <summary>
    /// Error from the Veeva API
    /// </summary>
    public class VeevaAPIResponseError
    {
        /// <summary>
        /// The type
        /// </summary>
        [JsonProperty("type")]
        public string Type;

        /// <summary>
        /// The message
        /// </summary>
        [JsonProperty("message")]
        public string Message;
    }
}
