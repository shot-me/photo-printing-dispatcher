using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace photo_printing_dispatcher
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"[{DateTime.Now}] Photo printing dispatcher started");
            
            var configuration = BuildConfiguration();
            var pollingInterval = int.Parse(configuration["DatabasePollingIntervalInSeconds"]);
            
            var databaseManager = new DatabaseManager(configuration.GetConnectionString("m1217_cola19"));
            var printManager = new PrintManager(configuration["FullPathToPrinterExeFile"]);
            
            while (true)
            {
                var notPrintedPhotos = databaseManager.GetAllPhotos();

                foreach (var photo in notPrintedPhotos)
                {
                    printManager.Print(photo.TransformedPhotoUrl);
                    photo.DatePrinted = DateTime.Now;
                    databaseManager.UpdatePhoto(photo);
                }
                
                Console.WriteLine($"[{DateTime.Now}] Sleeping for {pollingInterval} seconds");
                Thread.Sleep(TimeSpan.FromSeconds(pollingInterval));
            }
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            
            var configuration = builder.Build();
            return configuration;
        }
    }
}