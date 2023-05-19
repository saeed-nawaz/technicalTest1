/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/

using System.Xml.Serialization;

namespace StormTechnologies.InvoiceGenerator.CXML
{
    [XmlRoot(ElementName = "DocumentReference", Namespace = "")]
    public class DocumentReference
    {

        [XmlAttribute(AttributeName = "payloadID", Namespace = "")]
        public int PayloadID { get; set; }
    }

    [XmlRoot(ElementName = "OrderReference", Namespace = "")]
    public class OrderReference
    {

        [XmlElement(ElementName = "DocumentReference", Namespace = "")]
        public DocumentReference DocumentReference { get; set; }
    }

    [XmlRoot(ElementName = "InvoiceDetailOrderInfo", Namespace = "")]
    public class InvoiceDetailOrderInfo
    {

        [XmlElement(ElementName = "OrderReference", Namespace = "")]
        public OrderReference OrderReference { get; set; }
    }

    [XmlRoot(ElementName = "UnitPrice", Namespace = "")]
    public class UnitPrice
    {

        [XmlElement(ElementName = "Money", Namespace = "")]
        public Money Money { get; set; }
    }

    [XmlRoot(ElementName = "ItemID", Namespace = "")]
    public class ItemID
    {

        [XmlElement(ElementName = "SupplierPartID", Namespace = "")]
        public string SupplierPartID { get; set; }
    }

    [XmlRoot(ElementName = "ManufacturerName", Namespace = "")]
    public class ManufacturerName
    {

        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "InvoiceDetailItemReference", Namespace = "")]
    public class InvoiceDetailItemReference
    {

        [XmlElement(ElementName = "ItemID", Namespace = "")]
        public ItemID ItemID { get; set; }

        [XmlElement(ElementName = "Description", Namespace = "")]
        public Description Description { get; set; }

        [XmlElement(ElementName = "ManufacturerPartID", Namespace = "")]
        public string ManufacturerPartID { get; set; }

        [XmlElement(ElementName = "ManufacturerName", Namespace = "")]
        public ManufacturerName ManufacturerName { get; set; }

        [XmlAttribute(AttributeName = "lineNumber", Namespace = "")]
        public int LineNumber { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "InvoiceDetailItem", Namespace = "")]
    public class InvoiceDetailItem
    {

        [XmlElement(ElementName = "UnitOfMeasure", Namespace = "")]
        public string UnitOfMeasure { get; set; }

        [XmlElement(ElementName = "UnitPrice", Namespace = "")]
        public UnitPrice UnitPrice { get; set; }

        [XmlElement(ElementName = "InvoiceDetailItemReference", Namespace = "")]
        public InvoiceDetailItemReference InvoiceDetailItemReference { get; set; }

        [XmlElement(ElementName = "SubtotalAmount", Namespace = "")]
        public SubtotalAmount SubtotalAmount { get; set; }

        [XmlElement(ElementName = "GrossAmount", Namespace = "")]
        public GrossAmount GrossAmount { get; set; }

        [XmlElement(ElementName = "NetAmount", Namespace = "")]
        public NetAmount NetAmount { get; set; }

        [XmlAttribute(AttributeName = "invoiceLineNumber", Namespace = "")]
        public int InvoiceLineNumber { get; set; }

        [XmlAttribute(AttributeName = "quantity", Namespace = "")]
        public int Quantity { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "InvoiceDetailOrder", Namespace = "")]
    public class InvoiceDetailOrder
    {

        [XmlElement(ElementName = "InvoiceDetailOrderInfo", Namespace = "")]
        public InvoiceDetailOrderInfo InvoiceDetailOrderInfo { get; set; }

        [XmlElement(ElementName = "InvoiceDetailItem", Namespace = "")]
        public InvoiceDetailItem InvoiceDetailItem { get; set; }
    }


}