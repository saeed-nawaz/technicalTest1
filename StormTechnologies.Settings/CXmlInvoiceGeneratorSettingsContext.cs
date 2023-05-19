/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/
namespace StormTechnologies.Settings
{
    /// <summary>
    /// Settings for CXML invoice generator
    /// </summary>
    public class CXmlInvoiceGeneratorSettingsContext
    {
        public string OutputFilePath { get; set; }
        public string PayloadId { get; set; }
        public string DeploymentMode { get; set; }
        public string Operation { get; set; }
        public string Purpose { get; set; }
        public string Language { get; set; }

        public CXmlInvoiceGeneratorSettings From { get; set; }
        public CXmlInvoiceGeneratorSettings To { get; set; }
        public CXmlInvoiceGeneratorSettings Sender { get; set; }
    }
}