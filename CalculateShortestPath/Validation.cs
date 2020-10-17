using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculateShortestPath
{
    public static class Validation
    {
        public static void ValidateInput(IReadOnlyCollection<string> inputs)
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
