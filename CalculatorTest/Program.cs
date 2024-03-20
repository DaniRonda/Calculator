using System;

namespace CalculatorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
            var calculator = new Calculator(connectionString);
            
            Console.WriteLine("Calculator");
            Console.WriteLine("------------------");

            while (true)
            {
                Console.Write("Maths (example, 5 + 3): ");
                string input = Console.ReadLine();

                
                string[] parts = input.Split(new[] { '+', '-', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                {
                    Console.WriteLine("Wrong format.");
                    continue;
                }

                double num1, num2;

                if (!double.TryParse(parts[0], out num1) || !double.TryParse(parts[1], out num2))
                {
                    Console.WriteLine("Sad trumpet sound");
                    continue;
                }

                char oper = input[parts[0].Length];

                double result;

                switch (oper)
                {
                    case '+':
                        result = calculator.Add(num1, num2);
                        break;
                    case '-':
                        result = calculator.Subtract(num1, num2);
                        break;
                    case '*':
                        result = calculator.Multiply(num1, num2);
                        break;
                    case '/':
                        if (num2 == 0)
                        {
                            Console.WriteLine("Cannot dive by 0, dumbass.");
                            continue;
                        }
                        result = calculator.Divide(num1, num2);
                        break;
                    default:
                        Console.WriteLine("Invalid operator.");
                        continue;
                }

                Console.WriteLine($"Result: {result}");

                
                var savedCalculation = calculator.SaveCalculation(num1, num2, oper.ToString(), result);

                Console.WriteLine($"Saved with id: {savedCalculation.Id}");

                Console.Write("More maths? (y/n): ");
                string continueInput = Console.ReadLine().ToLower();

                if (continueInput != "s")
                    break;
            }

            Console.WriteLine("bye");
        }
    }
}
