/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/
namespace StormTechnologies.Settings
{
    /// <summary>
    /// Grouped settings From, To and Sender CXML elements
    /// </summary>
    public class CXmlInvoiceGeneratorSettings
    {
        public string Domain { get; set; }
        public string Identity { get; set; }
        public string SharedSecret { get; set; }
        public string UserAgent { get; set; }
    }
}