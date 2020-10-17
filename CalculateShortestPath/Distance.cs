using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculateShortestPath
{
    public class Distance
    {
        private readonly HexGrid grid;

        public Distance()
        {
            grid = new HexGrid();
        }

        public void GenerateGrid()
        {
            grid.GenerateSpirals(new Hex(0, 0, 1));
        }

        public List<int> GetDistance(int start, int end)
        {
            var startHex = grid.GetByStep(start);
            var targetHex = grid.GetByStep(end);

            if (startHex == null || targetHex == null)
            {
                Validation.HasInvalidInput();
            }

            var next = startHex;
            var wholePath = new List<int>();
            while (next?.Step != targetHex?.Step)
            {
                var neighbors = grid.GetNeighbors(next);
                next = FindNext(neighbors, targetHex);
                if (next != null)
                {
                    Console.WriteLine($"Found next step: {next.Step}");
                    wholePath.Add(next.Step);
                }
                else
                {
                    Console.WriteLine("Ups something went wrong, try again!");
                    Environment.Exit(0);
                }
            }

            return wholePath;
        }

        private static Hex FindNext(IEnumerable<Hex> neighbors, Hex target)
        {
            var closestDistance = int.MaxValue;
            Hex closestHex = null;

            foreach (var hex in neighbors.Where(x => x != null))
            {
                var distanceX = hex.X - target.X;
                var distanceY = hex.Y - target.Y;

                int distance;

                if (distanceX < 0 != distanceY < 0)
                {
                    distance = Math.Max(Math.Abs(distanceX), Math.Abs(distanceY));
                    if (distance <= closestDistance)
                    {
                        closestDistance = distance;
                        closestHex = hex;
                    }
                }
                else
                {
                    distance = Math.Abs(distanceX) + Math.Abs(distanceY);
                    if (distance <= closestDistance)
                    {
                        closestDistance = distance;
                        closestHex = hex;
                    }
                }
            }

            return closestHex;
        }

        public int GetDistanceWithoutFullPath(int start, int end)
        {
            var startHex = grid.GetByStep(start);
            var targetHex = grid.GetByStep(end);

            if (startHex == null || targetHex == null)
            {
                Validation.HasInvalidInput();
            }

            var distance = 0;

            var distanceX = startHex.X - targetHex.X;
            var distanceY = startHex.Y - targetHex.Y;

            if (distanceX < 0 != distanceY < 0)
            {
                distance = Math.Max(Math.Abs(distanceX), Math.Abs(distanceY));
            }
            else
            {
                distance = Math.Abs(distanceX) + Math.Abs(distanceY);
            }

            return distance;
        }
    }
}