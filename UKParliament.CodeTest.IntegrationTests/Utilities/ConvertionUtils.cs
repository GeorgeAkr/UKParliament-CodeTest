using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.IntegrationTests.Utilities
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

        /// <summary>
        /// Deserializes JSON to a Person object.
        /// </summary>
        /// <param name="message">The JSON representation of the Person.</param>
        /// <returns>The Person object or null in case it could not be deserialized.</returns>
        public static PersonViewModel GetPersonViewModel(string message)
        {
            PersonViewModel result = null;
            try
            {
                result = JsonConvert.DeserializeObject<PersonViewModel>(message);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return result;
        }
    }
}