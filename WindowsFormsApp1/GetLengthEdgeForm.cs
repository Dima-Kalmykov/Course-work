using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class GetLengthEdgeForm : Form
    {
        // Флаг, который показывает, нажали мы кнопку отмены, или нет.
        internal bool cancel;

        // Итоговое число.
        internal double number;

        internal GetLengthEdgeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработка введённого текста,
        /// и получение итогового числа number.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            // Строка, которую при возможности преобразуем в число.
            string strForParse = GetNumberTextBox.Text;

            // Смотрим, не введена ли какая-то константа. 
            if (GetNumberTextBox.Text == "E" || GetNumberTextBox.Text == "e")
                number = Math.E;
            else if (GetNumberTextBox.Text == "Pi" || GetNumberTextBox.Text == "pi")
                number = Math.PI;
            else
            {
                // Случай, конда ничего не ввели.
                if (GetNumberTextBox.TextLength == 0)
                {
                    MessageBox.Show("Введите положительное число в диапазоне [1;999]");
                    GetNumberTextBox.Text = "";
                    GetNumberTextBox.Focus();
                    return;
                }

                // Случай, когда введено ключевое слово sqrt.
                // Тут мы выделяем то, что внутри sqrt и начинаем обработку.
                if (GetNumberTextBox.TextLength > 5)
                    if (GetNumberTextBox.Text.Substring(0, 5) == "Sqrt(" ||
                        GetNumberTextBox.Text.Substring(0, 5) == "sqrt(")
                        if (GetNumberTextBox.Text[GetNumberTextBox.TextLength - 1] == ')')
                            strForParse = GetNumberTextBox.Text.Substring(5,
                                GetNumberTextBox.TextLength - 6);

                // Случай, когда ничего внутри sqrt нет.
                if (strForParse.Length == 0)
                {
                    MessageBox.Show("Введите положительное число в диапазоне [1;999]");
                    GetNumberTextBox.Text = "";
                    GetNumberTextBox.Focus();
                    return;
                }

                // Случай, когда внутри sqrt какая-то константа.
                if (strForParse == "E" || strForParse == "e")
                    number = Math.E;
                else if (strForParse == "Pi" || strForParse == "pi")
                    number = Math.PI;
                else
                {
                    // Случай, когда число не начинается с цифры, или начинается 0,
                    // или заканчивается не цифрой.
                    if (!(char.IsDigit(strForParse[0])) || strForParse[0] == '0' ||
                        !(char.IsDigit(strForParse[strForParse.Length - 1])))
                    {
                        MessageBox.Show("Введите положительное число в диапазоне [1;999]");
                        GetNumberTextBox.Text = "";
                        GetNumberTextBox.Focus();
                        return;
                    }

                    // Случай, когда в числе есть символы,
                    // отличные от цифр, точек и запятых.
                    for (int i = 0; i < strForParse.Length; i++)
                    {
                        if (!(char.IsDigit(strForParse[i]) ||
                              strForParse[i] == ',' || strForParse[i] == '.'))
                        {
                            MessageBox.Show("Введите положительное число в диапазоне [1;999]");
                            GetNumberTextBox.Text = "";
                            GetNumberTextBox.Focus();
                            return;
                        }
                    }

                    // Вспомогательный массив, чтобы поменять точку на запятую.
                    char[] tempArr = strForParse.ToCharArray();

                    // Меняем точки на запятые.
                    for (int i = 0; i < tempArr.Length; i++)
                    {
                        if (tempArr[i] == '.')
                            tempArr[i] = ',';
                    }

                    // Записываем результат (после того, как мы поменяли точки на запятые).
                    strForParse = new string(tempArr);

                    // Пытаемся преобразовать получившуюся строку к double.
                    if (!double.TryParse(strForParse, out number))
                    {
                        MessageBox.Show("Введите положительное число в диапазоне [1;999]");
                        GetNumberTextBox.Text = "";
                        GetNumberTextBox.Focus();
                        return;
                    }

                    // Случай, когда число не входит в диапазон.
                    if (number < 1 || number > 999)
                    {
                        MessageBox.Show("Введите положительное число в диапазоне [1;999]");
                        GetNumberTextBox.Text = "";
                        GetNumberTextBox.Focus();
                        return;
                    }
                }

                // Если было введено sqrt, то возвращаем корень из числа.
                if (GetNumberTextBox.TextLength > 5)
                    if (GetNumberTextBox.Text[1] == 'q')
                        number = Math.Sqrt(number);
            }

            cancel = false;

            GetNumberTextBox.Text = "";
            GetNumberTextBox.Focus();
            Close();
        }

        /// <summary>
        /// Устанавливаем нужные параметры флага,
        /// и перед выходом  очищаем TextBox и
        /// оставляем на нём фокус для последующего ввода.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            cancel = true;

            GetNumberTextBox.Text = "";
            GetNumberTextBox.Focus();
            Close();
        }

        /// <summary>
        /// Убираем верхнюю панель.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetLengthEdgeForm_Load(object sender, EventArgs e) =>
            ControlBox = false;
    }
}