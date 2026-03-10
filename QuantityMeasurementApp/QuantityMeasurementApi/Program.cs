using System;
using QuantityMeasurementService;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the service
            IQuantityMeasurementService appService = new QuantityMeasurementServices();
            
            // Start the application
            appService.InitializeApplication();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}