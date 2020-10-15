using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculateShortestPath
{
    public class Distance
    {
        private static readonly List<DirectionDetails> Directions = new List<DirectionDetails>()
        {
            //new DirectionDetails()
            //{
            //    Direction = HexDirection.Bottom,
            //    X = -1,
            //    Y = -1
            //},
            //new DirectionDetails()
            //{
            //    Direction = HexDirection.LowerLeft,
            //    X = 0,
            //    Y = 1
            //},
            //new DirectionDetails()
            //{
            //    Direction = HexDirection.UpperLeft,
            //    X = 1,
            //    Y = 1
            //},
            //new DirectionDetails()
            //{
            //    Direction = HexDirection.Top,
            //    X = 1,
            //    Y = 0
            //},
            //new DirectionDetails()
            //{
            //    Direction = HexDirection.UpperRight,
            //    X = 0,
            //    Y = -1
            //},
            //new DirectionDetails()
            //{
            //    Direction = HexDirection.LowerRight,
            //    X = -1,
            //    Y = -1
            //}
            new DirectionDetails()
            {
                Direction = HexDirection.LowerLeft,
                X = -1,
                Y = 0
            },
            new DirectionDetails()
            {
                Direction = HexDirection.UpperLeft,
                X = 0,
                Y = 1
            },
            new DirectionDetails()
            {
                Direction = HexDirection.Top,
                X = 1,
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
                X = 0,
                Y = -1
            },
            new DirectionDetails()
            {
                Direction = HexDirection.Bottom,
                X = 0,
                Y = -1
            }
        };
        private List<Hex> Hexes = new List<Hex>();

        public List<int> GetDistance(int start, int end)
        {
            GenerateSpirals();

            var hexOne = GetHexByName(start);
            var hexTwo = GetHexByName(end);

            if (hexOne == null || hexTwo == null)
            {
                Program.HasInvalidInput();
            }

            var next = hexOne;
            var wholePath = new List<int>();
            while (next?.Name != hexTwo?.Name)
            {
                var neighbors = GetHexNeighbors(next);
                next = FindNext(neighbors, hexTwo);
                if (next != null)
                {
                    Console.WriteLine($"Found next step: {next.Name}");
                    wholePath.Add(next.Name);
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
            var shortestDistance = int.MaxValue;
            Hex hexWithShortestDistance = null;

            foreach (var hex in neighbors.Where(x => x != null))
            {
                var distanceX = hex.X - target.X;
                var distanceY = hex.Y - target.Y;

                int distance;

                if (distanceX < 0 == distanceY < 0)
                {
                    distance = Math.Max(Math.Abs(distanceX), Math.Abs(distanceY));
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        hexWithShortestDistance = hex;
                    }
                }
                else
                {
                    distance = Math.Abs(distanceX) + Math.Abs(distanceY);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        hexWithShortestDistance = hex;
                    }
                }
            }

            return hexWithShortestDistance;
        }

        private void GenerateSpirals(int numberOfSpirals = 5)
        {
            var x = 0;
            var y = 0;
            var count = 1;

            Hexes.Add(new Hex() { X = x, Y = y, Name = count });

            for (int i = 1; i <= numberOfSpirals; i++)
            {
                Hexes.AddRange(GenerateRing(x, y, i, ref count));
            }

            Console.WriteLine($"Data has been generated with: {Hexes.Count + 1} hexes");
        }

        private static IEnumerable<Hex> GenerateRing(int x, int y, int currentSpiral, ref int count)
        {
            var hexesRing = new List<Hex>();
            var newHex = new Hex();

            var direction = GetDirection(HexDirection.Bottom);
            newHex.X = x + direction.X * currentSpiral;
            newHex.Y = y + direction.Y * currentSpiral;

            count++;
            newHex.Name = count;
            hexesRing.Add(newHex);

            var currentHex = newHex;
            for (int i = 0; i < Directions.Count; i++)
            {
                for (int j = 0; j < currentSpiral; j++)
                {
                    count++;
                    var nextHexNeighbor = GetHexNeighbor(currentHex, null ?? Directions[i], count);
                    hexesRing.Add(nextHexNeighbor);
                    currentHex = nextHexNeighbor;
                }
            }

            return hexesRing;
        }

        private IEnumerable<Hex> GetHexNeighbors(Hex currentHex)
        {
            var neighbors = new List<Hex>();
            foreach (var direction in Directions)
            {
                var nextHexNeighbor = Hexes.FirstOrDefault(e =>
                    e.X == (currentHex?.X + direction.X) && e.Y == (currentHex?.Y + direction.Y));
                neighbors.Add(nextHexNeighbor);
            }

            return neighbors;
        }

        private Hex GetHexByName(int number)
        {
            return Hexes.FirstOrDefault(x => x.Name == number);
        }

        private static DirectionDetails GetDirection(HexDirection direction)
        {
            return Directions.FirstOrDefault(x => x.Direction == direction);
        }

        private static Hex GetHexNeighbor(Hex currentHex, DirectionDetails direction, int count)
        {
            return new Hex()
            {
                X = currentHex.X + direction.X,
                Y = currentHex.Y + direction.Y,
                Name = count
            };
        }

        public int GetDistanceWithoutFullPath(int start, int end)
        {
            PopulateHardcodedData();

            var hexOne = GetHexByName(start);
            var hexTwo = GetHexByName(end);

            if (hexOne == null || hexTwo == null)
            {
                Program.HasInvalidInput();
            }

            var distance = 0;

            var distanceX = hexOne?.X - hexTwo?.X;
            var distanceY = hexOne?.Y - hexTwo?.Y;

            if (distanceX < 0 == distanceY < 0)
            {
                distance = Math.Max(Math.Abs((int)distanceX), Math.Abs((int)distanceY));
            }
            else
            {
                distance = Math.Abs((int)distanceX) + Math.Abs((int)distanceY);
            }

            return distance;
        }

        private void PopulateHardcodedData()
        {
            Hexes = new List<Hex>();
            int x = 0;
            int y = 0;
            Hexes.Add(new Hex {X = x, Y = y, Name = 1});
            x = -1;
            y = -1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 2});
            x = -1;
            y = 0;
            Hexes.Add(new Hex {X = x, Y = y, Name = 3});
            x = 0;
            y = 1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 4});
            x = 1;
            y = 1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 5});
            x = 1;
            y = 0;
            Hexes.Add(new Hex {X = x, Y = y, Name = 6});
            x = 0;
            y = -1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 7});
            x = -1;
            y = -2;
            Hexes.Add(new Hex {X = x, Y = y, Name = 8});
            x = -2;
            y = -2;
            Hexes.Add(new Hex {X = x, Y = y, Name = 9});
            x = -2;
            y = -1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 10});
            x = -2;
            y = 0;
            Hexes.Add(new Hex {X = x, Y = y, Name = 11});
            x = -1;
            y = 1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 12});
            x = 0;
            y = 2;
            Hexes.Add(new Hex {X = x, Y = y, Name = 13});
            x = 1;
            y = 2;
            Hexes.Add(new Hex {X = x, Y = y, Name = 14});
            x = 2;
            y = 2;
            Hexes.Add(new Hex {X = x, Y = y, Name = 15});
            x = 2;
            y = 1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 16});
            x = 2;
            y = 0;
            Hexes.Add(new Hex {X = x, Y = y, Name = 17});
            x = 1;
            y = -1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 18});
            x = 0;
            y = -2;
            Hexes.Add(new Hex {X = x, Y = y, Name = 19});
            x = -1;
            y = -3;
            Hexes.Add(new Hex {X = x, Y = y, Name = 20});
            x = -2;
            y = -3;
            Hexes.Add(new Hex {X = x, Y = y, Name = 21});
            x = -3;
            y = -3;
            Hexes.Add(new Hex {X = x, Y = y, Name = 22});
            x = -3;
            y = -2;
            Hexes.Add(new Hex {X = x, Y = y, Name = 23});
            x = -3;
            y = -1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 24});
            x = -3;
            y = 0;
            Hexes.Add(new Hex {X = x, Y = y, Name = 25});
            x = -2;
            y = 1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 26});
            x = -1;
            y = 2;
            Hexes.Add(new Hex {X = x, Y = y, Name = 27});
            x = 0;
            y = 3;
            Hexes.Add(new Hex {X = x, Y = y, Name = 28});
            x = 1;
            y = 3;
            Hexes.Add(new Hex {X = x, Y = y, Name = 29});
            x = 2;
            y = 3;
            Hexes.Add(new Hex {X = x, Y = y, Name = 30});
            x = 3;
            y = 3;
            Hexes.Add(new Hex {X = x, Y = y, Name = 31});
            x = 3;
            y = 2;
            Hexes.Add(new Hex {X = x, Y = y, Name = 32});
            x = 3;
            y = 1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 33});
            x = 3;
            y = 0;
            Hexes.Add(new Hex {X = x, Y = y, Name = 34});
            x = 2;
            y = -1;
            Hexes.Add(new Hex {X = x, Y = y, Name = 35});
            x = 1;
            y = -2;
            Hexes.Add(new Hex {X = x, Y = y, Name = 36});
            x = 0;
            y = -3;
            Hexes.Add(new Hex {X = x, Y = y, Name = 37});
            x = -1;
            y = -4;
            Hexes.Add(new Hex {X = x, Y = y, Name = 38});
            x = -2;
            y = -4;
            Hexes.Add(new Hex {X = x, Y = y, Name = 39});
            x = -3;
            y = -4;
            Hexes.Add(new Hex {X = x, Y = y, Name = 40});
        }
    }
}