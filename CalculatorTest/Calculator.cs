
using Dapper;
using Npgsql;

namespace CalculatorTest;


public class Calculator
    {
        private readonly string _connectionString;

        public Calculator(string connectionString)
        {
            _connectionString = connectionString;
        }

    public double Add(double n1, double n2)
    {
        double result = n1 + n2;
        SaveCalculation(n1, n2, "+", result);
        return result;
    }

    public double Subtract(double n1, double n2)
    {
        double result = n1 - n2;
        SaveCalculation(n1, n2, "-", result);
        return result;
    }

    public double Multiply(double n1, double n2)
    {
        double result = n1 * n2;
        SaveCalculation(n1, n2, "*", result);
        return result;
    }

    public double Divide(double n1, double n2)
    {
        if (n2 == 0)
        {
            Console.WriteLine("Error: Cannot divide by zero");
            return double.NaN;
        }

        double result = n1 / n2;
        SaveCalculation(n1, n2, "/", result);
        return result;
    }
    
    public Calculation SaveCalculation(double number1, double number2, string oper, double result)
    {
        var sql = @"
        INSERT INTO public.Calculations (number1, number2, Operator, result)
        VALUES (@Number1, @Number2, @Operator, @Result)
        RETURNING id as " + nameof(Calculation.Id) + @",
                  number1 as " + nameof(Calculation.Number1) + @",
                  number2 as " + nameof(Calculation.Number2) + @",
                  Operator as " + nameof(Calculation.Operator) + @",
                  result as " + nameof(Calculation.Result) + ";";
    
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            var parameters = new
            {
                Number1 = number1,
                Number2 = number2,
                Operator = oper,
                Result = result
            };

            return conn.QueryFirst<Calculation>(sql, parameters);
        }
    }
    
}