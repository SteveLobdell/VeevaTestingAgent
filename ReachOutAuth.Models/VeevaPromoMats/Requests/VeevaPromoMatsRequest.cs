using Newtonsoft.Json.Linq;

namespace ReachOutAuth.Models.VeevaPromoMats
{
    /// <summary>
    /// Class VeevaPromoMatsRequest.
    /// </summary>
    public class VeevaPromoMatsRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VeevaPromoMatsRequest"/> class.
        /// </summary>
        public VeevaPromoMatsRequest() 
        { 
            this.Data = new JArray();
        }

        /// <summary>
        /// The client identifier
        /// </summary>
        public string ClientID;

        /// <summary>
        /// The content
        /// </summary>
        public JArray Data;
    }
}
