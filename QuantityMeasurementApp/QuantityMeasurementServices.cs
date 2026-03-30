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
        // Ask the user for two measurements and perform the requested operation. Errors are caught and printed.
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
                var u1 = units[firstChoice - 1];
                var u2 = units[secondChoice - 1];
                AddMeasurements(u1, u2);
            }
            catch (InvalidMeasurementException ex)
            {
                Console.WriteLine($"Measurement Error: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Input Error: Please enter a valid number."); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        // A new helper method that makes your switch statement incredibly clean
        // helper method that does the work once we know which units the user wants
        private void CompareMeasurements(MeasurementUnit unit1, MeasurementUnit unit2)
        {
            double val1 = GetValue(unit1.ToString());
            // perform conversion of the first value into the second unit
            double converted = QuantityMeasurement.Convert(val1, unit1, unit2);

            double val2 = GetValue(unit2.ToString());
            var measurement2 = new QuantityMeasurement(val2, unit2);

            bool areEqual = measurement1.Equals(measurement2);
            Console.WriteLine($"Are {val1} {unit1} and {val2} {unit2} equal? {areEqual}\n");
        }        
            Console.WriteLine($"{val1} {unit1} is {converted} {unit2}\n");
        private void AddMeasurements(MeasurementUnit unit1, MeasurementUnit unit2)
        {
            double val1 = GetValue(unit1.ToString());
            double val2 = GetValue(unit2.ToString());
            var first = new QuantityMeasurement(val1, unit1);
            var second = new QuantityMeasurement(val2, unit2);
            var sum = first.Add(second);
            Console.WriteLine($"{val1} {unit1} plus {val2} {unit2} equals {sum.ConvertTo(unit1)} {unit1}\n");
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