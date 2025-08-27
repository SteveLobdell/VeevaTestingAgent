using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Filters;

namespace ReachOutAuth.Models.VeevaPromoMats.APIExamples
{
    /// <summary>
    /// Class VeevaPromoMatsDocumentRequestExample.
    /// Implements the <see cref="JObject" />
    /// </summary>
    /// <seealso cref="JObject" />
    public class VeevaPromoMatsDocumentRequestExample : IExamplesProvider<JObject>
    {
        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns>JObject.</returns>
        public JObject GetExamples()
        {
            return JObject.Parse("{   \"clientID\": \"string\",   \"data\":[      {         \"id\": 0,         \"version_id\": \"string\",         \"name__v\": \"string\",         \"status__v\": \"string\",         \"document_number__v\": \"string\",         \"subtype__v\": \"string\",         \"major_version_number__v\": 0,         \"minor_version_number__v\": 0,         \"branding__vs\":[            \"string\"         ],         \"document_product__vr\":{            \"responseDetails\":{               \"pagesize\": 0,               \"pageoffset\": 0,               \"size\": 0,               \"total\": 0            },            \"data\":[               {                  \"name__v\": \"string\"               }            ]         },         \"document_class__cr\":{            \"responseDetails\":{               \"pagesize\": 0,               \"pageoffset\": 0,               \"size\": 0,               \"total\": 0            },            \"data\":[               {                  \"name__v\": \"string\"               }            ]         },         \"prescribing_information_required__vs\": \"boolean\",         \"audience__vs\":[            \"string\"         ],         \"field_use__vs\": \"boolean\",         \"internal_users__c\":[            \"string\"         ],         \"method_of_delivery__vs\":[            \"string\"         ],         \"fulfillment_fgs__c\": \"boolean\",         \"expiration_date__vs\": \"string\",         \"requires_fdm__c\": \"boolean\",         \"thumbnail\": \"string\" ,  \"objective__c\": \"string\",  \"fulfillment_sku__c\": \"string\",  \"appropriate_use__c\": \"string\"  }   ]}");
        }
    }
}
