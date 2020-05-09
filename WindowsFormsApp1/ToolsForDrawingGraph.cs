using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WindowsFormsApp1
{
    public class ToolsForDrawingGraph
    {
        // Инструменты для рисования графа.
        private readonly Bitmap bitmap;
        private  Graphics cover;
        private PointF point;
        private readonly Font font;
        private readonly Brush brush;

        private readonly Pen blackPen;
        private readonly Pen redPen;
        private readonly Pen bluePen;
        private readonly Pen greenPen;

        // Радиус вершины.
        internal const int VertexRadius = Consts.VertexRadius;
        internal const int PointRadius = Consts.PointRadius;

        // Ширина и высота наконечника стрелки.
        private const int WidthArrow = Consts.WidthArrow;
        private const int HeightArrow = Consts.HeightArrow;

        private static readonly Random Rnd = new Random(DateTime.Now.Millisecond);

        internal ToolsForDrawingGraph(int width, int height)
        {
            // Установка необходимых параметров
            // @"C:\Users\Dmitry\Desktop".
            bitmap = new Bitmap(width, height);
            //bitmap = new Bitmap(@"C:\Users\Dmitry\Desktop\1561182897_3.jpg");
            cover = Graphics.FromImage(bitmap);
            

            blackPen = new Pen(Color.Black, Consts.WidthPen);
            redPen = new Pen(Color.Red, Consts.WidthPen);
            bluePen = new Pen(Color.CornflowerBlue, Consts.WidthPen);
            greenPen = new Pen(Color.Green, Consts.WidthPen);
            brush = Brushes.Black;

            font = new Font("Arial", Consts.FontSize);
        }


        /// <summary>
        /// Заполнение матрицы смежности.
        /// </summary>
        /// <param name="amountVertex"> Количество вершин </param>
        /// <param name="edges"> Список рёбер </param>
        /// <param name="adjacencyMatrix"> Матрица смежности </param>
        internal void FillAdjMatrix(int amountVertex, List<Edge> edges, double[,] adjacencyMatrix)
        {
            // Очищаем матрицу.
            Array.Clear(adjacencyMatrix, 0, amountVertex);

            foreach (var edge in edges)
            {
                adjacencyMatrix[edge.Ver1, edge.Ver2] = Math.Round(edge.Weight,
                                                                   Consts.Accuracy,
                                                                   MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// Рисование вершины.
        /// </summary>
        /// <param name="x"> x координата вершины </param>
        /// <param name="y"> y координата вершины </param>
        /// <param name="number"> Число в вершине </param>
        internal void DrawVertex(int x, int y, string number)
        {
            cover.FillEllipse(Brushes.White, x - VertexRadius, y - VertexRadius,
                2 * VertexRadius, 2 * VertexRadius);

            cover.DrawEllipse(blackPen, x - VertexRadius, y - VertexRadius,
                2 * VertexRadius, 2 * VertexRadius);

            var yCoordinate = y - VertexRadius / 2;
            var xCoordinate = x - VertexRadius / 2;

            // Определям положение числа в вершине в зависимости от числа его знаков.
            point = int.Parse(number) < 10
                ? new PointF(xCoordinate + 2, yCoordinate)
                : new PointF(xCoordinate - 3, yCoordinate);

            // Рисуем число в вершине.
            cover.DrawString(number, font, brush, point);
        }

        internal void DrawPoint(float x, float y)
        {
            cover.FillEllipse(Brushes.Blue,
                x - PointRadius / 2.0f, y - PointRadius / 2.0f, PointRadius, PointRadius);

            cover.DrawEllipse(greenPen,
                x - PointRadius / 2.0f, y - PointRadius / 2.0f, PointRadius, PointRadius);
        }

        /// <summary>
        /// Рисование ребра.
        /// </summary>
        /// <param name="vertex1"> Первая вершина </param>
        /// <param name="vertex2"> Вторая вершина </param>
        /// <param name="edge"> Список рёбер </param>
        internal void DrawEdge(Vertex vertex1, Vertex vertex2, Edge edge)
        {
            // Если петля, то рисуем дугу.
            if (edge.Ver1 == edge.Ver2)
            {
                cover.DrawArc(greenPen, vertex1.X - 2 * VertexRadius, vertex1.Y - 2 * VertexRadius,
                    2 * VertexRadius, 2 * VertexRadius, 90, 270);

                // Дорисовываем наконечник.
                cover.DrawLine(greenPen,
                    vertex1.X - 7, vertex1.Y - VertexRadius - 7,
                    vertex1.X, vertex1.Y - VertexRadius);

                cover.DrawLine(greenPen,
                    vertex1.X + 7, vertex1.Y - VertexRadius - 7,
                    vertex2.X, vertex2.Y - VertexRadius);

                // Заново отображаем вершину.
                DrawVertex(vertex1.X, vertex1.Y, (edge.Ver1 + 1).ToString());
            }
            // Если это не петля, то рисуем стрелку.
            else
            {
                DrawArrow(vertex1, vertex2);
                DrawVertex(vertex1.X, vertex1.Y, (edge.Ver1 + 1).ToString());
                DrawVertex(vertex2.X, vertex2.Y, (edge.Ver2 + 1).ToString());
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
                k = MyMath.GetK(ver1, ver2);
                b = MyMath.GetB(ver1, ver2);

                // Вычисляем промежуточную величину.
                var sqrtDiscriminant2 = MyMath.GetSqrtOfDiscriminant(ver2, k, b);

                // Мы ищем точки пересечения прямой, проходящей через 2 данные веришны,
                // и окружности, которая описана около веришины. Их будет 2.
                // Причём одна находится ближе к первой вершине (xMin, yMin),
                // а другая дальше (xMax, yMax).

                // Ищем соответсвующие точки, и получаем координаты xEnd, yEnd
                // в зависимости от расположения вершин.
                var xMaxEnd = (sqrtDiscriminant2 + ver2.X + k * (ver2.Y - b)) / (k * k + 1);
                var xMinEnd = (-sqrtDiscriminant2 + ver2.X + k * (ver2.Y - b)) / (k * k + 1);

                var yMinEnd = k * xMaxEnd + b;
                var yMaxEnd = k * xMinEnd + b;

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
                var sqrtDiscriminant1 = MyMath.GetSqrtOfDiscriminant(ver1, k, b);

                var xMaxBegin = (sqrtDiscriminant1 + ver1.X + k * (ver1.Y - b)) / (k * k + 1);
                var xMinBegin = (-sqrtDiscriminant1 + ver1.X + k * (ver1.Y - b)) / (k * k + 1);

                var yMinBegin = k * xMaxBegin + b;
                var yMaxBegin = k * xMinBegin + b;

                var beginPoint = GetXBeginAndYBegin(ver1, ver2, xMaxBegin, yMaxBegin, xMinBegin, yMinBegin);

                xBegin = beginPoint.X;
                yBegin = beginPoint.Y;

            }
            // Случай, когда у вершин одинаковые x коориднаты.
            else
            {
                xBegin = xEnd = ver1.X;

                if (ver1.Y > ver2.Y)
                {
                    yBegin = ver1.Y - VertexRadius;
                    yEnd = ver2.Y + VertexRadius;
                }
                else
                {
                    yBegin = ver1.Y + VertexRadius;
                    yEnd = ver2.Y - VertexRadius;
                }
            }


            var (firstPoint, secondPoint) = GetArrowHeadPoints(new PointF((float)xBegin, (float)yBegin),
                new PointF((float)xEnd, (float)yEnd), k);

            var firstEndArrowX = firstPoint.X;
            var firstEndArrowY = firstPoint.Y;
            var secondEndArrowX = secondPoint.X;
            var secondEndArrowY = secondPoint.Y;

            // Рисуем ребро + наконечник.
            DrawArrowhead(xBegin, yBegin, xEnd, yEnd, firstEndArrowX, firstEndArrowY, secondEndArrowX, secondEndArrowY);
        }

        private PointF GetXBeginAndYBegin(Vertex ver1, Vertex ver2, double xMaxBegin,
            double yMaxBegin, double xMinBegin, double yMinBegin)
        {

            double xBegin = 0;
            double yBegin = 0;

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

            return new PointF((float)xBegin, (float)yBegin);
        }

        private (PointF, PointF) GetArrowHeadPoints(PointF pointBegin, PointF pointEnd, double k)
        {
            var (xBegin, yBegin) = (pointBegin.X, pointBegin.Y);
            var (xEnd, yEnd) = (pointEnd.X, pointEnd.Y);

            var xCoordinate = MyMath.GetXCoordinate(pointBegin, pointEnd);
            var yCoordinate = MyMath.GetYCoordinate(pointBegin, pointEnd);

            // Две точки с координатами наконечника.
            var firstEndArrowX = xCoordinate + HeightArrow / Math.Sqrt((1 + 1 / (k * k)));
            var firstEndArrowY = -1 / k * firstEndArrowX + yCoordinate + xCoordinate / k;

            var secondEndArrowX = xCoordinate - HeightArrow / Math.Sqrt((1 + 1 / (k * k)));
            var secondEndArrowY = -1 / k * secondEndArrowX + yCoordinate + xCoordinate / k;

            PointF p1 = new PointF((float)firstEndArrowX, (float)firstEndArrowY);
            PointF p2 = new PointF((float)secondEndArrowX, (float)secondEndArrowY);

            (PointF firstPoint, PointF secondPoint) arrowHeadPoints = (p1, p2);
            // В зависимости от расположения вершин, ищем эти координаты.
            if (xBegin == xEnd)
            {
                arrowHeadPoints = GetEndOfArrowHead2(yEnd, yBegin, xEnd);
            }
            else if (yBegin == yEnd)
            {
                arrowHeadPoints = GetEndOfArrowHead(xBegin, xEnd, yEnd);
            }

            return arrowHeadPoints;
        }

        private (PointF, PointF) GetEndOfArrowHead(double xBegin, double xEnd, double yEnd)
        {
            double firstEndArrowX;
            double firstEndArrowY;
            double secondEndArrowX;
            double secondEndArrowY;

            if (xBegin < xEnd)
            {
                firstEndArrowX = xEnd - WidthArrow;
                firstEndArrowY = yEnd - HeightArrow;

                secondEndArrowX = xEnd - WidthArrow;
                secondEndArrowY = yEnd + HeightArrow;
            }
            else
            {
                firstEndArrowX = xEnd + WidthArrow;
                firstEndArrowY = yEnd + HeightArrow;

                secondEndArrowX = xEnd + WidthArrow;
                secondEndArrowY = yEnd - HeightArrow;
            }

            return (new PointF((float)firstEndArrowX, (float)firstEndArrowY),
                    new PointF((float)secondEndArrowX, (float)secondEndArrowY));
        }

        private (PointF, PointF) GetEndOfArrowHead2(double yEnd, double yBegin, double xEnd)
        {
            double firstEndArrowX;
            double firstEndArrowY;
            double secondEndArrowX;
            double secondEndArrowY;

            if (yBegin < yEnd)
            {
                firstEndArrowX = xEnd - HeightArrow;
                firstEndArrowY = yEnd - WidthArrow;

                secondEndArrowX = xEnd + HeightArrow;
                secondEndArrowY = yEnd - WidthArrow;
            }
            else
            {
                firstEndArrowX = xEnd - HeightArrow;
                firstEndArrowY = yEnd + WidthArrow;

                secondEndArrowX = xEnd + HeightArrow;
                secondEndArrowY = yEnd + WidthArrow;
            }

            return (new PointF((float)firstEndArrowX, (float)firstEndArrowY),
                    new PointF((float)secondEndArrowX, (float)secondEndArrowY));
        }

        private void DrawArrowhead(double xBegin, double yBegin, double xEnd, double yEnd, double firstEndArrowX,
            double firstEndArrowY, double secondEndArrowX, double secondEndArrowY)
        {
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

        private static void CheckNearVertex(ref int i, ref List<Vertex> vertex, Point currentPoint)
        {
            var hasNearOtherVertex = true;

            // Если это первая веришна, то просто добавляем её.
            if (i == 0)
            {
                vertex.Add(new Vertex(currentPoint.X, currentPoint.Y));
                return;
            }
            // Если это уже не первая вершина, то проверяем,
            // чтобы рядом не было других
            if (vertex.Any(ver => Math.Abs(ver.X - currentPoint.X) < 2 * VertexRadius &&
                                  Math.Abs(ver.Y - currentPoint.Y) < 2 * VertexRadius))
            {
                i--;
                hasNearOtherVertex = false;
            }

            // Если рядом нет других, то добавляем новую вершину.
            if (hasNearOtherVertex)
            {
                vertex.Add(new Vertex(currentPoint.X, currentPoint.Y));
            }
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
            var vertex = new List<Vertex>();

            for (var i = 0; i < adjMatrix.GetLength(0); i++)
            {
                var xCoordinate = Rnd.Next(2 * VertexRadius, maxX + 1);
                var yCoordinate = Rnd.Next(2 * VertexRadius, maxY + 1);

                CheckNearVertex(ref i, ref vertex, new Point(xCoordinate, yCoordinate));
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
            var edges = new List<Edge>();

            // Весом каждого ребра служит значение из матрицы смежности.
            for (var i = 0; i < adjMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < adjMatrix.GetLength(1); j++)
                {
                    if (Math.Abs(adjMatrix[i, j]) > double.Epsilon)
                    {
                        edges.Add(new Edge(i, j, adjMatrix[i, j]));
                    }
                }
            }

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
            {
                DrawEdge(vertex[edge.Ver1], vertex[edge.Ver2], edge);
            }

            for (var i = 0; i < vertex.Count; i++)
            {
                DrawVertex(vertex[i].X, vertex[i].Y, (i + 1).ToString());
            }
        }

        /// <summary>
        /// Получаем пару индексов вершин.
        /// </summary>
        /// <param name="adjacencyMatrix"> Матрица смежности </param>
        /// <returns> Кортеж вершин </returns>
        private static (int, int) GetRandomFreePair(double[,] adjacencyMatrix)
        {
            var freePairsOfVertex = new List<(int, int)>();

            for (var i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < adjacencyMatrix.GetLength(0); j++)
                {
                    if (Math.Abs(adjacencyMatrix[i, j]) < double.Epsilon)
                    {
                        freePairsOfVertex.Add((i, j));
                    }
                }
            }

            var randomIndexOfList = Rnd.Next(freePairsOfVertex.Count);

            return freePairsOfVertex[randomIndexOfList];
        }

        /// <summary>
        /// Задаём рёбрам случайный вес.
        /// </summary>
        /// <param name="adjMatrix"> Матрица смежности </param>
        /// <returns> Матрица смежности </returns>
        internal double[,] SetDistanceEdge(double[,] adjMatrix)
        {
            // Min и Max значения веса рёбер.
            var min = 1;
            var max = 999;

            // Пробегаемся по всем элементам и заполняем матрицу.
            for (int i = 0; i < adjMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < adjMatrix.GetLength(1); j++)
                {
                    if (adjMatrix[i, j] == 1)
                        adjMatrix[i, j] =
                            Rnd.Next(min, max) + Rnd.NextDouble() +
                            Rnd.Next(2) * double.Epsilon;
                }

            }

            return adjMatrix;
        }

        internal List<Edge> GetOtherGraphWithGivenAmountOfEdgesAndVertex(List<Vertex> vertex, List<Edge> edges)
        {
            var size = vertex.Count;
            var matrix = new double[size, size];
            List<Edge> edgesForChoice = edges.GetRange(0, edges.Count);
            var curVertexIndex = 0;

            var newEdges = new List<Edge>(size);
            var counter = 1;

            var randomVertexIndex = Rnd.Next(vertex.Count);
            if (vertex.Count == 1)
            {
                return edgesForChoice;
            }

            while (curVertexIndex == randomVertexIndex)
            {
                randomVertexIndex = Rnd.Next(vertex.Count);
            }

            var randomEdge = edgesForChoice[Rnd.Next(edgesForChoice.Count)];
            newEdges.Add(new Edge(curVertexIndex, randomVertexIndex, randomEdge.Weight));
            edgesForChoice.Remove(randomEdge);


            matrix[curVertexIndex, randomVertexIndex] = 1;

            curVertexIndex = randomVertexIndex;

            while (counter < vertex.Count - 1)
            {
                randomVertexIndex = Rnd.Next(vertex.Count);

                while (curVertexIndex == randomVertexIndex || newEdges.Any(e => e.Ver1 == randomVertexIndex))
                {
                    randomVertexIndex = Rnd.Next(vertex.Count);
                }


                randomEdge = edgesForChoice[Rnd.Next(edgesForChoice.Count)];
                newEdges.Add(new Edge(curVertexIndex, randomVertexIndex, randomEdge.Weight));
                edgesForChoice.Remove(randomEdge);

                matrix[curVertexIndex, randomVertexIndex] = 1;

                curVertexIndex = randomVertexIndex;

                counter++;
            }

            counter++;

            var expectedSum = (newEdges.Count) * (newEdges.Count + 1) / 2;
            var index1 = expectedSum - newEdges.Sum(e => e.Ver1);
            var index2 = expectedSum - newEdges.Sum(e => e.Ver2);

            randomEdge = edgesForChoice[Rnd.Next(edgesForChoice.Count)];
            newEdges.Add(new Edge(index1, index2, randomEdge.Weight));
            edgesForChoice.Remove(randomEdge);

            matrix[index1, index2] = 1;

            while (counter < edges.Count)
            {
                var (vertexIndex1, vertexIndex2) = GetRandomFreePair(matrix);
                matrix[vertexIndex1, vertexIndex2] = 1;
                randomEdge = edgesForChoice[Rnd.Next(edgesForChoice.Count)];
                newEdges.Add(new Edge(vertexIndex1, vertexIndex2, randomEdge.Weight));
                edgesForChoice.Remove(randomEdge);

                counter++;
            }

            return newEdges;
        }

        private static double[,] SpecialCaseAdjacencyMatrix(int size)
        {
            var adjacencyMatrix = new double[size, size];

            switch (size)
            {
                case 1:
                    adjacencyMatrix[0, 0] = Rnd.Next(2);
                    return adjacencyMatrix;

                case 2:
                    adjacencyMatrix[1, 0] = 1;
                    adjacencyMatrix[0, 1] = 1;
                    adjacencyMatrix[0, 0] = Rnd.Next(2);
                    adjacencyMatrix[1, 1] = Rnd.Next(2);
                    return adjacencyMatrix;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void FillAdjacencyMatrix(ref double[,] adjacencyMatrix, int i)
        {
            var size = adjacencyMatrix.GetLength(0);

            if (i == size - 1)
            {
                adjacencyMatrix[i, 0] = 1;
            }
            else
            {
                adjacencyMatrix[i, i + 1] = 1;
            }
        }

        private static double[,] GetAdjacencyMatrix(int size)
        {
            var adjacencyMatrix = new double[size, size];

            // Проводим рёбра по кругу (1 -> 2 -> 3 -> ... -> n-1 -> n -> 1)
            for (var i = 0; i < size; i++)
            {
                FillAdjacencyMatrix(ref adjacencyMatrix, i);
            }

            // Из каждой веришны проводим ещё одно ребро случайным образом.
            for (var i = 0; i < size; i++)
            {
                adjacencyMatrix[i, Rnd.Next(size)] = 1;
            }

            // Случайным образом заполняем диагональ.
            for (var i = 0; i < size; i++)
            {
                adjacencyMatrix[i, i] = Rnd.Next(2);
            }

            return adjacencyMatrix;
        }

        /// <summary>
        /// Заполняем матрицу смежности случайным образом.
        /// При этом граф должен получиться сильносвязным.
        /// </summary>
        /// <param name="size"> Размер матрицы </param>
        /// <returns> Матрица смежности </returns>
        internal double[,] GetRandomAdjacencyMatrix(int size)
        {
            if (size == 1 || size == 2)
            {
                return SpecialCaseAdjacencyMatrix(size);
            }

            double[,] adjacencyMatrix = GetAdjacencyMatrix(size);

            return adjacencyMatrix;
        }

        /// <summary>
        /// Возвращаем изменения на холсте.
        /// </summary>
        /// <returns> холст </returns>
        internal Bitmap GetBitmap() => bitmap;

        /// <summary>
        /// Очистка холста.
        /// </summary>
        internal void ClearField() => 
        cover.Clear(Color.SlateGray);

        /// <summary>
        /// Выделение вершины.
        /// </summary>
        /// <param name="x"> x координата вершины </param>
        /// <param name="y"> y координата вершины </param>
        internal void DrawSelectedVertex(int x, int y) =>
            cover.DrawEllipse(redPen, x - VertexRadius, y - VertexRadius, 2 * VertexRadius, 2 * VertexRadius);
    }
}