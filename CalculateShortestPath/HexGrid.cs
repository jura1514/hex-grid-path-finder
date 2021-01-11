using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculateShortestPath
{
    public class HexGrid
    {
        private readonly List<Hex> List;

        public HexGrid()
        {
            List = new List<Hex>();
        }

        public Hex GetByStep(int step)
        {
            return List.FirstOrDefault(x => x.Step == step);
        }

        public IEnumerable<Hex> GetNeighbors(Hex currentHex)
        {
            var neighbors = new List<Hex>();
            foreach (var direction in Directions.List)
            {
                var nextHexNeighbor = List.FirstOrDefault(e =>
                    e.X == (currentHex?.X + direction.X) && e.Y == (currentHex?.Y + direction.Y));
                neighbors.Add(nextHexNeighbor);
            }

            return neighbors;
        }

        public void GenerateSpirals(Hex center, int spirals = 4)
        {
            var count = center.Step;
            List.Add(center);

            for (int i = 1; i <= spirals; i++)
            {
                List.AddRange(GenerateRing(center, i,ref count));
            }

            Console.WriteLine($"Grid has been generated with: {List.Count} hexes");
        }

        private static IList<Hex> GenerateRing(Hex center, int spiral, ref int count)
        {
            var hexesRing = new List<Hex>();
            var direction = Directions.GetDirection(HexDirection.Bottom);

            // calculate the bottom hex with how far are you from center by multiplying it by spiral
            var hex = new Hex(center.X + direction.X * spiral, center.Y + direction.Y * spiral, count);

            for (int i = 0; i < Directions.List.Count; i++)
            {
                for (int j = 0; j < spiral; j++)
                {
                    count++;
                    hex = hex.GetHexNeighbor(Directions.List[i], count);
                    hexesRing.Add(hex);
                }
            }

            return hexesRing;
        }
    }
}