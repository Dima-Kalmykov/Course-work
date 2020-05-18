namespace WindowsFormsApp1
{
    internal class MyPoint
    {
        public long X;
        public long Y;

        public MyPoint(long x, long y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => "{" + $"{X}, {Y}" + "}";
    }
}
