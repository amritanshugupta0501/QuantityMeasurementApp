using System;

namespace QuantityMeasurementApp
{
    // Starting point of the console application.
    public class Program
    {
        static void Main(string[] args)
        {
            // Create the services helper and start asking the user for
            // input. The program logic lives in QuantityMeasurementServices.
            QuantityMeasurementServices measurementServices = new QuantityMeasurementServices();
            measurementServices.InitializeApplication();
        }
    }
}