/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/
using System.Xml.Serialization;

namespace StormTechnologies.InvoiceGenerator.CXML
{
    [XmlRoot(ElementName = "Request", Namespace = "")]
    public class Request
    {
                [XmlElement(ElementName = "InvoiceDetailRequest", Namespace = "")]
        public InvoiceDetailRequest InvoiceDetailRequest { get; set; }

        [XmlAttribute(AttributeName = "deploymentMode", Namespace = "")]
        public string DeploymentMode { get; set; }

    }

    [XmlRoot(ElementName = "InvoiceDetailRequest", Namespace = "")]
    public class InvoiceDetailRequest
    {
        [XmlElement(ElementName = "InvoiceDetailRequestHeader", Namespace = "")]
        public InvoiceDetailRequestHeader InvoiceDetailRequestHeader { get; set; }

        [XmlElement(ElementName = "InvoiceDetailOrder", Namespace = "")]
        public List<InvoiceDetailOrder> InvoiceDetailOrder { get; set; }

        [XmlElement(ElementName = "InvoiceDetailSummary", Namespace = "")]
        public InvoiceDetailSummary InvoiceDetailSummary { get; set; }
    }
}