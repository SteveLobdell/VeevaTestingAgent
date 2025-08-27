using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReachOutVeevaPromoMats.Models.API
{
    /// <summary>
    /// Veeva API Response details
    /// </summary>
    public class VeevaAPIResponseDetails
    {
        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        [JsonProperty("pagesize")]
        public int PageSize { get; set; }

        /// <summary>
        /// The page offset
        /// </summary>
        /// <value>The page offset.</value>
        [JsonProperty("pageoffset")]
        public int PageOffset { get; set; }

        /// <summary>
        /// The size
        /// </summary>
        /// <value>The size.</value>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// The total
        /// </summary>
        /// <value>The total.</value>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// The error
        /// </summary>
        /// <value>The next page.</value>
        [JsonProperty("next_page")]
        public string NextPage { get; set; }
    }
}
