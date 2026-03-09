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
                Console.WriteLine("Type Of Units Available : \n1. Weight\n2. Length\n3. Volume\n4. Temperature");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid Choice input");
                    return;
                }
                switch (choice)
                {
                    case 1:
                        HandleMeasurementCategory<WeightUnit>("Weight", WeightConverter.Instance); 
                        break;
                    case 2: 
                        HandleMeasurementCategory<LengthUnit>("Length", LengthConverter.Instance); 
                        break;
                    case 3:
                        HandleMeasurementCategory<VolumeUnit>("Volume", VolumeConverter.Instance);
                        break;
                    case 4:
                        HandleMeasurementCategory<TemperatureUnit>("Volume", TemperatureConverter.Instance);
                        break;
                    default:
                        Console.WriteLine("Invalid choice"); 
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        // Manages the shared workflow for a category: showing available units, getting user selections, and choosing an operation.
        private void HandleMeasurementCategory<TUnit>(string name, IMeasurable<TUnit> converter) where TUnit : Enum
        {
            // Identify if the measurement unit selected is temperature or not
            bool isTemperature = typeof(TUnit) == typeof(TemperatureUnit);
            Console.WriteLine($"\n{name} Operations");
            Console.WriteLine("1. Compare\n2. Add\n3. Subtract");
            if (!isTemperature)
            {
                Console.WriteLine("4. Divide");
            }
            Console.Write("\nSelect Operation: ");
            if (!int.TryParse(Console.ReadLine(), out int opChoice))
            {
                return;
            } 
            if (isTemperature && opChoice == 4)
            {
                throw new InvalidMeasurementException("Temperature values cannot be divided. This operation is physically nonsensical.");
                return;
            }
            var units = (TUnit[])Enum.GetValues(typeof(TUnit));
            PrintUnits(units); 
            Console.Write("Select First Unit (Number): ");
            int u1 = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Select Second Unit (Number): ");
            int u2 = int.Parse(Console.ReadLine()) - 1;
            TUnit unit1 = units[u1];
            TUnit unit2 = units[u2];
            if (opChoice == 1)
            {
               CompareUnits(unit1, unit2, converter);
            }
            else
            {
                ArithmeticOperation op = opChoice switch
                {
                    2 => ArithmeticOperation.Add,
                    3 => ArithmeticOperation.Subtract,
                    4 => ArithmeticOperation.Divide,
                    _ => throw new InvalidOperationException("Invalid Operation Selection")
                };
                ExecuteArithmetic(unit1, unit2, converter, op);
            }
        }
        // Handling arithmetic operations as per the user choice.
        private void ExecuteArithmetic<TUnit>(TUnit unit1, TUnit unit2, IMeasurable<TUnit> converter, ArithmeticOperation op) where TUnit : Enum
        {
            double val1 = GetValue(unit1.ToString());
            double val2 = GetValue(unit2.ToString());

            var first = new Quantity<TUnit>(val1, unit1, converter);
            var second = new Quantity<TUnit>(val2, unit2, converter);

            var units = (TUnit[])Enum.GetValues(typeof(TUnit));
            Console.WriteLine($"\nSelect target unit for the {op}:");
            PrintUnits(units);

            if (int.TryParse(Console.ReadLine(), out int targetChoice))
            {
                TUnit targetUnit = units[targetChoice - 1];

                // Call the specific operation based on the Enum
                Quantity<TUnit> result = op switch
                {
                    ArithmeticOperation.Add => first.Add(second, targetUnit),
                    ArithmeticOperation.Subtract => first.Subtract(second, targetUnit),
                    ArithmeticOperation.Divide => first.Division(second, targetUnit),
                    _ => throw new ArgumentException("Invalid operation")
                };

                string symbol = op switch { ArithmeticOperation.Add => "+", ArithmeticOperation.Subtract => "-", _ => "/" };
                Console.WriteLine($"\nRESULT: {val1} {unit1} {symbol} {val2} {unit2} = {result.ConvertTo(targetUnit)} {targetUnit}\n");
            }
        }
        // Handles the comparison logic by creating Quantity objects and checking if they represent the same physical magnitude.
        private void CompareUnits<TUnit>(TUnit u1, TUnit u2, IMeasurable<TUnit> conv) where TUnit : Enum
        {
            var q1 = new Quantity<TUnit>(GetValue(u1.ToString()), u1, conv);
            var q2 = new Quantity<TUnit>(GetValue(u2.ToString()), u2, conv);
            Console.WriteLine($"Equality Result: {q1.Equals(q2)}");
        }
        // Helper method that iterates through any Unit Enum to display a numbered list of choices to the user.
        private void PrintUnits<T>(T[] units) where T : Enum
        {
            for (int i = 0; i < units.Length; i++) Console.WriteLine($"{i + 1}. {units[i]}");
        }
        // Prompts the user for a numerical input, parses it, and runs the validation check.
        public double GetValue(string label)
        {
            Console.Write($"Enter value for {label}: ");
            double val = double.Parse(Console.ReadLine());
            ValidateValue(val);
            return val;
        }
    }
}