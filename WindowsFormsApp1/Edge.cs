using System.Threading;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Класс, описывающий рёбра.
    /// </summary>
    public class Edge
    {
        // Номера вершин, между которыми находится ребро.
        internal int Ver1;
        internal int Ver2;

        internal double K;
        internal double B;
        internal double Step;

        // Вес ребра.
        internal double Weight;

        internal Edge(int ver1, int ver2, double weight)
        {
            this.Ver1 = ver1;
            this.Ver2 = ver2;
            this.Weight = weight;
        }

        /// <summary>
        /// Вывод информация о ребре.
        /// Сделано для вывода информации в файл.
        /// </summary>
        /// <returns> Строка с информацией о ребре </returns>
        public override string ToString() =>
            $"{Ver1} {Ver2} {Weight}";
    }
}