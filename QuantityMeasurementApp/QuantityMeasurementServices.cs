using System;

namespace QuantityMeasurementApp
{
    public class QuantityMeasurementServices
    {
        // Checks if a measurement value is valid (not negative or infinity) before processing.
        public void ValidateValue(double checkValue)
        {
            if (double.IsNegative(checkValue) || double.IsInfinity(checkValue))
            {
                throw new InvalidMeasurementException($"The measurement value {checkValue} is invalid.");
            }
        }
        // Entry point that displays the main menu and directs the user to Weight, Length, or Volume categories.
        public void InitializeApplication()
        {
            try
            {
                Console.WriteLine("Welcome to Quantity Measurement!");
                Console.WriteLine("Type Of Units Available : \n1. Weight\n2. Length\n3. Volume");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter 1 or 2.");
                    return;
                }
                if (choice == 1)
                {
                    HandleMeasurement<WeightUnit>("Weight", WeightConverter.Instance);
                }
                else if (choice == 2)
                {
                    HandleMeasurement<LengthUnit>("Length", LengthConverter.Instance);
                }
                else if (choice == 3)
                {
                    HandleMeasurement<VolumeUnit>("Volume", VolumeConverter.Instance);
                }
                else
                {
                    Console.WriteLine("Invalid choice input");
                }
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
        // Manages the shared workflow for a category: showing available units, getting user selections, and choosing an operation.
        private void HandleMeasurement<TUnit>(string categoryName, IUnitConverter<TUnit> converter) where TUnit : Enum
        {
            Console.WriteLine("1. Compare Units\n2. Add Units\n3. Subtract Units\n4. Divide Units");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                return;
            }
            Console.WriteLine($"\nAvailable {categoryName} units:");
            var units = (TUnit[])Enum.GetValues(typeof(TUnit));
            PrintUnits(units);

            Console.Write("Enter the number for the first unit: ");
            int firstChoice = int.Parse(Console.ReadLine());
            Console.Write("Enter the number for the second unit: ");
            int secondChoice = int.Parse(Console.ReadLine());

            if (firstChoice < 1 || firstChoice > units.Length || secondChoice < 1 || secondChoice > units.Length)
            {
                Console.WriteLine("Invalid choice input");
                return;
            }

            TUnit unit1 = units[firstChoice - 1];
            TUnit unit2 = units[secondChoice - 1];

            if (choice == 1)
            {
                CompareUnits(unit1, unit2, converter);
            }
            else if (choice == 2)
            {
                AddMeasurements(unit1, unit2, converter);
            }
            else if (choice == 3)
            {
                SubtractMeasurements(unit1, unit2, converter);
            }
            else if (choice == 4)
            {
                DivisionOfMeasurements(unit1, unit2, converter);
            }
        }
        // Handles the comparison logic by creating Quantity objects and checking if they represent the same physical magnitude.
        private void CompareUnits<TUnit>(TUnit unit1, TUnit unit2, IUnitConverter<TUnit> converter) where TUnit : Enum
        {
            double val1 = GetValue(unit1.ToString());
            double val2 = GetValue(unit2.ToString());

            // Creating the Quantity objects using the generic unit and converter
            var first = new Quantity<TUnit>(val1, unit1, converter);
            var second = new Quantity<TUnit>(val2, unit2, converter);

            bool result = first.Equals(second);
            Console.WriteLine($"Are the two units equal? {result}");
        }
        // Handles the addition logic by creating Quantity objects and calculating their sum in a user-specified target unit.
        private void AddMeasurements<TUnit>(TUnit unit1, TUnit unit2, IUnitConverter<TUnit> converter) where TUnit : Enum
        {
            double val1 = GetValue(unit1.ToString());
            double val2 = GetValue(unit2.ToString());

            var first = new Quantity<TUnit>(val1, unit1, converter);
            var second = new Quantity<TUnit>(val2, unit2, converter);

            var units = (TUnit[])Enum.GetValues(typeof(TUnit));
            Console.WriteLine("\nSelect the target unit for the sum:");
            PrintUnits(units);

            if (int.TryParse(Console.ReadLine(), out int targetChoice) && targetChoice >= 1 && targetChoice <= units.Length)
            {
                TUnit targetUnit = units[targetChoice - 1];
                var sumQuantity = first.Add(second, targetUnit);

                Console.WriteLine($"\nRESULT: {val1} {unit1} + {val2} {unit2} = {sumQuantity.ConvertTo(targetUnit)} {targetUnit}\n");
            }
        }
        // // Handles the subtraction logic by creating Quantity objects and calculating their difference in a user-specified target unit.
        private void SubtractMeasurements<TUnit>(TUnit unit1, TUnit unit2, IUnitConverter<TUnit> converter) where TUnit : Enum
        {
            double val1 = GetValue(unit1.ToString());
            double val2 = GetValue(unit2.ToString());

            var first = new Quantity<TUnit>(val1, unit1, converter);
            var second = new Quantity<TUnit>(val2, unit2, converter);

            var units = (TUnit[])Enum.GetValues(typeof(TUnit));
            Console.WriteLine("\nSelect the target unit for the sum:");
            PrintUnits(units);

            if (int.TryParse(Console.ReadLine(), out int targetChoice) && targetChoice >= 1 && targetChoice <= units.Length)
            {
                TUnit targetUnit = units[targetChoice - 1];
                var sumQuantity = first.Subtract(second, targetUnit);

                Console.WriteLine($"\nRESULT: {val1} {unit1} + {val2} {unit2} = {sumQuantity.ConvertTo(targetUnit)} {targetUnit}\n");
            }
        }
        // Handles the division logic by creating Quantity objects and calculating their quotient in a user-specified target unit.
        private void DivisionOfMeasurements<TUnit>(TUnit unit1, TUnit unit2, IUnitConverter<TUnit> converter) where TUnit : Enum
        {
            double val1 = GetValue(unit1.ToString());
            double val2 = GetValue(unit2.ToString());

            var first = new Quantity<TUnit>(val1, unit1, converter);
            var second = new Quantity<TUnit>(val2, unit2, converter);

            var units = (TUnit[])Enum.GetValues(typeof(TUnit));
            Console.WriteLine("\nSelect the target unit for the sum:");
            PrintUnits(units);

            if (int.TryParse(Console.ReadLine(), out int targetChoice) && targetChoice >= 1 && targetChoice <= units.Length)
            {
                TUnit targetUnit = units[targetChoice - 1];
                var sumQuantity = first.Division(second, targetUnit);

                Console.WriteLine($"\nRESULT: {val1} {unit1} + {val2} {unit2} = {sumQuantity.ConvertTo(targetUnit)} {targetUnit}\n");
            }
        }
        // Helper method that iterates through any Unit Enum to display a numbered list of choices to the user.
        private void PrintUnits<T>(T[] units) where T : Enum
        {
            for (int i = 0; i < units.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {units[i]}");
            }
        }
        // Prompts the user for a numerical input, parses it, and runs the validation check.
        public double GetValue(string measurement)
        {
            Console.Write($"Give the {measurement} value: ");
            double value = double.Parse(Console.ReadLine());
            ValidateValue(value);
            return value;
        }
    }
}