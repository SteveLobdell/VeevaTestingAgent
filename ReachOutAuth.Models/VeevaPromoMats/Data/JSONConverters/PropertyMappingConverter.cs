using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReachOutAuth.Models.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

/// <summary>
/// The JSONConverters namespace.
/// </summary>
namespace ReachOutAuth.Models.VeevaPromoMats.Data.JSONConverters
{
    /// <summary>
    /// Class PropertyMappingConverter.
    /// Implements the <see cref="JsonConverter" />
    /// </summary>
    /// <seealso cref="JsonConverter" />
    public class PropertyMappingConverter : JsonConverter
    {
        /// <summary>
        /// Gets the property mappings.
        /// </summary>
        /// <value>The property mappings.</value>
        private Dictionary<string, string> PropertyMappings { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMappingConverter"/> class.
        /// </summary>
        /// <param name="propertyMappings">The property mappings.</param>
        public PropertyMappingConverter(Dictionary<string, string> propertyMappings)
        {
            PropertyMappings = propertyMappings;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            PropertyInfo[] dataProperties = objectType.GetProperties();
            object returnObject = Activator.CreateInstance(objectType);

            try
            {
                JObject jsonObject = JObject.Load(reader);

                foreach (JProperty prop in jsonObject.Properties())
                {
                    KeyValuePair<string, string> mappedName = PropertyMappings.FirstOrDefault(y => y.Key == prop.Name);
                    if (mappedName.Value != null)
                    {
                        PropertyInfo property = dataProperties.FirstOrDefault(x => x.Name == mappedName.Value);
                        if (property != null)
                        {
                            try
                            {
                                property.SetPropertyFromJProperty(returnObject, prop);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return returnObject;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON.
        /// </summary>
        /// <value><c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON; otherwise, <c>false</c>.</value>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON.
        /// </summary>
        /// <value><c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON; otherwise, <c>false</c>.</value>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}

