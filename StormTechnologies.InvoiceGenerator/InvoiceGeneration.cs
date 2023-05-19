/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/
using Microsoft.Extensions.Options;
using StormTechnologies.Entities.Dto;
using StormTechnologies.InvoiceGenerator.CXML;
using StormTechnologies.Repository;
using StormTechnologies.Settings;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace StormTechnologies.InvoicingService
{
    public class InvoiceGeneration : IInvoiceGeneration
    {   
        private const string FILE_NAME_PREFIX = "CXMLInvoice_";
        private readonly CXmlInvoiceGeneratorSettingsContext settings;
        private readonly IInvoiceRepository invoiceRepository;

        #region C'tor
        public InvoiceGeneration(IOptions<CXmlInvoiceGeneratorSettingsContext> appSettings, IInvoiceRepository invoiceRepository)
        {
            this.settings = appSettings.Value;
            this.invoiceRepository = invoiceRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Generates the CXML for all new invoices
        /// </summary>
        /// <returns>Number of invoices created</returns>
        public int GenerateInvoices()
        {
            int totalInvoicesCreated = 0;

            //Set directory to save files to
            string outputDirectory = settings.OutputFilePath;

            //Create directory if it doesn't exist
            Directory.CreateDirectory(settings.OutputFilePath);
            Console.WriteLine($"Output directory for xml files:{settings.OutputFilePath}.");

            //Note: Setting populateChildNodes=true, we can get all invoices and populate all child nodes by uncommenting below line and commenting out the next line.
            //List<Invoice>? invoices = invoiceRepository.GetAllInvoices(populateChildNodes: true);

            //Get all invoices but populate child nodes as we go - not all at once.
            List<Invoice>? invoices = invoiceRepository.GetAllInvoices();
            if (invoices is not null)
            {
                Console.WriteLine();
                foreach (var invoice in invoices)
                {
                    try
                    {
                        invoice.InvoiceItems = invoiceRepository.GetInvoiceItemsByInvoiceId(invoice.Id);
                        invoice.DeliveryAddress = invoiceRepository.GetDeliveryAddressBySalesOrderId(invoice.SalesOrderId);
                        invoice.BillingAddress = invoiceRepository.GetBillingAddressByInvoiceId(invoice.Id);
                        Console.WriteLine($"Invoice entity {invoice.Id}, populated with {invoice?.InvoiceItems?.Count} invoice items.");

                        Console.WriteLine($"Generating cXML for invoice id:{invoice.Id}.");
                        string invoiceXml = ComposeInvoice(invoice);
                        string fileName = $"{FILE_NAME_PREFIX}{DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)}.xml";
                        string fullPath = Path.Combine(outputDirectory, fileName);

                        // Write file using StreamWriter
                        using (StreamWriter writer = new StreamWriter(fullPath))
                        {
                            writer.WriteLine(invoiceXml);
                            Console.WriteLine($"Succesfully output cXML file for invoice id:{invoice.Id}. Filename:{fullPath}.");
                            Console.WriteLine();
                            totalInvoicesCreated++;
                        }
                    }
                    catch (Exception ex)
                    {
                        //TODO: Do some logging here
                        Console.WriteLine($"Error creating CXML Invoice with Id:{invoice.Id}. {ex.Message}");

                        //Swallow error for this invoice and continue to next invoice. In live environment, we would want to throw the exception.
                        //throw;
                    }
                }
            }

            return totalInvoicesCreated;
        }

        /// <summary>
        /// Creates the CXML invoice from the invoice entity by combining the header and request objects
        /// </summary>
        /// <param name="invoice">Invoice entity</param>
        /// <returns>String of XML representing the CXML Invoice</returns>
        public string ComposeInvoice(Invoice invoice)
        {
            CXMLInvoice cxmlInvoice = new()
            {
                PayloadID = settings.PayloadId,
                Timestamp = DateTime.UtcNow,
                Header = CreateHeader(),
                Request = CreateRequest(invoice)
            };

            string xmlString;
            using (var stringWriter = new Utf8StringWriter())
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(CXMLInvoice));
                    XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented };
                    xmlWriter.WriteDocType("cXML", null, @"http://xml.cxml.org/schemas/cXML/1.2/InvoiceDetail.dtd", null);

                    xmlSerializer.Serialize(xmlWriter, cxmlInvoice);
                    xmlString = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n{stringWriter.ToString()}";
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return xmlString;
        }
        #endregion

        #region Private Methods
        private Header CreateHeader()
        {
            return new()
            {
                From = new() { Credential = new() { Domain = settings.From.Domain, Identity = settings.From.Identity } },
                To = new() { Credential = new() { Domain = settings.To.Domain, Identity = settings.To.Identity } },
                Sender = new()
                {
                    Credential = new() { Domain = settings.Sender.Domain, Identity = settings.Sender.Identity, SharedSecret = settings.Sender.SharedSecret },
                    UserAgent = settings.Sender.UserAgent
                }
            };
        }

        private Request CreateRequest(Invoice invoice)
        {
            //Prepare the invoiceDetail orders
            List<InvoiceDetailOrder> invoiceDetailOrders = new();
            int lineNumber = 1;
            foreach (var invoiceItems in invoice.InvoiceItems)
            {
                invoiceDetailOrders.Add(CreateInvoiceDetailOrder(invoiceItems, invoice.VATPercentage, lineNumber++, invoice.CurrencyCode));
            }

            //Get the subTotal of the whole invoice
            decimal subTotal = invoice.InvoiceItems.Sum(x => x.LineTotal);

            return new()
            {
                DeploymentMode = settings.DeploymentMode,
                InvoiceDetailRequest = new()
                {
                    InvoiceDetailRequestHeader = CreateInvoiceDetailRequestHeader(invoice),
                    InvoiceDetailOrder = invoiceDetailOrders,
                    InvoiceDetailSummary = CreateInvoiceDetailSummary(invoice, subTotal)
                }
            };
        }

        private InvoiceDetailRequestHeader CreateInvoiceDetailRequestHeader(Invoice invoice)
        {
            var currentDateTime = DateTime.UtcNow;

            return new()
            {
                InvoiceID = $"{invoice.Id}{currentDateTime:MMM}{currentDateTime:yy}",
                Purpose = settings.Purpose,
                Operation = settings.Operation,
                InvoiceDate = currentDateTime,
                InvoiceDetailLineIndicator = new() { IsAccountingInLine = "yes" },
                InvoicePartner = new() {
                    new ()
                    {
                        Contact = new()
                        {
                            Role = "soldTo",
                            Name = new() {Lang=settings.Language, Text = invoice.DeliveryAddress.ContactName },
                            PostalAddress = new()
                            {
                                Street = new() { invoice.DeliveryAddress.AddrLine1, invoice.DeliveryAddress.AddrLine2 },
                                City = invoice.DeliveryAddress.Town,
                                State = invoice.DeliveryAddress.State,
                                PostalCode = invoice.DeliveryAddress.PostCode,
                                Country = new() { IsoCountryCode = invoice.DeliveryAddress.CountryCode, Text = invoice.DeliveryAddress.Country }
                            },
                        }
                    },
                    new ()
                    {
                        Contact = new()
                        {
                            Role = "billTo",
                            Name = new() {Lang=settings.Language, Text = invoice.BillingAddress.ContactName },
                            PostalAddress = new()
                            {
                                Street = new() { invoice.BillingAddress.AddrLine1, invoice.BillingAddress.AddrLine2 },
                                City = invoice.BillingAddress.Town,
                                State = invoice.BillingAddress.State,
                                PostalCode = invoice.BillingAddress.PostCode,
                                Country = new() { IsoCountryCode = invoice.BillingAddress.CountryCode, Text = invoice.BillingAddress.Country }
                            },
                        }
                    }
                },
                PaymentTerm = new()
                {
                    PayInNumberofDays = invoice.PaymentTermsDays,
                    NetDueDays = invoice.PaymentTermsDays
                },
            };
        }

        private InvoiceDetailOrder CreateInvoiceDetailOrder(InvoiceItem invoiceItem, decimal vatPercentage, int lineNumber, string currencyCode)
        {
            decimal vatRate = vatPercentage / 100;

            return new()
            {
                //Assuming payload id is invoiceitem id
                InvoiceDetailOrderInfo = new() { OrderReference = new() { DocumentReference = new() { PayloadID = invoiceItem.Id } } },
                InvoiceDetailItem = new()
                {
                    InvoiceLineNumber = lineNumber,
                    Quantity = invoiceItem.Quantity,
                    UnitOfMeasure = "EA",
                    UnitPrice = new() { Money = new() { Currency = currencyCode, Amount = invoiceItem.UnitPrice } },
                    InvoiceDetailItemReference = new()
                    {
                        LineNumber = lineNumber,
                        ItemID = new() { SupplierPartID = invoiceItem.StockItemId.ToString() },
                        Description = new() { Lang = "en", Text = invoiceItem.Description },
                        ManufacturerPartID = invoiceItem.PartNo,
                        ManufacturerName = new() { Lang = settings.Language, Text = invoiceItem.Manufacturer },

                    },
                    SubtotalAmount = new() { Money = new() { Currency = currencyCode, Amount = invoiceItem.LineTotal } },
                    NetAmount = new() { Money = new() { Currency = currencyCode, Amount = invoiceItem.LineTotal } },
                    GrossAmount = new() { Money = new() { Currency = currencyCode, Amount = invoiceItem.LineTotal * (1 + vatRate) } },
                }
            };
        }

        private InvoiceDetailSummary CreateInvoiceDetailSummary(Invoice invoice, decimal subTotalAmount)
        {
            string currencyCode = invoice.CurrencyCode;
            return new()
            {
                SubtotalAmount = new() { Money = new() { Currency = currencyCode, Amount = subTotalAmount } },
                //Assuming payload id is invoiceitem id
                Tax = new()
                {
                    Money = new() { Currency = currencyCode, Amount = invoice.VATAmount },
                    Description = new() { Lang = "en", Text = "VAT" },
                    TaxDetail = new()
                    {
                        Category = "VAT",
                        Purpose = "tax",
                        PercentageRate = invoice.VATPercentage,
                        TaxableAmount = new() { Money = new() { Currency = currencyCode, Amount = subTotalAmount } },
                        TaxAmount = new() { Money = new() { Currency = currencyCode, Amount = invoice.VATAmount } },
                        TaxLocation = new() { Lang = settings.Language, Text = "UK" },
                    }
                },
                ShippingAmount = new() { Money = new() { Currency = currencyCode, Amount = 0 } }, //No shipping amount from data store
                GrossAmount = new() { Money = new() { Currency = currencyCode, Amount = invoice.GrossAmount } },
                NetAmount = new() { Money = new() { Currency = currencyCode, Amount = invoice.NetAmount } },
                DueAmount = new() { Money = new() { Currency = currencyCode, Amount = invoice.GrossAmount } },
            };
        }
        #endregion
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.