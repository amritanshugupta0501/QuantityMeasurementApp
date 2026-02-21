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
                // Added Option 3 to showcase the new generic power!
                Console.WriteLine("Select which measurements you want to compare : \n1. Inches to Inches \n2. Feet to Feet \n3. Inches to Feet");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        CompareMeasurements(MeasurementUnit.INCH, MeasurementUnit.INCH);
                        break;
                    case "2":
                        CompareMeasurements(MeasurementUnit.FEET, MeasurementUnit.FEET);
                        break;
                    case "3":
                        CompareMeasurements(MeasurementUnit.INCH, MeasurementUnit.FEET);
                        break;
                    default:
                        Console.WriteLine("Invalid choice input");
                        break;
                }
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

        // A new helper method that makes your switch statement incredibly clean
        private void CompareMeasurements(MeasurementUnit unit1, MeasurementUnit unit2)
        {
            double val1 = GetValue(unit1.ToString());
            var measurement1 = new QuantityMeasurement(val1, unit1);

            double val2 = GetValue(unit2.ToString());
            var measurement2 = new QuantityMeasurement(val2, unit2);

            bool areEqual = measurement1.Equals(measurement2);
            Console.WriteLine($"Are {val1} {unit1} and {val2} {unit2} equal? {areEqual}\n");
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