using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using ReachOutAuth.Models.Extensions;

namespace ReachOutAuth.Models.VeevaPromoMats.Data.JSONConverters
{
    /// <summary>
    /// Class VeevaPromoMatsDocumentDataConverter.
    /// Implements the <see cref="JsonConverter" />
    /// </summary>
    /// <seealso cref="JsonConverter" />
    public class VeevaPromoMatsDocumentDataConverter : JsonConverter
    {
        /// <summary>
        /// The property mappings
        /// </summary>
        private readonly Dictionary<string, string> PropertyMappings;

        /// <summary>
        /// The types
        /// </summary>
        private readonly Type[] _types;

        /// <summary>
        /// Initializes a new instance of the <see cref="VeevaPromoMatsDocumentDataConverter" /> class.
        /// </summary>
        /// <param name="types">The types.</param>
        public VeevaPromoMatsDocumentDataConverter(params Type[] types)
        {
            _types = types;
            PropertyMappings = this.LoadMappingFile();
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
            PropertyInfo[] dataProperties = typeof(VeevaPromoMatsDocumentData).GetProperties();
            List<VeevaPromoMatsDocumentData> dataList = (List<VeevaPromoMatsDocumentData>)Activator.CreateInstance(objectType);

            var jsonList = JArray.Load(reader);

            foreach (JObject content in jsonList .Children<JObject>())
            {
                VeevaPromoMatsDocumentData data = new VeevaPromoMatsDocumentData();

                foreach (JProperty prop in content.Properties())
                {
                    KeyValuePair<string, string> mappedName = PropertyMappings.FirstOrDefault(y => y.Key == prop.Name);
                    if (mappedName.Value != null)
                    {
                        PropertyInfo property = dataProperties.FirstOrDefault(x => x.Name == mappedName.Value);
                        if (property != null)
                        {
                            property.SetPropertyFromJProperty(data, prop);
                        }
                    }
                }

                dataList.Add(data);
            }

            return dataList;
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
            return _types.Any(t => t == objectType);
        }

        /// <summary>
        /// Loads the mapping file.
        /// </summary>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        private Dictionary<string, string> LoadMappingFile()
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string mappings = File.ReadAllText(Path.Combine(executableLocation, "VeevaPromoMats", "Data", "PropertyMappings", "VeevaPromoMatsDocumentDataMapping.json"));

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(mappings);
        }
    }
}

