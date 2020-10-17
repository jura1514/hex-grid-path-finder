using System.Collections.Generic;
using System.Linq;

namespace CalculateShortestPath
{
    public static class Directions
    {
        public static readonly List<DirectionDetails> List = new List<DirectionDetails>()
        {
            new DirectionDetails()
            {
                Direction = HexDirection.UpperLeft,
                X = -1,
                Y = 1
            },
            new DirectionDetails()
            {
                Direction = HexDirection.Top,
                X = 0,
                Y = 1
            },
            new DirectionDetails()
            {
                Direction = HexDirection.UpperRight,
                X = 1,
                Y = 0
            },
            new DirectionDetails()
            {
                Direction = HexDirection.LowerRight,
                X = 1,
                Y = -1
            },
            new DirectionDetails()
            {
                Direction = HexDirection.Bottom,
                X = 0,
                Y = -1
            },
            new DirectionDetails()
            {
                Direction = HexDirection.LowerLeft,
                X = -1,
                Y = 0
            },
        };

        public static DirectionDetails GetDirection(HexDirection direction)
        {
            return List.FirstOrDefault(x => x.Direction == direction);
        }
    }
}
