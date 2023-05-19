/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/
using System.Text;

namespace StormTechnologies.InvoicingService
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
