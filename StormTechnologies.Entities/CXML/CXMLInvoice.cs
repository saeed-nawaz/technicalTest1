/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/

using System.Xml.Serialization;

namespace StormTechnologies.InvoiceGenerator.CXML
{
    [XmlRoot(ElementName = "cXML")]
    public class CXMLInvoice
    {

        [XmlElement(ElementName = "Header", Namespace = "")]
        public Header Header { get; set; }

        [XmlElement(ElementName = "Request", Namespace = "")]
        public Request Request { get; set; }

        [XmlAttribute(AttributeName = "version", Namespace = "")]
        public double Version { get; set; }

        [XmlAttribute(AttributeName = "payloadID", Namespace = "")]
        public string PayloadID { get; set; }

        [XmlAttribute(AttributeName = "timestamp", Namespace = "")]
        public DateTime Timestamp { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Money", Namespace = "")]
    public class Money
    {
        [XmlAttribute(AttributeName = "currency", Namespace = "")]
        public string Currency { get; set; }

        [XmlText]
        public decimal Amount { get; set; }
    }

    [XmlRoot(ElementName = "SubtotalAmount", Namespace = "")]
    public class SubtotalAmount
    {

        [XmlElement(ElementName = "Money", Namespace = "")]
        public Money Money { get; set; }
    }

    [XmlRoot(ElementName = "Description", Namespace = "")]
    public class Description
    {
        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "NetAmount", Namespace = "")]
    public class NetAmount
    {

        [XmlElement(ElementName = "Money", Namespace = "")]
        public Money Money { get; set; }
    }

    [XmlRoot(ElementName = "GrossAmount", Namespace = "")]
    public class GrossAmount
    {

        [XmlElement(ElementName = "Money", Namespace = "")]
        public Money Money { get; set; }
    }

}