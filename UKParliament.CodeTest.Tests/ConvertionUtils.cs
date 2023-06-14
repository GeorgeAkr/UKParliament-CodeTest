using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace UKParliament.CodeTest.Tests
{
    public static class ConvertionUtils
    {
        /// <summary>
        /// Serializes objects to JSON.
        /// </summary>
        /// <param name="obj">The object to be serialized.</param>
        /// <param name="formatted">Optional paramater for the JSON's properties to be formatted in lowercase.</param>
        /// <returns>The JSON representation of the input object.</returns>
        public static string GetJson(object obj, bool formatted = false)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            string result = JsonConvert.SerializeObject(obj, serializerSettings);

            if (formatted)
            {
                result = JValue.Parse(result).ToString(Formatting.Indented);
            }
            return result;
        }
    }
}