namespace CalculateShortestPath
{
    public class Hex
    {
        public int X { get; }
        public int Y { get; }
        public int Step { get; }

        public Hex(int x, int y, int step)
        {
            X = x;
            Y = y;
            Step = step;
        }

        public Hex GetHexNeighbor(DirectionDetails direction, int count)
        {
            return new Hex(X + direction.X, Y + direction.Y, count);
        }
    }
}