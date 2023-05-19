
/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/

using DatabaseAccess;
using StormTechnologies.Entities.Dto;
using System.Data;

namespace StormTechnologies.Repository
{
    /// <summary>
    /// Repository for invoice data access.
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        #region Fields and Properties
        private readonly Invoices invoicesDataStore;
        #endregion

        #region C'tor
        /// <summary>
        /// Repository for invoice data.
        /// Injected with StormTechnologies.DataStore.Invoices data store.
        /// </summary>
        /// <param name="invoicesDataStore">Storm Technologies data access dll for test</param>
        public InvoiceRepository(Invoices invoicesDataStore)
        {
            this.invoicesDataStore = invoicesDataStore;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Obtains all the invoices from the data store
        /// </summary>
        /// <param name="populateChildNodes">Flag to indicate to if you wish to populate invoice with all child data (nodes). 
        /// TRUE = Populate all child POCOs, FALSE (default) = Do not populate child node POCOs</param>
        /// <returns>List of Invoice Entities</returns>
        public List<Invoice>? GetAllInvoices(bool populateChildNodes = false)
        {
            try
            {
                List<Invoice>? invoices = null;
                invoices = GetAllInvoices();

                if (invoices is not null && invoices.Any() && populateChildNodes)
                {
                    foreach (Invoice? invoice in invoices)
                    {
                        // Get the invoice items for this invoice
                        if (invoice is not null)
                        {
                            try
                            {
                                invoice.InvoiceItems = GetInvoiceItemsByInvoiceId(invoice.Id);
                                invoice.DeliveryAddress = GetDeliveryAddressBySalesOrderId(invoice.SalesOrderId);
                                invoice.BillingAddress = GetBillingAddressByInvoiceId(invoice.Id);
                                Console.WriteLine($"Invoice entity {invoice.Id}, populated with {invoice?.InvoiceItems?.Count} invoice items.");
                            }
                            catch (Exception)
                            {
                                //As we are iterating through a list of invoices, we don't want to stop the process if one fails.
                                //The data access dll throws an exception if no data is found, so ignore this exception.

                                //TODO: Do some logging here
                            }
                        }
                    }
                }

                return invoices;
            }
            catch (Exception)
            {
                //TODO: Log error to a logger describing non-sensitive parameters that were used in the call
                throw;
            }
        }

        /// <summary>
        /// Obtains all the invoiceItems from the data.
        /// Note: I would normally use a paging mechanism to prevent large data sets being returned.
        /// </summary>
        /// <returns>List of Invoice Entities</returns>
        public List<Invoice>? GetAllInvoices()
        {
            try
            {
                //Treat Storm Technologies data access dll as a data of data
                DataTable data = this.invoicesDataStore.GetNewInvoices();

                List<Invoice>? invoices = null;
                if (data is not null)
                {
                    //Map the enties to POCOs
                    invoices = data.MapToMultipleEntity<Invoice>();
                }

                Console.WriteLine($"Getting all invoices. Total invoices: {invoices?.Count}.");
                return invoices;
            }
            catch (Exception)
            {
                //TODO: Log error to a logger describing non-sensitive parameters that were used in the call
                throw;
            }
        }

        /// <summary>
        /// Obtain single invoice from the data by invoice Id.        
        /// </summary>
        /// <param name="invoiceId">InvoiceId of the Invoice to retrieve</param>
        /// <returns>Single Invoice entity or NULL if not found</returns>
        public Invoice? GetInvoiceByInvoiceId(int invoiceId)
        {
            try
            {
                //Treat Storm Technologies data access dll as a data of data
                DataTable data = this.invoicesDataStore.GetNewInvoices();

                Invoice? invoice = null;
                if (data is not null)
                {
                    //Map the enties to POCOs
                    var invoices = data.MapToMultipleEntity<Invoice>();
                    if (invoices is not null)
                    {
                        //find the invoice that matches the invoiceId
                        invoice = invoices.Where(i => i.Id == invoiceId).FirstOrDefault();
                    }
                }

                Console.WriteLine($"Get single invoice of InvoiceId:{invoiceId}.");
                return invoice;
            }
            catch (Exception)
            {
                //TODO: Log error to a logger describing non-sensitive parameters that were used in the call
                throw;
            }
        }

        /// <summary>
        /// Obtains all the invoice items from the data that match the salesOrderId.
        /// </summary>
        /// <param name="invoiceId">InvoiceId of the invoice items to retrieve</param>
        /// <returns>List of InvoiceItem entities or NULL if not found</returns>
        public List<InvoiceItem>? GetInvoiceItemsByInvoiceId(int invoiceId)
        {
            try
            {
                //Treat Storm Technologies data access dll as a data of data
                DataTable data = this.invoicesDataStore.GetItemsOnInvoice(invoiceId);

                List<InvoiceItem>? invoiceItems = null;
                if (data is not null)
                {
                    //Map the enties to POCOs
                    invoiceItems = data.MapToMultipleEntity<InvoiceItem>();
                }
                Console.WriteLine($"Get invoice items for InvoiceId:{invoiceId}. Total invoice items:{invoiceItems?.Count}.");
                return invoiceItems;
            }
            catch (Exception)
            {
                //TODO: Log error to a logger describing non-sensitive parameters that were used in the call
                throw;
            }
        }

        /// <summary>
        /// Obtains the delivery address from the data that matches the SalesOrderId.
        /// </summary>
        /// <param name="salesOrderId">SalesOrderId of the Delivery Address</param>
        /// <returns>Address entity or NULL if not found</returns>
        public Address? GetDeliveryAddressBySalesOrderId(int salesOrderId)
        {
            try
            {
                //Treat Storm Technologies data access dll as a data of data
                DataRow data = this.invoicesDataStore.GetDeliveryAddressForSalesOrder(salesOrderId);

                Address? address = null;
                if (data is not null)
                {
                    //Map the enties to POCOs
                    address = data.MapToSingleEntity<Address>();

                    if(address is not null) address.Id = salesOrderId; //add the salesOrderId as the primary key to the address for completeness
                }

                Console.WriteLine($"Get delivery address for SalesOrderId:{salesOrderId}.");
                return address;
            }
            catch (Exception)
            {
                //TODO: Log error to a logger describing non-sensitive parameters that were used in the call
                throw;
            }
        }

        /// <summary>
        /// Obtains the billing address from the data that matches the InvoiceId.
        /// </summary>
        /// <param name="invoiceId">InvoiceId of the Billing Address</param>
        /// <returns>Address entity or NULL if not found</returns>
        public Address? GetBillingAddressByInvoiceId(int invoiceId)
        {
            try
            {
                //Treat Storm Technologies data access dll as a data of data
                DataRow data = this.invoicesDataStore.GetBillingAddressForInvoice(invoiceId);

                Address? address = null;
                if (data is not null)
                {
                    //Map the enties to POCOs
                    address = data.MapToSingleEntity<Address>();

                    if (address is not null) address.Id = invoiceId; //add the invoiceId as the primary key to the address for completeness
                }

                Console.WriteLine($"Get billing address for InvoiceId:{invoiceId}.");
                return address;
            }
            catch (Exception)
            {
                //TODO: Log error to a logger describing non-sensitive parameters that were used in the call
                throw;
            }
        }
        #endregion
    }
}