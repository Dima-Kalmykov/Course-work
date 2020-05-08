using System;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Статический класс для различных вычислений.
    /// </summary>
    internal static class Calculate
    {
        // Радиус вершины.
        private const int VertexRadius = Consts.VertexRadius;

        // Ширина наконечника стрелки.
        private const int WidthArrow = Consts.WidthArrow;

        /// <summary>
        /// Вычисление расстояния между двумя точками.
        /// </summary>
        /// <param name="x1"> x координата первой точки </param>
        /// <param name="y1"> y координата первой точки </param>
        /// <param name="x2"> x координата второй точки </param>
        /// <param name="y2"> y координата второй точки </param>
        /// <returns> Расстояние между двумя точками </returns>
        internal static double GetDistance(double x1, double y1, double x2, double y2) =>
            Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

        /// <summary>
        /// Вычисление коэффициента k прямой, проходящей через две точки.
        /// </summary>
        /// <param name="ver1"> Первая вершина </param>
        /// <param name="ver2"> Вторая вершина </param>
        /// <returns> Коэффициент k </returns>
        internal static double GetK(Vertex ver1, Vertex ver2) =>
           (double)(ver2.Y - ver1.Y) / (ver2.X - ver1.X);

        /// <summary>
        /// Вычисление члена b прямой, проходящей через две точки.
        /// </summary>
        /// <param name="ver1"> Первая вершина </param>
        /// <param name="ver2"> Вторая вершина </param>
        /// <returns> Член b </returns>
        internal static double GetB(Vertex ver1, Vertex ver2) =>
            (double)(ver2.X * ver1.Y - ver1.X * ver2.Y) / (ver2.X - ver1.X);

        /// <summary>
        /// Вспомогательный метод, для поиска точки пересечения прямой и окружности.
        /// </summary>
        /// <param name="ver"> координаты центра окружности </param>
        /// <param name="k"> Наклонный коэффициент прямой y = kx + b</param>
        /// <param name="b"> Член b прямой y = kx + b </param>
        /// <returns> Дискриминант квадратного уравнения с заданными коэффициентами </returns>
        internal static double GetSqrtOfDiscriminant(Vertex ver, double k, double b) =>
           Math.Sqrt(VertexRadius * VertexRadius * (1 + k * k) - ver.Y * ver.Y + 2 * b * ver.Y -
            b * b + 2 * k * ver.X * (ver.Y - b) - k * k * ver.X * ver.X);

        /// <summary>
        /// Вычисление количества знаков числа, включая запятую.
        /// </summary>
        /// <param name="number"> xЧисло </param>
        /// <returns> Количество знаков числа, с учётом запятой </returns>
        internal static int GetDigits(double number)
        {
            // Флаг для отбрасывания нулей дробной части,
            // после того, как мы перевели дробную в целое число. 
            bool zeroAtEnd = true;

            // Число знаков дробной части.
            int digitsFraction = 0;

            // Число знаков целой части.
            int result = 0;

            // Выделяем у числа дробную часть. 
            double fraction = (number - Math.Truncate(number));

            // Оставляем только три знака после запятой.
            fraction = Math.Round(fraction, 3, MidpointRounding.AwayFromZero);

            // Переводим дробную часть в целое число.
            fraction *= 10000;

            // Если дробная часть была вида 0,0xx.
            if (fraction < 1000 && fraction > 99)
                digitsFraction++;
            // Если дробная часть была вида 0,00x.
            else if (fraction < 100 && fraction > 0)
                digitsFraction += 2;

            // fraction могло быть равно 2, но хранится как 1.999,
            // поэтому необходимо перед подсчётом отбросить все знаки после запятой.
            fraction = Math.Round(fraction, 0, MidpointRounding.AwayFromZero);

            // Отбрасываем все нули справа, т.к.
            // число могло иметь меньше 3 знаков после запятой.
            // После чего считаем число знаков дробной части.
            while ((int)fraction > 0)
            {
                if (fraction % 10 == 0 && zeroAtEnd)
                {
                    fraction /= 10;
                    continue;
                }

                fraction /= 10;
                zeroAtEnd = false;

                digitsFraction++;
            }

            // Считаем число знаков целой части числа.
            if (Math.Abs(number) > double.Epsilon)
            {
                result = (int)Math.Log10(number) + 1;
            }
            // Если число было дробным, то
            // возвращаем сумму числа знаков дробной и целой частей + запятую.
            if (digitsFraction != 0)
                return result + digitsFraction + 1;

            // Если число было целым, то
            // возвращаем только число знаков целой части.
            return result;
        }

        /// <summary>
        /// Поиск x координаты точки, делящей отрезок в заданном отношении.
        /// </summary>
        /// <param name="x1"> x координата первой точки </param>
        /// <param name="y1"> y координата первой точки </param>
        /// <param name="x2"> x координата второй точки </param>
        /// <param name="y2"> y координата второй точки </param>
        /// <returns> x координата точки, делящей отрезок в заданном отношении </returns>
        internal static double GetXCoordinate(double x1, double y1, double x2, double y2)
        {
            // Отношение, в котором делится отрезок точкой.
            var delta = (GetDistance(x1, y1, x2, y2) - WidthArrow) / WidthArrow;

            return (x1 + delta * x2) / (1 + delta);
        }

        /// <summary>
        /// Поиск y координаты точки, делящей отрезок в заданном отношении.
        /// </summary>
        /// <param name="x1"> x координата первой точки </param>
        /// <param name="y1"> y координата первой точки </param>
        /// <param name="x2"> x координата второй точки </param>
        /// <param name="y2"> y координата второй точки </param>
        /// <returns> y координата точки, делящей отрезок в заданном отношении </returns>
        internal static double GetCoordOy(double x1, double y1, double x2, double y2)
        {
            // Отношение, в котором делится отрезок точкой.
            var delta = (GetDistance(x1, y1, x2, y2) - WidthArrow) / WidthArrow;

            return (y1 + delta * y2) / (1 + delta);
        }
    }
}