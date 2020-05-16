using Dangl.Calculator;
using System;
using System.Windows.Forms;
using MetroFramework;

namespace WindowsFormsApp1
{
    public partial class GetLengthEdgeForm : MetroFramework.Forms.MetroForm
    {
        // Флаг, который показывает, нажали мы кнопку отмены, или нет.
        internal bool WasCancel;

        // Итоговое число.
        internal double Weight;

        internal GetLengthEdgeForm()
        {
            InitializeComponent();
        }

        private static bool IsNotSuitableWeight(double weight) =>
                   weight < Consts.MinEdgeWeight ||
                   weight > Consts.MaxEdgeWeight ||
                   double.IsInfinity(weight) ||
                   double.IsNaN(weight);

        private void ShowErrorMessage()
        {
            var myMessageBox = new MyMessageBox();
            myMessageBox.ShowNumberInRange("Number must be in range\n" +
                                           "          [0.0001; 10^4]");
            GetNumberTextBox.Text = string.Empty;
            GetNumberTextBox.Focus();
        }

        public (DialogResult, bool, double) MyShow(bool isAlpha = false)
        {
            if (!isAlpha)
            {
                var gl = new GetLengthEdgeForm
                {
                    Text = "Get edge weight",
                    WasCancel = false,
                    Weight = 0,
                    TextForUnderstandingLabel = {Text = "Enter edge weight"}
                };
                return (gl.ShowDialog(), gl.WasCancel, gl.Weight);
            }
            else
            {
                var gl = new GetLengthEdgeForm
                {
                    Text = "Get alpha value",
                    WasCancel = false,
                    Weight = 0,
                    TextForUnderstandingLabel = {Text = "Enter alpha value"},
                };

                return (gl.ShowDialog(), gl.WasCancel, gl.Weight);
            }
        }

        /// <summary>
        /// Обработка введённого текста,
        /// и получение итогового числа number.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            try
            {
                Weight = Calculator.Calculate(GetNumberTextBox.Text).Result;

                if (IsNotSuitableWeight(Weight))
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                ShowErrorMessage();
                return;
            }

            WasCancel = false;

            GetNumberTextBox.Text = string.Empty;
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
            WasCancel = true;

            GetNumberTextBox.Text = string.Empty;
            GetNumberTextBox.Focus();
            Close();
        }

        /// <summary>
        /// Убираем верхнюю панель.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetLengthEdgeForm_Load(object sender, EventArgs e)
        {
            Theme = MetroThemeStyle.Dark;
            ControlBox = false;
        }
    }
}