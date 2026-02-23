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
        private void AddMeasurements(MeasurementUnit unit1, MeasurementUnit unit2)
        {
            double val1 = GetValue(unit1.ToString());
            double val2 = GetValue(unit2.ToString());
            var first = new QuantityMeasurement(val1, unit1);
            var second = new QuantityMeasurement(val2, unit2);

            // ask user for a target unit for the result
            var units = (MeasurementUnit[])Enum.GetValues(typeof(MeasurementUnit));
            Console.WriteLine("Select the unit in which you want the sum to be displayed:");
            for (int i = 0; i < units.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {units[i]}");
            }
            Console.Write("Enter the number for the target unit: ");
            if (!int.TryParse(Console.ReadLine(), out int targetChoice) || 
                targetChoice < 1 || targetChoice > units.Length)
            {
                Console.WriteLine("Invalid target unit choice. Operation cancelled.");
                return;
            }
            var targetUnit = units[targetChoice - 1];

            var sum = first.Add(second, targetUnit);
            Console.WriteLine($"{val1} {unit1} plus {val2} {unit2} equals {sum.ConvertTo(targetUnit)} {targetUnit}\n");
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