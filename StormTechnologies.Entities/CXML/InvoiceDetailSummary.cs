/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/

using System.Xml.Serialization;

namespace StormTechnologies.InvoiceGenerator.CXML
{
    [XmlRoot(ElementName = "TaxableAmount", Namespace = "")]
    public class TaxableAmount
    {

        [XmlElement(ElementName = "Money", Namespace = "")]
        public Money Money { get; set; }
    }

    [XmlRoot(ElementName = "TaxAmount", Namespace = "")]
    public class TaxAmount
    {

        [XmlElement(ElementName = "Money", Namespace = "")]
        public Money Money { get; set; }
    }

    [XmlRoot(ElementName = "TaxLocation", Namespace = "")]
    public class TaxLocation
    {

        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "TaxDetail", Namespace = "")]
    public class TaxDetail
    {

        [XmlElement(ElementName = "TaxableAmount", Namespace = "")]
        public TaxableAmount TaxableAmount { get; set; }

        [XmlElement(ElementName = "TaxAmount", Namespace = "")]
        public TaxAmount TaxAmount { get; set; }

        [XmlElement(ElementName = "TaxLocation", Namespace = "")]
        public TaxLocation TaxLocation { get; set; }

        [XmlAttribute(AttributeName = "purpose", Namespace = "")]
        public string Purpose { get; set; }

        [XmlAttribute(AttributeName = "category", Namespace = "")]
        public string Category { get; set; }

        [XmlAttribute(AttributeName = "percentageRate", Namespace = "")]
        public decimal PercentageRate { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Tax", Namespace = "")]
    public class Tax
    {

        [XmlElement(ElementName = "Money", Namespace = "")]
        public Money Money { get; set; }

        [XmlElement(ElementName = "Description", Namespace = "")]
        public Description Description { get; set; }

        [XmlElement(ElementName = "TaxDetail", Namespace = "")]
        public TaxDetail TaxDetail { get; set; }
    }

    [XmlRoot(ElementName = "ShippingAmount", Namespace = "")]
    public class ShippingAmount
    {

        [XmlElement(ElementName = "Money", Namespace = "")]
        public Money Money { get; set; }
    }



    [XmlRoot(ElementName = "DueAmount", Namespace = "")]
    public class DueAmount
    {

        [XmlElement(ElementName = "Money", Namespace = "")]
        public Money Money { get; set; }
    }

    [XmlRoot(ElementName = "InvoiceDetailSummary", Namespace = "")]
    public class InvoiceDetailSummary
    {

        [XmlElement(ElementName = "SubtotalAmount", Namespace = "")]
        public SubtotalAmount SubtotalAmount { get; set; }

        [XmlElement(ElementName = "Tax", Namespace = "")]
        public Tax Tax { get; set; }

        [XmlElement(ElementName = "ShippingAmount", Namespace = "")]
        public ShippingAmount ShippingAmount { get; set; }

        [XmlElement(ElementName = "GrossAmount", Namespace = "")]
        public GrossAmount GrossAmount { get; set; }

        [XmlElement(ElementName = "NetAmount", Namespace = "")]
        public NetAmount NetAmount { get; set; }

        [XmlElement(ElementName = "DueAmount", Namespace = "")]
        public DueAmount DueAmount { get; set; }
    }


}