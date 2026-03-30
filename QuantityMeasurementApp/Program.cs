using System;
using QuantityMeasurementService;
using QuantityMeasurementRepository;
namespace QuantityMeasurementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the service
            QuantityMeasurementController applicationController = new QuantityMeasurementController();
            // Start the application
            applicationController.InitializeApplication();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}