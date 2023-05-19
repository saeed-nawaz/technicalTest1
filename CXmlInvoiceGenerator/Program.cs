using DatabaseAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StormTechnologies.InvoicingService;
using StormTechnologies.Repository;
using StormTechnologies.Settings;

namespace CXmlInvoiceGenerator
{
    /// <summary>
    /// Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
    /// 
    /// Application that process data from a datastore (data access dll in this case) and generates CXML files.
    /// </summary>
    internal class Program
    {
        #region Main
        static int Main(string[] args)
        {
            Console.WriteLine("************************************************************************");
            Console.WriteLine("*                Storm Technologies Technical Test                     *");
            Console.WriteLine("*                ---------------------------------                     *");
            Console.WriteLine("*             Code Written by Saeed Nawaz (May-2023)                   *");
            Console.WriteLine("************************************************************************");
            Console.WriteLine("");
            Console.WriteLine("CXmlInvoiceGenerator Application start...");
            Console.WriteLine("");

            int resultCode = -1; //Set a default error code

            //Inject dependencies
            IHostBuilder builder = BuildHost(args);

            //Start the cXML invoice generation
            resultCode = GenerateCXMLForNewInvoices(builder);

            Console.WriteLine("************  *CXmlInvoiceGenerator Application END  ********************");
            Console.WriteLine("Press any to close this window.");
            Console.ReadKey();

            return resultCode;
        }
        #endregion

        #region Private methods
        private static int GenerateCXMLForNewInvoices(IHostBuilder builder)
        {
            int resultCode = -1; //Set a default error code
            var host = builder.Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var invoiceService = services.GetRequiredService<IInvoiceGeneration>();
                    resultCode = invoiceService.GenerateInvoices();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Occured: {ex.Message}.");
                }
            }

            return resultCode;
        }

        private static IHostBuilder BuildHost(string[] args)
        {
            try
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args)
                    .Build();

                return new HostBuilder()
                     .ConfigureAppConfiguration((hostingContext, config) =>
                     {
                         config.SetBasePath(Path.Combine(AppContext.BaseDirectory));
                         config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                     })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.Configure<CXmlInvoiceGeneratorSettingsContext>(Configuration.GetSection("CXmlInvoiceGenerator"));
                        services.AddSingleton<IInvoiceGeneration, InvoiceGeneration>();
                        services.AddSingleton<Invoices>();
                        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
                    }).UseConsoleLifetime();

            }
            catch (Exception)
            {
                //TODO: Do some logging here
                throw;
            }

        }
        #endregion
    }
}