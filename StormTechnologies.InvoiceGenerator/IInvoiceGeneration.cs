/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/
using StormTechnologies.Entities.Dto;

namespace StormTechnologies.InvoicingService
{
    public interface IInvoiceGeneration
    {
        string ComposeInvoice(Invoice invoice);
        int GenerateInvoices();
    }
}