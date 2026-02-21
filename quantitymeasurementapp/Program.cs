using System;

namespace QuantityMeasurementApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            QuantityMeasurementServices measurementServices = new QuantityMeasurementServices();
            measurementServices.InitializeApplication();
        }
    }
}