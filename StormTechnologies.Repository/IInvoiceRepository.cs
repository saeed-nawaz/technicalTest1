
/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/
using StormTechnologies.Entities.Dto;

namespace StormTechnologies.Repository
{
    /// <summary>
    /// Interface representing the invoice repository
    /// </summary>
    public interface IInvoiceRepository
    {
        List<Invoice>? GetAllInvoices(bool populateChildNodes = false);
        List<Invoice>? GetAllInvoices();
        Address? GetBillingAddressByInvoiceId(int invoiceId);
        Address? GetDeliveryAddressBySalesOrderId(int salesOrderId);
        Invoice? GetInvoiceByInvoiceId(int invoiceId);
        List<InvoiceItem>? GetInvoiceItemsByInvoiceId(int invoiceId);
    }
}