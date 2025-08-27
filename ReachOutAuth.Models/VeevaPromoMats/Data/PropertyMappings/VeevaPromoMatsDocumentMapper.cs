using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ReachOutAuth.Models.VeevaPromoMats.Data.PropertyMappings
{
    public class VeevaPromoMatsDocumentMapper
    {
        /// <summary>
        /// Gets the client document mapping.
        /// </summary>
        /// <param name="clientID">The client identifier.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public static Dictionary<string, string> GetClientDocumentMapping(string clientID)
        {
            string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string mappingFile = Path.Combine(location, "VeevaPromoMats", "Data", "PropertyMappings", "Document", $"{clientID}.json");

            if (!File.Exists(mappingFile))
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(mappingFile));
            }
        }
    }
}
