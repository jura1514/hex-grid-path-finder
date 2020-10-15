using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculateShortestPath
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the start and end tiles separated by space:");
            var input = Console.ReadLine();
            var inputs = input?.Split(" ");

            ValidateInput(inputs);

            var startValue = int.Parse(inputs[0]);
            var endValue = int.Parse(inputs[1]);

            Console.WriteLine($"You have entered:{startValue} {endValue}");

            var distance = new Distance();
            var path = distance.GetDistance(startValue, endValue);

            Console.WriteLine($"Total steps taken:{path.Count} {GetPath(path)}");

            Console.WriteLine($"Distance: {distance.GetDistanceWithoutFullPath(startValue, endValue)}");

            Console.WriteLine("End");
        }

        private static string GetPath(List<int> path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            path.ForEach(x =>
            {
                sb.Append($"{x}-");
            });
            sb.Length -= 1;
            sb.Append(")");
            return sb.ToString();
        }

        private static void ValidateInput(IReadOnlyCollection<string> inputs)
        {
            if (inputs == null || inputs.Count != 2)
            {
                HasInvalidInput();
            }

            var startParsed = int.TryParse(inputs?.ElementAt(0), out _);
            var endParsed = int.TryParse(inputs?.ElementAt(1), out _);

            if (!startParsed || !endParsed)
            {
                HasInvalidInput();
            }
        }

        public static void HasInvalidInput()
        {
            Console.WriteLine("Invalid Input");
            Environment.Exit(0);
        }
    }
}
