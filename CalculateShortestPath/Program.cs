using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateShortestPath
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var search = new SearchAlgorithms();
            //search.ReachTargetUsingDijkstarsAlgorithm(28, 32);

            var input = "Y";
            while (input?.ToUpper() == "Y")
            {
                RunFindPath();
                Console.WriteLine("Do you want to use path finder again? y/n");
                input = Console.ReadLine();
            }

            Console.WriteLine("End");
        }

        private static void RunFindPath()
        {
            var distance = new Distance();
            distance.GenerateGrid();

            Console.WriteLine("Enter the start and end tiles separated by space:");
            var input = Console.ReadLine();
            var inputs = input?.Split(" ");

            Validation.ValidateInput(inputs);

            var startValue = int.Parse(inputs[0]);
            var endValue = int.Parse(inputs[1]);

            Console.WriteLine($"You have entered:{startValue} {endValue}");

            var path = distance.GetDistance(startValue, endValue);

            Console.WriteLine($"Total steps taken:{path.Count} {GetPath(path)}");
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
    }
}
