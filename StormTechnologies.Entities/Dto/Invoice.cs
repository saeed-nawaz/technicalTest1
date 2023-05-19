/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/
namespace StormTechnologies.Entities.Dto
{
    /// <summary>
    /// Invoice entity - main invoice data
    /// 
    /// Assumption for this technical test:As I know no data will be nullable from the test data, therefore I am not using nullable types such as decimal? etc.
    /// This prevents the need to check for null values with .HasValue or .GetValueOrDefault() in the code for this test, ordinarily I would use nullable types.
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Id of the invoice
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of the customer
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Id of the sales order
        /// </summary>
        public int SalesOrderId { get; set; }

        /// <summary>
        /// Currency code e.g. GBP
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Net amount
        /// </summary>
        public decimal NetAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal VATAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal GrossAmount { get; set; }

        /// <summary>
        /// VAT Code to differentiate between VAT rates
        /// </summary>
        public string VATCode { get; set; }

        /// <summary>
        /// Set as decimal and not int as VAT can change in the future with decimal digits e.g. 17.5%
        /// </summary>
        public decimal VATPercentage { get; set; }

        /// <summary>
        /// Payment terms in days
        /// </summary>
        public int PaymentTermsDays { get; set; }

        /// <summary>
        /// Line items associated with the invoice
        /// </summary>
        public List<InvoiceItem>? InvoiceItems { get; set; }

        /// <summary>
        /// Delivery address of the invoice
        /// </summary>
        public Address? DeliveryAddress { get; set; }

        /// <summary>
        /// Billing address of the invoice
        /// </summary>
        public Address? BillingAddress { get; set; }
    }
}