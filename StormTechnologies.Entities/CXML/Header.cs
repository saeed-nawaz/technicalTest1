
/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/

using System.Xml.Serialization;

namespace StormTechnologies.InvoiceGenerator.CXML
{
    [XmlRoot(ElementName = "Header", Namespace = "")]
    public class Header
    {

        [XmlElement(ElementName = "From", Namespace = "")]
        public From From { get; set; }

        [XmlElement(ElementName = "To", Namespace = "")]
        public To To { get; set; }

        [XmlElement(ElementName = "Sender", Namespace = "")]
        public Sender Sender { get; set; }
    }

    [XmlRoot(ElementName = "Credential", Namespace = "")]
    public class Credential
    {

        [XmlElement(ElementName = "Identity", Namespace = "")]
        public string Identity { get; set; }

        [XmlAttribute(AttributeName = "domain", Namespace = "")]
        public string Domain { get; set; }

        [XmlText]
        public string Text { get; set; }

        [XmlElement(ElementName = "SharedSecret", Namespace = "")]
        public string SharedSecret { get; set; }
    }

    [XmlRoot(ElementName = "From", Namespace = "")]
    public class From
    {

        [XmlElement(ElementName = "Credential", Namespace = "")]
        public Credential Credential { get; set; }
    }

    [XmlRoot(ElementName = "To", Namespace = "")]
    public class To
    {

        [XmlElement(ElementName = "Credential", Namespace = "")]
        public Credential Credential { get; set; }
    }

    [XmlRoot(ElementName = "Sender", Namespace = "")]
    public class Sender
    {

        [XmlElement(ElementName = "Credential", Namespace = "")]
        public Credential Credential { get; set; }

        [XmlElement(ElementName = "UserAgent", Namespace = "")]
        public string UserAgent { get; set; }
    }
}