/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/

using System.Xml.Serialization;

namespace StormTechnologies.InvoiceGenerator.CXML
{
    [XmlRoot(ElementName = "InvoiceDetailLineIndicator", Namespace = "")]
    public class InvoiceDetailLineIndicator
    {

        [XmlAttribute(AttributeName = "isAccountingInLine", Namespace = "")]
        public string IsAccountingInLine { get; set; }
    }

    [XmlRoot(ElementName = "Name", Namespace = "")]
    public class Name
    {

        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Country", Namespace = "")]
    public class Country
    {
        [XmlAttribute(AttributeName = "isoCountryCode", Namespace = "")]
        public string IsoCountryCode { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "PostalAddress", Namespace = "")]
    public class PostalAddress
    {

        [XmlElement(ElementName = "Street", Namespace = "")]
        public List<string> Street { get; set; }

        [XmlElement(ElementName = "City", Namespace = "")]
        public string City { get; set; }

        [XmlElement(ElementName = "State", Namespace = "")]
        public string State { get; set; }

        [XmlElement(ElementName = "PostalCode", Namespace = "")]
        public string PostalCode { get; set; }

        [XmlElement(ElementName = "Country", Namespace = "")]
        public Country Country { get; set; }
    }

    [XmlRoot(ElementName = "InvoicePartner", Namespace = "")]
    public class InvoicePartner
    {
        [XmlElement(ElementName = "Contact", Namespace = "")]
        public Contact Contact { get; set; }
    }

    [XmlRoot(ElementName = "Contact", Namespace = "")]
    public class Contact
    {
        [XmlElement(ElementName = "Name", Namespace = "")]
        public Name Name { get; set; }

        [XmlElement(ElementName = "PostalAddress", Namespace = "")]
        public PostalAddress PostalAddress { get; set; }

        [XmlAttribute(AttributeName = "role", Namespace = "")]
        public string Role { get; set; }

        [XmlAttribute(AttributeName = "addressID", Namespace = "")]
        public int AddressID { get; set; }
    }

    [XmlRoot(ElementName = "DiscountPercent", Namespace = "")]
    public class DiscountPercent
    {

        [XmlAttribute(AttributeName = "percent", Namespace = "")]
        public int Percent { get; set; }
    }

    [XmlRoot(ElementName = "Discount", Namespace = "")]
    public class Discount
    {

        [XmlElement(ElementName = "DiscountPercent", Namespace = "")]
        public DiscountPercent DiscountPercent { get; set; }

        [XmlElement(ElementName = "DiscountDueDays", Namespace = "")]
        public int DiscountDueDays { get; set; }
    }

    [XmlRoot(ElementName = "PaymentTerm", Namespace = "")]
    public class PaymentTerm
    {

        [XmlElement(ElementName = "Discount", Namespace = "")]
        public Discount Discount { get; set; }

        [XmlElement(ElementName = "NetDueDays", Namespace = "")]
        public int NetDueDays { get; set; }

        [XmlAttribute(AttributeName = "payInNumberofDays", Namespace = "")]
        public int PayInNumberofDays { get; set; }
    }

    [XmlRoot(ElementName = "InvoiceDetailRequestHeader", Namespace = "")]
    public class InvoiceDetailRequestHeader
    {

        [XmlElement(ElementName = "InvoiceDetailHeaderIndicator", Namespace = "")]
        public object InvoiceDetailHeaderIndicator { get; set; }

        [XmlElement(ElementName = "InvoiceDetailLineIndicator", Namespace = "")]
        public InvoiceDetailLineIndicator InvoiceDetailLineIndicator { get; set; }

        [XmlElement(ElementName = "InvoicePartner", Namespace = "")]
        public List<InvoicePartner> InvoicePartner { get; set; }

        [XmlElement(ElementName = "PaymentTerm", Namespace = "")]
        public PaymentTerm PaymentTerm { get; set; }

        [XmlAttribute(AttributeName = "invoiceID", Namespace = "")]
        public string InvoiceID { get; set; }

        [XmlAttribute(AttributeName = "purpose", Namespace = "")]
        public string Purpose { get; set; }

        [XmlAttribute(AttributeName = "operation", Namespace = "")]
        public string Operation { get; set; }

        [XmlAttribute(AttributeName = "invoiceDate", Namespace = "")]
        public DateTime InvoiceDate { get; set; }

        [XmlText]
        public string Text { get; set; }
    }


}