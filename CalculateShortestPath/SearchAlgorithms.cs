using System;
using System.Collections.Generic;

namespace CalculateShortestPath
{
    public class SearchAlgorithms
    {
        private readonly HexGrid grid;

        public SearchAlgorithms()
        {
            grid = new HexGrid();
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            grid.GenerateSpirals(new Hex(0, 0, 1));
        }

        public void ReachTargetUsingBreadthFirstSearch(int start, int end)
        {
            var startHex = grid.GetByStep(start);
            var targetHex = grid.GetByStep(end);

            if (startHex == null || targetHex == null)
            {
                Validation.HasInvalidInput();
            }

            Queue<Hex> frontier = new Queue<Hex>();
            List<Hex> reached = new List<Hex>();

            frontier.Enqueue(startHex);
            reached.Add(startHex);
            Hex goal = null;
            Dictionary<Hex, Hex> camefrom = new Dictionary<Hex, Hex>();
            camefrom[startHex] = null;

            while (frontier.Count != 0)
            {
                var current = frontier.Dequeue();

                if (current == targetHex)
                {
                    goal = current;
                    break;
                }

                var neighbours = grid.GetNeighbors(current);

                foreach (var n in neighbours)
                {
                    if (n != null)
                    {
                        if (!reached.Contains(n))
                        {
                            frontier.Enqueue(n);
                            reached.Add(n);
                            camefrom[n] = current;
                        }
                    }
                }
            }

            Console.WriteLine($"Reached goal: {goal.Step}");
        }

        public void ReachTargetUsingDijkstarsAlgorithm(int start, int end)
        {
            var startHex = grid.GetByStep(start);
            var targetHex = grid.GetByStep(end);

            var step13 = grid.GetByStep(13);
            var step29 = grid.GetByStep(29);
            step13.SetCost(5);
            step29.SetCost(5);

            if (startHex == null || targetHex == null)
            {
                Validation.HasInvalidInput();
            }

            PriorityQueue<Hex> frontier = new PriorityQueue<Hex>();

            frontier.Enqueue(startHex, 0);
            Hex goal = null;
            Dictionary<Hex, Hex> camefrom = new Dictionary<Hex, Hex>();
            Dictionary<Hex, double> costSoFar = new Dictionary<Hex, double>();

            camefrom[startHex] = null;
            costSoFar[startHex] = 0;

            while (frontier.Count != 0)
            {
                var current = frontier.Dequeue();

                if (current == targetHex)
                {
                    goal = current;
                    break;
                }

                var neighbours = grid.GetNeighbors(current);

                foreach (var n in neighbours)
                {
                    if (n != null)
                    {
                        // all hexes have the same cost of 1 for now
                        var newCost = costSoFar[current] + n.Cost;
                        if (!costSoFar.ContainsKey(n) || newCost < costSoFar[n])
                        {
                            costSoFar[n] = newCost;
                            var priority = newCost;
                            // uncomment for A* search
                            //var priority = newCost + Math.Abs(n.X - targetHex.X) + Math.Abs(n.Y - targetHex.Y); ;
                            frontier.Enqueue(n, priority);
                            camefrom[n] = current;
                        }
                    }
                }
            }

            Console.WriteLine($"Reached goal: {goal.Step}");
        }

        public class PriorityQueue<T>
        {
            private List<Tuple<T, double>> elements = new List<Tuple<T, double>>();

            public int Count
            {
                get { return elements.Count; }
            }

            public void Enqueue(T item, double priority)
            {
                elements.Add(Tuple.Create(item, priority));
            }

            public T Dequeue()
            {
                int bestIndex = 0;

                for (int i = 0; i < elements.Count; i++)
                {
                    if (elements[i].Item2 < elements[bestIndex].Item2)
                    {
                        bestIndex = i;
                    }
                }

                T bestItem = elements[bestIndex].Item1;
                elements.RemoveAt(bestIndex);
                return bestItem;
            }
        }
    }
}
