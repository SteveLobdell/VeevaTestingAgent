using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The API namespace.
/// </summary>
namespace ReachOutVeevaPromoMats.Models.API
{
    /// <summary>
    /// Class to manage responses from the Veeva API
    /// </summary>
    public class VeevaAPIResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VeevaAPIResponse" /> class.
        /// </summary>
        public VeevaAPIResponse() 
        { 
        }

        /// <summary>
        /// The response status
        /// </summary>
        [JsonProperty("responseStatus")]
        public string ResponseStatus;

        /// <summary>
        /// The session identifier
        /// </summary>
        [JsonProperty("sessionId")]
        public string SessionID;

        /// <summary>
        /// The response details
        /// </summary>
        [JsonProperty("responseDetails")]
        public VeevaAPIResponseDetails ResponseDetails;

        /// <summary>
        /// The response details
        /// </summary>
        [JsonProperty("data")]
        public JArray Data;

        /// <summary>
        /// The errors
        /// </summary>
        [JsonProperty("errors")]
        public List<VeevaAPIResponseError> Errors;
    }
}
