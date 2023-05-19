/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/

using Newtonsoft.Json;

namespace StormTechnologies.Entities.Dto
{
    /// <summary>
    /// Address entity - holds address data
    /// 
    /// Assumption for this technical test:As I know no data will be nullable from the test data, therefore I am not using nullable types such as decimal? etc.
    /// This prevents the need to check for null values with .HasValue or .GetValueOrDefault() in the code for this test, ordinarily I would use nullable types.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Address Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the contact
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Address line 1
        /// </summary>
        public string AddrLine1 { get; set; }

        /// <summary>
        /// Address line 2
        /// </summary>
        public string AddrLine2 { get; set; }

        /// <summary>
        /// Address line 3
        /// </summary>
        [JsonProperty("AddrLine3")]
        public string Town { get; set; }


        /// <summary>
        /// Address line 4
        /// </summary>
        [JsonProperty("AddrLine4")]
        public string State { get; set; }

        /// <summary>
        /// Address line 5 - Not used
        /// </summary>
        public string AddrLine5 { get; set; }

        /// <summary>
        /// Address Post/Zip Code
        /// </summary>
        [JsonProperty("AddrPostCode")]
        public string PostCode { get; set; }

        /// <summary>
        /// Country Code
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Country name
        /// </summary>
        public string Country { get; set; }
    }
}