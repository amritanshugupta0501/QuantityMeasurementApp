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
                Console.WriteLine("Select which measurement you want to compare : \n1. Inches \n2.Feet");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        double valueInInches1 = GetValue("Inches");
                        var inches1 = new InchesMeasurement(valueInInches1);
                        double valueInInches2 = GetValue("Inches");
                        var inches2 = new InchesMeasurement(valueInInches2);
                        Console.WriteLine("Are the two values equal? "+inches1.Equals(inches2));
                        break;
                    case "2":
                        double valueInFeet1 = GetValue("Feet");
                        var feet1 = new FeetMeasurement(valueInFeet1);
                        double valueInFeet2 = GetValue("Feet");
                        var feet2 = new FeetMeasurement(valueInFeet2);
                        Console.WriteLine("Are the two values equal? " + feet1.Equals(feet2));
                        break;
                    default:
                        Console.WriteLine("Invalid choice input");
                        break;
                }
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
        public double GetValue(string measurement)
        {
            Console.Write($"Give the {measurement} value : ");
            double value = double.Parse(Console.ReadLine());
            ValidateValue(value);
            return value;
        }
    }
}