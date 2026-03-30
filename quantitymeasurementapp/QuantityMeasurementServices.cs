using System;

namespace QuantityMeasurementApp
{
    // Contains methods to check input values and run the basic console interaction with the user.
    public class QuantityMeasurementServices
    {
        // Check that the value is not negative or infinite. If it is invalid, throw our custom exception.
        public void ValidateValue(double checkValue)
        {
            if(double.IsNegative(checkValue) || double.IsInfinity(checkValue))
            {
                throw new InvalidMeasurementException($"The measurement value {checkValue} is invalid.");
            }
        }
        // Ask the user for two measurements, compare them, and report whether they are equal. Errors are caught and printed.
        public void InitializeApplication()
        {
            try
            {
                Console.Write("Give value in Feet : ");
                double valueInFeet = double.Parse(Console.ReadLine());
                ValidateValue(valueInFeet);
                FeetMeasurement feetValue1 = new FeetMeasurement(valueInFeet);
                Console.Write("Give 2nd value in Feet : ");
                valueInFeet = double.Parse(Console.ReadLine());
                FeetMeasurement feetValue2 = new FeetMeasurement(valueInFeet);
                Console.WriteLine("Are the two measurements equal ? " + feetValue1.Equals(feetValue2));
            }
            catch (InvalidMeasurementException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}