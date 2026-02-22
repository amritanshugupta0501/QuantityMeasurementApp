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
                Console.WriteLine("Welcome to Quantity Measurement!");
                Console.WriteLine("Available units:");
                PrintUnits();
                Console.Write("Enter the number for the first unit: ");
                int firstChoice = int.Parse(Console.ReadLine());
                Console.Write("Enter the number for the second unit: ");
                int secondChoice = int.Parse(Console.ReadLine());
                var units = (MeasurementUnit[])Enum.GetValues(typeof(MeasurementUnit));
                if (firstChoice < 1 || firstChoice > units.Length ||
                    secondChoice < 1 || secondChoice > units.Length)
                {
                    Console.WriteLine("Invalid choice input");
                    return;
                }

                CompareMeasurements(units[firstChoice - 1], units[secondChoice - 1]);
            }
            catch (InvalidMeasurementException ex)
            {
                Console.WriteLine($"Measurement Error: {ex.Message}");
            }
            catch (FormatException)
            {
                // specifically catching parse errors from Console.ReadLine()
                Console.WriteLine("Input Error: Please enter a valid number."); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        // helper method that does the work once we know which units the user wants
        private void CompareMeasurements(MeasurementUnit unit1, MeasurementUnit unit2)
        {
            double val1 = GetValue(unit1.ToString());
            var measurement1 = new QuantityMeasurement(val1, unit1);

            double val2 = GetValue(unit2.ToString());
            var measurement2 = new QuantityMeasurement(val2, unit2);

            bool areEqual = measurement1.Equals(measurement2);
            Console.WriteLine($"Are {val1} {unit1} and {val2} {unit2} equal? {areEqual}\n");
        }   

        private void PrintUnits()
        {
            var units = (MeasurementUnit[])Enum.GetValues(typeof(MeasurementUnit));
            for (int i = 0; i < units.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {units[i]}");
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