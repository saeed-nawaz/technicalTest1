/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/

using Newtonsoft.Json;
using System.Text.Json;

namespace StormTechnologies.Entities.Dto
{
    /// <summary>
    /// InvoiceItem entity - holds line item data for the invoice
    /// 
    /// Assumption for this technical test:As I know no data will be nullable from the test data, therefore I am not using nullable types such as decimal? etc.
    /// This prevents the need to check for null values with .HasValue or .GetValueOrDefault() in the code for this test, ordinarily I would use nullable types.
    /// </summary>
    [Serializable]
    public class InvoiceItem
    {
        /// <summary>
        /// Id of this invoice line item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of the invoice
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        /// Id of the item
        /// </summary>
        public int StockItemId { get; set; }

        /// <summary>
        /// Manufacturer of the item
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Part number of the item
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        /// Description of the item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("Qty")]
        public int Quantity { get; set; }

        /// <summary>
        /// Price of the item
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Subtotal/linetotal of item
        /// </summary>
        public decimal LineTotal { get; set; }
    }
}