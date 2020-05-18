using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    internal static class MyMath
    {
        private const int VertexRadius = Consts.VertexRadius;
        private const int WidthArrow = Consts.WidthArrow;

        internal static double GetDistance(PointF point1, PointF point2)
        {
            var (x1, y1) = (point1.X, point1.Y);
            var (x2, y2) = (point2.X, point2.Y);

            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        internal static double GetK(Vertex ver1, Vertex ver2) =>
           (double)(ver2.Y - ver1.Y) / (ver2.X - ver1.X);

        internal static double GetB(Vertex ver1, Vertex ver2) =>
            (double)(ver2.X * ver1.Y - ver1.X * ver2.Y) / (ver2.X - ver1.X);

        internal static double GetSqrtOfDiscriminant(Vertex ver, double k, double b) =>
           Math.Sqrt(VertexRadius * VertexRadius * (1 + k * k) - ver.Y * ver.Y + 2 * b * ver.Y -
            b * b + 2 * k * ver.X * (ver.Y - b) - k * k * ver.X * ver.X);

        private static void ChangeDigitsFraction(ref int digitsFraction, double fraction)
        {
            // Если дробная часть была вида 0,0xx.
            if (fraction < 1000 && fraction > 99)
            {
                digitsFraction++;
            }
            // Если дробная часть была вида 0,00x.
            else if (fraction < 100 && fraction > 0)
            {
                digitsFraction += 2;
            }
        }

        private static double GetFraction(double number)
        {
            // Выделяем у числа дробную часть. 
            var fraction = number - Math.Truncate(number);

            // Оставляем только три знака после запятой.
            fraction = Math.Round(fraction, Consts.Accuracy, MidpointRounding.AwayFromZero);

            // Переводим дробную часть в целое число.
            fraction *= 10000;

            return fraction;
        }

        private static int GetDigitsFraction(int digitsFraction, double fraction)
        {
            var hasZeroAtEnd = true;
            // Отбрасываем все нули справа, т.к.
            // число могло иметь меньше 3 знаков после запятой.
            // После чего считаем число знаков дробной части.
            while ((int)fraction > 0)
            {
                if (Math.Abs(fraction % 10) < double.Epsilon && hasZeroAtEnd)
                {
                    fraction /= 10;
                    continue;
                }

                fraction /= 10;
                hasZeroAtEnd = false;

                digitsFraction++;
            }

            return digitsFraction;
        }

        internal static int GetAmountOfDigits(double number)
        {
            // Флаг для отбрасывания нулей дробной части,
            // после того, как мы перевели дробную в целое число. 

            // Число знаков дробной части.
            var digitsFraction = 0;

            // Число знаков целой части.
            var result = 0;

            // Выделяем у числа дробную часть. 
            var fraction = GetFraction(number);

            ChangeDigitsFraction(ref digitsFraction, fraction);

            // fraction могло быть равно 2, но хранится как 1.999,
            // поэтому необходимо перед подсчётом отбросить все знаки после запятой.
            fraction = Math.Round(fraction, 0, MidpointRounding.AwayFromZero);

            digitsFraction = GetDigitsFraction(digitsFraction, fraction);

            // Считаем число знаков целой части числа.
            if (Math.Abs(number) > double.Epsilon)
            {
                result = (int)Math.Log10(number) + 1;
            }

            // Если число было дробным, то
            // возвращаем сумму числа знаков дробной и целой частей + запятую.

            // Если число было целым, то
            // возвращаем только число знаков целой части.
            return digitsFraction != 0
                ? result + digitsFraction + 1
                : result;
        }

        internal static double GetXCoordinate(PointF point1, PointF point2)
        {
            var (x1, x2) = (point1.X, point2.X);

            // Отношение, в котором делится отрезок точкой.
            var delta = (GetDistance(point1, point2) - WidthArrow) / WidthArrow;

            return (x1 + delta * x2) / (1 + delta);
        }

        internal static double GetYCoordinate(PointF point1, PointF point2)
        {
            var (y1, y2) = (point1.Y, point2.Y);

            // Отношение, в котором делится отрезок точкой.
            var delta = (GetDistance(point1, point2) - WidthArrow) / WidthArrow;

            return (y1 + delta * y2) / (1 + delta);
        }
    }
}