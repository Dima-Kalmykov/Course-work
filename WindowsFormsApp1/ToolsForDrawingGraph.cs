using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsApp1
{
    public class ToolsForDrawingGraph
    {
        // Инструменты для рисования графа.
        private Bitmap bitmap;
        private Graphics cover;
        private PointF point;
        private Font font;
        private Brush brush;

        private Pen blackPen;
        private Pen redPen;
        private Pen bluePen;
        private Pen greenPen;
        private Pen whitePen;

        // Радиус вершины.
        internal int R = 20;

        internal int PointR = 10;

        // Ширина и высота наконечника стрелки.
        private int widthArrow = 10;
        private int heightArrow = 7;

        private Random rnd = new Random();

        internal ToolsForDrawingGraph(int width, int height)
        {
            // Установка необходимых параметров.
            bitmap = new Bitmap(width, height);
            cover = Graphics.FromImage(bitmap);

            blackPen = new Pen(Color.Black, 2);
            redPen = new Pen(Color.Red, 2);
            bluePen = new Pen(Color.CornflowerBlue, 2);
            greenPen = new Pen(Color.Green, 2);
            whitePen = new Pen(Color.White, 2);
            brush = Brushes.Black;

            font = new Font("Arial", 15);
        }

        internal void DrawAllEdges(List<Edge> edges, List<Vertex> vertex)
        {
            foreach (var edge in edges)
                DrawEdge(vertex[edge.Ver1], vertex[edge.Ver2], edge);
        }

        /// <summary>
        /// Заполнение матрицы смежности.
        /// </summary>
        /// <param name="amountVertex"> Количество вершин </param>
        /// <param name="edges"> Список рёбер </param>
        /// <param name="adjMatrix"> Матрица смежности </param>
        internal void FillAdjMatrix(int amountVertex, List<Edge> edges, double[,] adjMatrix)
        {
            // Очищаем матрицу.
            Array.Clear(adjMatrix, 0, amountVertex);

            for (int i = 0; i < edges.Count; i++)
                adjMatrix[edges[i].Ver1, edges[i].Ver2] =
                    Math.Round(edges[i].Weight, 3, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Рисование вершины.
        /// </summary>
        /// <param name="x"> x координата вершины </param>
        /// <param name="y"> y координата вершины </param>
        /// <param name="number"> Число в вершине </param>
        internal void DrawVertex(int x, int y, string number)
        {
            cover.FillEllipse(Brushes.White, x - R, y - R, 2 * R, 2 * R);
            cover.DrawEllipse(blackPen, x - R, y - R, 2 * R, 2 * R);

            // Определям положение числа в вершине в зависимости от числа его знаков.
            point = int.Parse(number) < 10 ?
                new PointF(x - R / 2 + 2, y - R / 2) :
                new PointF(x - R / 2 - 3, y - R / 2);

            // Рисуем число в вершине.
            cover.DrawString(number, font, brush, point);
        }

        internal void DeletePoint(float x, float y, Vertex ver1, Vertex ver2)
        {
                cover.FillEllipse(Brushes.White,
                    x - PointR / 2, y - PointR / 2, PointR, PointR);

                cover.DrawEllipse(whitePen,
                    x - PointR / 2, y - PointR / 2, PointR, PointR);

                DrawArrow(ver1, ver2);
        }

        internal void DrawPoint(float x, float y)
        {
                cover.FillEllipse(Brushes.Blue,
                    x - PointR / 2, y - PointR / 2, PointR, PointR);

                cover.DrawEllipse(greenPen,
                    x - PointR / 2, y - PointR / 2, PointR, PointR);
        }

        /// <summary>
        /// Рисование ребра.
        /// </summary>
        /// <param name="ver1"> Первая вершина </param>
        /// <param name="ver2"> Вторая вершина </param>
        /// <param name="edge"> Список рёбер </param>
        internal void DrawEdge(Vertex ver1, Vertex ver2, Edge edge)
        {
            // Если петля, то рисуем дугу.
            if (edge.Ver1 == edge.Ver2)
            {
                cover.DrawArc(greenPen, ver1.X - 2 * R, ver1.Y - 2 * R,
                    2 * R, 2 * R, 90, 270);

                // Дорисовываем наконечник.
                cover.DrawLine(greenPen,
                    ver1.X - 7, ver1.Y - R - 7,
                    ver1.X, ver1.Y - R);

                cover.DrawLine(greenPen,
                    ver1.X + 7, ver1.Y - R - 7,
                    ver2.X, ver2.Y - R);

                // Заново отображаем вершину.
                DrawVertex(ver1.X, ver1.Y, (edge.Ver1 + 1).ToString());
            }
            // Если это не петля, то рисуем стрелку.
            else
            {
                DrawArrow(ver1, ver2);
                DrawVertex(ver1.X, ver1.Y, (edge.Ver1 + 1).ToString());
                DrawVertex(ver2.X, ver2.Y, (edge.Ver2 + 1).ToString());
            }
        }

        /// <summary>
        /// Рисование стрелки.
        /// </summary>
        /// <param name="ver1"> Первая вершина </param>
        /// <param name="ver2"> Вторая вершина </param>
        internal void DrawArrow(Vertex ver1, Vertex ver2)
        {
            // Коэффициенты прямой y = kx + b.
            double k = 0;
            double b;

            // Координаты точки, куда приходит стрелка.
            double xEnd;
            double yEnd;

            // Координаты точки, откуда выходит стрелка.
            double xBegin;
            double yBegin;

            // Случай, когда у вершин разные x координаты.
            if (ver2.X != ver1.X)
            {
                // Вычисляем коэффициенты прямой y = kx + b.
                k = Calculate.GetK(ver1, ver2);
                b = Calculate.GetB(ver1, ver2);

                // Вычисляем промежуточную величину.
                double sqrtDiscriminant2 = Calculate.GetSqrtDiscriminant(ver2, k, b);

                // Мы ищем точки пересечения прямой, проходящей через 2 данные веришны,
                // и окружности, которая описана около веришины. Их будет 2.
                // Причём одна находится ближе к первой вершине (xMin, yMin),
                // а другая дальше (xMax, yMax).

                // Ищем соответсвующие точки, и получаем координаты xEnd, yEnd
                // в зависимости от расположения вершин.
                double xMaxEnd = (sqrtDiscriminant2 + ver2.X + k * (ver2.Y - b)) / (k * k + 1);
                double xMinEnd = (-sqrtDiscriminant2 + ver2.X + k * (ver2.Y - b)) / (k * k + 1);

                double yMinEnd = k * xMaxEnd + b;
                double yMaxEnd = k * xMinEnd + b;

                if (ver2.X > ver1.X)
                {
                    xEnd = xMinEnd;
                    yEnd = yMaxEnd;
                }
                else
                {
                    xEnd = xMaxEnd;
                    yEnd = yMinEnd;
                }

                // Аналогично.
                double sqrtDiscriminant1 = Calculate.GetSqrtDiscriminant(ver1, k, b);

                double xMaxBegin = (sqrtDiscriminant1 + ver1.X + k * (ver1.Y - b)) / (k * k + 1);
                double xMinBegin = (-sqrtDiscriminant1 + ver1.X + k * (ver1.Y - b)) / (k * k + 1);

                double yMinBegin = k * xMaxBegin + b;
                double yMaxBegin = k * xMinBegin + b;

                if (ver2.X > ver1.X)
                {
                    if (ver2.Y < ver1.Y)
                    {
                        xBegin = xMaxBegin;
                        yBegin = yMinBegin;
                    }
                    else
                    {
                        xBegin = xMaxBegin;
                        yBegin = yMinBegin;
                    }
                }
                else
                {
                    if (ver2.Y < ver1.Y)
                    {
                        xBegin = xMinBegin;
                        yBegin = yMaxBegin;
                    }
                    else
                    {
                        xBegin = xMinBegin;
                        yBegin = yMaxBegin;
                    }
                }
            }
            // Случай, когда у вершин одинаковые x коориднаты.
            else
            {
                xBegin = xEnd = ver1.X;

                if (ver1.Y > ver2.Y)
                {
                    yBegin = ver1.Y - R;
                    yEnd = ver2.Y + R;
                }
                else
                {
                    yBegin = ver1.Y + R;
                    yEnd = ver2.Y - R;
                }
            }


            // Две точки с координатами наконечника.
            double firstEndArrowX = 0;
            double firstEndArrowY = 0;
            double secondEndArrowX = 0;
            double secondEndArrowY = 0;

            // В зависимости от расположения вершин, ищем эти координаты.
            if (xBegin != xEnd && yBegin != yEnd)
            {
                // Ищем координаты точки, делящей отрезок в отношении.
                // Затем строим через эту точку перпендикуляр определённой длины.
                // Концы этого перпендикуляра и будут точками для наконечника.

                double xCoordO = Calculate.GetCoordOx(xBegin, yBegin, xEnd, yEnd);
                double yCoordO = Calculate.GetCoordOy(xBegin, yBegin, xEnd, yEnd);

                firstEndArrowX = xCoordO + heightArrow / Math.Sqrt((1 + 1 / (k * k)));
                firstEndArrowY = -1 / k * firstEndArrowX + yCoordO + xCoordO / k;

                secondEndArrowX = xCoordO - heightArrow / Math.Sqrt((1 + 1 / (k * k)));
                secondEndArrowY = -1 / k * secondEndArrowX + yCoordO + xCoordO / k;
            }
            else if (xBegin == xEnd)
            {
                if (yBegin < yEnd)
                {
                    firstEndArrowX = xEnd - heightArrow;
                    firstEndArrowY = yEnd - widthArrow;

                    secondEndArrowX = xEnd + heightArrow;
                    secondEndArrowY = yEnd - widthArrow;
                }
                else
                {
                    firstEndArrowX = xEnd - heightArrow;
                    firstEndArrowY = yEnd + widthArrow;

                    secondEndArrowX = xEnd + heightArrow;
                    secondEndArrowY = yEnd + widthArrow;
                }
            }
            else if (yBegin == yEnd)
            {
                if (xBegin < xEnd)
                {
                    firstEndArrowX = xEnd - widthArrow;
                    firstEndArrowY = yEnd - heightArrow;

                    secondEndArrowX = xEnd - widthArrow;
                    secondEndArrowY = yEnd + heightArrow;
                }
                else
                {
                    firstEndArrowX = xEnd + widthArrow;
                    firstEndArrowY = yEnd + heightArrow;

                    secondEndArrowX = xEnd + widthArrow;
                    secondEndArrowY = yEnd - heightArrow;
                }
            }

            // Рисуем ребро + наконечник.
            cover.DrawLine(bluePen,
                (float)xBegin, (float)yBegin,
                (float)xEnd, (float)yEnd);

            cover.DrawLine(bluePen,
                (float)firstEndArrowX, (float)firstEndArrowY,
                (float)xEnd, (float)yEnd);

            cover.DrawLine(bluePen,
                (float)secondEndArrowX, (float)secondEndArrowY,
                (float)xEnd, (float)yEnd);
        }

        /// <summary>
        /// Заполняем список вершин случайным образом.
        /// </summary>
        /// <param name="adjMatrix"> Матрица смежности </param>
        /// <param name="maxX"> Границы холста </param>
        /// <param name="maxY"> Границы холста </param>
        /// <returns> Список вершин </returns>
        internal List<Vertex> GetRandomVertex(double[,] adjMatrix, int maxX, int maxY)
        {
            int xCoord;
            int yCoord;
            bool nearOtherVertex;

            List<Vertex> vertex = new List<Vertex>();

            for (int i = 0; i < adjMatrix.GetLength(0); i++)
            {
                xCoord = rnd.Next(2 * R, maxX + 1);
                yCoord = rnd.Next(2 * R, maxY + 1);

                nearOtherVertex = true;

                // Если это первая веришна, то просто добавляем её.
                if (i == 0)
                {
                    vertex.Add(new Vertex(xCoord, yCoord));
                    continue;
                }
                // Если это уже не первая вершина, то проверяем,
                // чтобы рядом не было других
                for (int j = 0; j < vertex.Count; j++)
                {
                    if (Math.Abs(vertex[j].X - xCoord) < 2 * R &&
                        Math.Abs(vertex[j].Y - yCoord) < 2 * R)
                    {
                        i--;
                        nearOtherVertex = false;
                        break;
                    }
                }

                // Если рядом нет других, то добавляем новую вершину.
                if (nearOtherVertex)
                    vertex.Add(new Vertex(xCoord, yCoord));
            }

            return vertex;
        }

        /// <summary>
        /// Заполняем список рёбер при генерации графа.
        /// </summary>
        /// <param name="adjMatrix"> Матрица смежности</param>
        /// <returns> Список рёбер </returns>
        internal List<Edge> GetRandomEdges(double[,] adjMatrix)
        {
            List<Edge> edges = new List<Edge>();

            // Весом каждого ребра служит значение из матрицы смежности.
            for (int i = 0; i < adjMatrix.GetLength(0); i++)
                for (int j = 0; j < adjMatrix.GetLength(1); j++)
                    if (adjMatrix[i, j] != 0)
                        edges.Add(new Edge(i, j, adjMatrix[i, j]));

            return edges;
        }

        /// <summary>
        /// Рисуем полный граф.
        /// </summary>
        /// <param name="edges"> Список рёбер </param>
        /// <param name="vertex"> Список вершин </param>
        internal void DrawFullGraph(List<Edge> edges, List<Vertex> vertex)
        {
            ClearField();

            foreach (var edge in edges)
                DrawEdge(vertex[edge.Ver1], vertex[edge.Ver2], edge);

            for (int i = 0; i < vertex.Count; i++)
                DrawVertex(vertex[i].X, vertex[i].Y, (i + 1).ToString());
        }

        /// <summary>
        /// Задаём рёбрам случайный вес.
        /// </summary>
        /// <param name="adjMatrix"> Матрица смежности </param>
        /// <returns> Матрица смежности </returns>
        internal double[,] SetDistanceEdge(double[,] adjMatrix)
        {
            // Min и Max значения веса рёбер.
            int min = 1;
            int max = 999;

            // Пробегаемся по всем элементам и заполняем матрицу.
            for (int i = 0; i < adjMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < adjMatrix.GetLength(1); j++)
                {
                    if (adjMatrix[i, j] == 1)
                        adjMatrix[i, j] =
                            rnd.Next(min, max) + rnd.NextDouble() +
                            rnd.Next(2) * double.Epsilon;
                }

            }

            return adjMatrix;
        }

        /// <summary>
        /// Заполняем матрицу смежности случайным образом.
        /// При этом граф должен получиться сильносвязным.
        /// </summary>
        /// <param name="size"> Размер матрицы </param>
        /// <returns> Матрица смежности </returns>
        internal double[,] GetRandomAdjMatrix(int size)
        {
            double[,] adjMatrix = new double[size, size];

            // Частный случай.
            if (size == 1)
            {
                adjMatrix[0, 0] = rnd.Next(2);
                return adjMatrix;
            }

            // Частный случай.
            if (size == 2)
            {
                adjMatrix[1, 0] = 1;
                adjMatrix[0, 1] = 1;
                adjMatrix[0, 0] = rnd.Next(2);
                adjMatrix[1, 1] = rnd.Next(2);

                return adjMatrix;
            }

            // Проводим рёбра по кругу (1 -> 2 -> 3 -> ... -> n-1 -> n -> 1)
            for (int i = 0; i < size; i++)
                if (i == size - 1)
                    adjMatrix[i, 0] = 1;
                else
                    adjMatrix[i, i + 1] = 1;

            // Из каждой веришны проводим ещё одно ребро случайным образом.
            for (int i = 0; i < size; i++)
                adjMatrix[i, rnd.Next(size)] = 1;

            // Случайным образом заполняем диагональ.
            for (int i = 0; i < size; i++)
                adjMatrix[i, i] = rnd.Next(2);

            return adjMatrix;
        }

        /// <summary>
        /// Возвращаем изменения на холсте.
        /// </summary>
        /// <returns> холст </returns>
        internal Bitmap GetBitmap() =>
            bitmap;

        /// <summary>
        /// Очистка холста.
        /// </summary>
        internal void ClearField() =>
            cover.Clear(Color.White);

        /// <summary>
        /// Выделение вершины.
        /// </summary>
        /// <param name="x"> x координата вершины </param>
        /// <param name="y"> y координата вершины </param>
        internal void DrawSelectedVertex(int x, int y) =>
            cover.DrawEllipse(redPen, x - R, y - R, 2 * R, 2 * R);
    }
}