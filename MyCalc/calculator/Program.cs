using System;
using System.Linq;

namespace calculator
{
    class MainClass
    {
        static Boolean CheckInput(String input)
        {

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsSymbol(input[i]) == true)
                {
                    if (input[i] == '+') { }

                    else
                    {
                        Console.WriteLine("Error: Symbol! -> " + input[i]);
                        return false;
                    }
                }

                if (Char.IsLetter(input[i]) == true)
                {
                    Console.WriteLine("Error: Letter! -> " + input[i]);
                    return false;
                }

                if (Char.IsPunctuation(input[i]) == true)
                {
                    if (input[i] == '-' ||
                       input[i] == '*' ||
                       input[i] == '/' ||
                       input[i] == '.')
                    { }
                    
                    else
                    {
                        Console.WriteLine("Error: Punctuation! -> " + input[i]);
                        return false;
                    }
                }
            }
            return true;
        }

        static Char GetOperator(String input)
        {
            Char mathOperator = '?';

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '+' || input[i] == '-' || input[i] == '/' || input[i] == '*')
                {
                    mathOperator = input[i];
                }
            }

            return mathOperator;
        }

        static Double[] GetOperands(String input)
        {

            Double[] numbers = { -1.0, -1.0 };

            String newString = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]) || input[i] == '.')
                {
                    newString = newString + input[i];
                }

                else
                    break;
            }
            numbers[0] = Convert.ToDouble(newString);

            newString = "";
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (Char.IsDigit(input[i]) || input[i] == '.')
                {
                    newString = newString + input[i];
                }

                else
                    break;
            }
            char[] array = newString.ToCharArray();
            Array.Reverse(array);
            numbers[1] = Convert.ToDouble(new string(array));
            return numbers;
        }

        static Tuple<Double, Double, Char> GetCleanInput(String input)
        {
            var numbers = GetOperands(input);
            Double firstOperand = numbers[0];
            Double secondOperand = numbers[1];
            Char mathOperator = GetOperator(input);

            return Tuple.Create(firstOperand, secondOperand, mathOperator);
        }

        static int[] RemoveCommas(Double first, Double second)
        {
            int[] numbers = { -1,-1};

            numbers[0] = (int)(first * 100);
            numbers[1] = (int)(second * 100);

            return numbers;
        }

        static Tuple<Double, String[]> Add(int[] input)
        {
            Double result = input[0] + input[1];
            String[] resultArray = { "  " + input[0] + "\n+" + (new string(' ', (input[0].ToString().Length - input[1].ToString().Length) + 1)) + input[1] + "\n-----------\n  " + result };

            return Tuple.Create(result, resultArray);
        }

        static Tuple<Double, String[]> Substract(int[] input)
        {
            Double result = input[0] - input[1];
            String[] resultArray = { "  " + input[0] + "\n-" + (new string(' ', (input[0].ToString().Length - input[1].ToString().Length) + 1)) + input[1] + "\n-----------" + "\n  " + result };

            return Tuple.Create(result, resultArray);
        }

        static Tuple<Double, String[]> Multiply(int[] input)
        {
            Double result = input[0] * input[1];
            //Double result = (input[0] * input[1]) / 10000.0;
            String[] resultArray = { "   " + (((Double)input[0]) / 100.0) + "\n" + "* " + (((Double)input[1]) / 100.0) + "\n-----------" + "\n   " + result };

            return Tuple.Create(result, resultArray);
        }

        static Tuple<Double, String[]> Divide(int[] input)
        {
            Double result = input[0] / input[1];
            //Double result = (input[0] / input[1]) / 10000.0;
            String[] resultArray = { "  " + input[0] + "\n" + "/ " + input[1] + "\n-----------" + "\n " + result };

            return Tuple.Create(result, resultArray);
        }

        static Tuple<Double, String[]> Calculate(Tuple<Double, Double, Char> input)
        {
            String[] resultArray = { "First", "Second" };
            var result = Tuple.Create(3.14, resultArray);

            int[] variables = RemoveCommas(input.Item1, input.Item2);

            switch (input.Item3)
            {
                case '+':
                    result = Add(variables);
                    break;
                case '-':
                    result = Substract(variables);
                    break;
                case '*':
                    result = Multiply(variables);
                    break;
                case '/':
                    result = Divide(variables);
                    break;
                default:
                    Console.WriteLine("Error in a Calculate method!");
                    System.Environment.Exit(0);
                    break;
            }

            return result;
        }

        public static void Main(string[] args)
        {

            String input = Console.ReadLine();

            // Test to check input.
            if (CheckInput(input) == false)
                Console.WriteLine("Wrong Input!");
            else { }
            //Console.WriteLine("Clean Input!");


            // Method to convert string line into Tuple<Double, Double, Char> or (firstOperand, secondOperand, mathOperator).
            var numbers = GetCleanInput(input);

            // Returns Tuple<Double, String[]> or (result, actions)
            Tuple<Double, String[]> result = Calculate(numbers);

            Console.WriteLine(string.Join("", result.Item2));




        }
    }
}
