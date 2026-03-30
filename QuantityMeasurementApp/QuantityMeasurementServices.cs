using System;

namespace QuantityMeasurementApp
{
    // Contains methods to check input values and run the basic console interaction with the user.
    public class QuantityMeasurementServices
    {
        // Check that the value is not negative or infinite. If it is invalid, throw our custom exception.
        public void ValidateValue(double checkValue)
        {
            if (double.IsNegative(checkValue) || double.IsInfinity(checkValue))
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
                Console.WriteLine("Type Of Units Available : \n1. Weight\n2. Length");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter 1 or 2.");
                    return;
                }
                if (choice == 1)
                {
                    HandleWeight();
                }
                else if (choice == 2)
                {
                    HandleLength();
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
        private void AddMeasurements(LengthUnit unit1, LengthUnit unit2)
        private void HandleLength()
        {
            Console.WriteLine("1. Compare Units\n2. Add Units");
            var choice = int.Parse(Console.ReadLine());
            Console.WriteLine("\nAvailable Length units:");
            var units = (LengthUnit[])Enum.GetValues(typeof(LengthUnit));
            PrintUnits(units);
            Console.Write("Enter the number for the first unit: ");
            int firstChoice = int.Parse(Console.ReadLine());
            Console.Write("Enter the number for the second unit: ");
            int secondChoice = int.Parse(Console.ReadLine());
            if (firstChoice < 1 || firstChoice > units.Length ||
                secondChoice < 1 || secondChoice > units.Length)
            {
                Console.WriteLine("Invalid choice input");
                return;
            }
            if(choice == 1)
            {
                CompareLengthUnits(units[firstChoice-1], units[secondChoice-1]);
            }
            else if(choice == 2)
            {
                AddLengthMeasurements(units[firstChoice - 1], units[secondChoice - 1]);
            }
            else
            {
                Console.WriteLine("Invalid choice input.");
            }
        }
        private void CompareLengthUnits(LengthUnit unit1, LengthUnit unit2)
        {
            double lenValue1 = GetValue(unit1.ToString());
            double lenValue2 = GetValue(unit2.ToString());
            var firstLength = new QuantityLength(lenValue1,unit1);
            var secondLength = new QuantityLength(lenValue2,unit2);
            bool result = firstLength.Equals(secondLength);
            Console.WriteLine("Are the two units equal? "+ result);
        }
        private void AddLengthMeasurements(LengthUnit unit1, LengthUnit unit2)
        {
            double lengthValue1 = GetValue(unit1.ToString());
            double lengthValue2 = GetValue(unit2.ToString());
            var lengthFirst = new QuantityLength(lengthValue1, unit1);
            var lengthSecond = new QuantityLength(lengthValue2, unit2);
            var units = (LengthUnit[])Enum.GetValues(typeof(LengthUnit));
            Console.WriteLine("\nSelect the unit in which you want the sum to be displayed:");
            PrintUnits(units);
            Console.Write("Enter the number for the target unit: ");
            if (!int.TryParse(Console.ReadLine(), out int targetChoice) || 
                targetChoice < 1 || targetChoice > units.Length)
            {
                Console.WriteLine("Invalid target unit choice. Operation cancelled.");
                return;
            }
            var targetUnit = units[targetChoice - 1];
            var sum = lengthFirst.Add(lengthSecond, targetUnit);
            Console.WriteLine($"\nRESULT: {lengthValue1} {unit1} plus {lengthValue2} {unit2} equals {sum.ConvertTo(targetUnit)} {targetUnit}\n");
        }   
        private void HandleWeight()
        {
            Console.WriteLine("1. Compare Units\n2. Add Units");
            var choice = int.Parse(Console.ReadLine());
            Console.WriteLine("\nAvailable Weight units:");
            var units = (WeightUnit[])Enum.GetValues(typeof(WeightUnit));
            PrintUnits(units);
            Console.Write("Enter the number for the first unit: ");
            int firstChoice = int.Parse(Console.ReadLine());
            Console.Write("Enter the number for the second unit: ");
            int secondChoice = int.Parse(Console.ReadLine());
            if (firstChoice < 1 || firstChoice > units.Length ||
                secondChoice < 1 || secondChoice > units.Length)
            {
                Console.WriteLine("Invalid choice input");
                return;
            }
            if(choice == 1)
            {
                CompareWeightUnits(units[firstChoice - 1],units[secondChoice - 1]);
            }
            else if(choice == 2)
            {
                AddWeightMeasurements(units[firstChoice - 1], units[secondChoice - 1]);
            }
            else
            {
                Console.WriteLine("Invalid choice input");
            }
        }
        private void CompareWeightUnits(WeightUnit unit1, WeightUnit unit2)
        {
            double weightValue1 = GetValue(unit1.ToString());
            double weightValue2 = GetValue(unit2.ToString());
            var firstWeight = new QuantityWeight(weightValue1,unit1);
            var secondWeight = new QuantityWeight(weightValue2,unit2);
            bool result = firstWeight.Equals(secondWeight);
            Console.WriteLine("Are the two weights equal? "+result);
        }
        private void AddWeightMeasurements(WeightUnit unit1, WeightUnit unit2)
        {
            double val1 = GetValue(unit1.ToString());
            double val2 = GetValue(unit2.ToString());
            var first = new QuantityWeight(val1, unit1);
            var second = new QuantityWeight(val2, unit2);
            var units = (WeightUnit[])Enum.GetValues(typeof(WeightUnit));
            Console.WriteLine("\nSelect the unit in which you want the sum to be displayed:");
            PrintUnits(units);
            Console.Write("Enter the number for the target unit: ");
            if (!int.TryParse(Console.ReadLine(), out int targetChoice) || 
                targetChoice < 1 || targetChoice > units.Length)
            {
                Console.WriteLine("Invalid target unit choice. Operation cancelled.");
                return;
            }
            var targetUnit = units[targetChoice - 1];
            var sum = first.Add(second, targetUnit);
            Console.WriteLine($"\nRESULT: {val1} {unit1} plus {val2} {unit2} equals {sum.ConvertTo(targetUnit)} {targetUnit}\n");
        }
        private void PrintUnits<T>(T[] units) where T : Enum
        {
            for (int i = 0; i < units.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {units[i]}");
            }
        }
        public double GetValue(string measurement)
        {
            Console.Write($"Give the {measurement} value: ");
            double value = double.Parse(Console.ReadLine());
            ValidateValue(value);
            return value;
        }
    }
}