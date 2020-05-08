namespace WindowsFormsApp1
{
    /// <summary>
    /// Класс, описывающий вершины.
    /// </summary>
    public class Vertex
    {
        // Координаты вершины.
        internal int X;
        internal int Y;

        internal bool HasPoint = false;

        internal Vertex(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Вывод информация о вершине.
        /// Сделано для вывода информации в файл.
        /// </summary>
        /// <returns> Строка с информацией о вершине </returns>
        public override string ToString() => $"{X} {Y}";
    }
}