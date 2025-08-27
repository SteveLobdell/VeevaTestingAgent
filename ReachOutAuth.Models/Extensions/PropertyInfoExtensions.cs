using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ReachOutAuth.Models.VeevaPromoMats.API;
using ReachOutAuth.Models.VeevaPromoMats.Data;

namespace ReachOutAuth.Models.Extensions
{
    /// <summary>
    /// Class PropertyInfoExtensions.
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="prop">The property.</param>
        public static void SetPropertyFromJProperty(this PropertyInfo property, object instance, JProperty prop)
        {
            switch (prop.Value.Type)
            {
                case JTokenType.Object:
                    VeevaAPIResponse subQuery = JsonConvert.DeserializeObject<VeevaAPIResponse>(prop.Value.ToString());

                    List<string> list = new List<string>();
                    foreach (JToken token in subQuery.Data)
                    {
                        foreach(JValue v in token.Values())
                        {
                            list.Add(v.ToString());
                        }
                    }

                    property.SetValue(instance, list);
                    break;
                case JTokenType.Array:
                    if(!string.IsNullOrEmpty(prop.Value.ToString()))
                    {
                        property.SetValue(instance, JsonConvert.DeserializeObject(prop.Value.ToString(), property.PropertyType));
                    }
                    break;
                case JTokenType.Property:
                    property.SetValue(instance, JsonConvert.DeserializeObject(prop.Value.ToString(), property.PropertyType));
                    break;
                case JTokenType.String:
                case JTokenType.Uri:

                    try
                    {
                        if (DateTimeOffset.TryParse(prop.Value.ToString(), out DateTimeOffset date))
                        {
                            property.SetValue(instance, prop.Value.ToObject<DateTimeOffset?>(), null);
                        }
                        else if (!string.IsNullOrEmpty(prop.Value.ToString()))
                        {
                            property.SetValue(instance, prop.Value.ToObject<string>(), null);
                        }
                    }
                    catch { }

                    break;
                case JTokenType.Integer:
                    property.SetValue(instance, prop.Value.ToObject<int?>(), null);
                    break;
                case JTokenType.Float:
                    property.SetValue(instance, prop.Value.ToObject<float?>(), null);
                    break;
                case JTokenType.Boolean:
                    property.SetValue(instance, prop.Value.ToObject<bool?>(), null);
                    break;
                case JTokenType.Date:
                    property.SetValue(instance, prop.Value.ToObject<DateTimeOffset?>(), null);
                    break;
                case JTokenType.TimeSpan:
                    property.SetValue(instance, prop.Value.ToObject<TimeSpan?>(), null);
                    break;
                case JTokenType.Guid:
                    property.SetValue(instance, prop.Value.ToObject<Guid?>(), null);
                    break;
                default:
                    property.SetValue(instance, null, null);
                    break;
            }
        }
    }
}
