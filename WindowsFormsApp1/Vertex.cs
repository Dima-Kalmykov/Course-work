namespace WindowsFormsApp1
{
    public class Vertex
    {
        internal int X;
        internal int Y;

        internal bool HasPoint = false;

        internal Vertex(int x, int y) => (X, Y) = (x, y);

        public override string ToString() => $"{X} {Y}";
    }
}