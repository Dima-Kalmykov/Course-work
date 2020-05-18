using Dangl.Calculator;
using System;
using System.Windows.Forms;
using MetroFramework;

namespace WindowsFormsApp1
{
    public partial class GetWeightEdgeForm : MetroFramework.Forms.MetroForm
    {
        // Флаг, который показывает, нажали мы кнопку отмены, или нет.
        internal bool WasCanceled;

        // Итоговое число.
        internal double Weight;

        internal GetWeightEdgeForm()
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
            myMessageBox.NotifyNumberMustBeInRange("Number must be in range\n" +
                                           "          [0.0001; 10^4]");
            GetNumberTextBox.Text = string.Empty;
            GetNumberTextBox.Focus();
        }

        public (DialogResult, bool, double) MyShow(bool isAlpha = false)
        {
            if (!isAlpha)
            {
                var gl = new GetWeightEdgeForm
                {
                    Text = "Get edge weight",
                    WasCanceled = false,
                    Weight = 0,
                    TextForUnderstandingLabel = {Text = "Enter edge weight"},
                    CancelButton = {Text = "Cancel"}
                };
                return (gl.ShowDialog(), gl.WasCanceled, gl.Weight);
            }
            else
            {
                var gl = new GetWeightEdgeForm
                {
                    Text = "Get alpha value",
                    WasCanceled = false,
                    Weight = 0,
                    TextForUnderstandingLabel = {Text = "Enter alpha value"},
                    CancelButton = {Text = "Default value"}
                };

                return (gl.ShowDialog(), gl.WasCanceled, gl.Weight);
            }
        }

        /// <summary>
        /// Обработка введённого текста,
        /// и получение итогового числа number.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButtonClick(object sender, EventArgs e)
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

            WasCanceled = false;

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
        private void CancelButtonClick(object sender, EventArgs e)
        {
            WasCanceled = true;

            GetNumberTextBox.Text = string.Empty;
            GetNumberTextBox.Focus();
            Close();
        }

        /// <summary>
        /// Убираем верхнюю панель.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLoad(object sender, EventArgs e)
        {
            Theme = MetroThemeStyle.Dark;
            ControlBox = false;
        }
    }
}