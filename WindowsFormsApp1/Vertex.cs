namespace WindowsFormsApp1
{
    public class Vertex
    {
        public int X;
        public int Y;

        public bool HasPoint = false;

        public Vertex(int x, int y) => (X, Y) = (x, y);

        public override string ToString() => $"{X} {Y}";
    }
}