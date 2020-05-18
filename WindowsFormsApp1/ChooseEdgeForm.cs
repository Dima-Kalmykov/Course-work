using MetroFramework;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Форма для выбора ребра, если существует обратное к данному.
    /// </summary>
    public partial class ChooseEdgeForm : MetroFramework.Forms.MetroForm
    {
        // Флаг, который показывает, нажали мы кнопку отмены, или нет.
        public bool WasCanceled;

        // Флаг, который показывает, какой вариант мы выбрали.
        public bool IsFirstAction;

        public ChooseEdgeForm()
        {
            InitializeComponent();
        }

        public (DialogResult, bool, bool) MyShowChangeLength(string caption, 
            string textFirstAction, string textSecondAction)
        {
            var cf = new ChooseEdgeForm
            {
                Width = 280,
                Height = 310,
                TextForUnderstandingLabel = { Text = caption },
                WasCanceled = false,
                IsFirstAction = false,
                FirstOptionButton = { Text = textFirstAction, Location = new Point(45, 135) },
                SecondOptionButton = { Text = textSecondAction, Location = new Point(45, 190) },
                CancelButton = { Location = new Point(45, 245) },
            };

            var res = cf.ShowDialog();

            return (res, cf.WasCanceled, cf.IsFirstAction);
        }

        public (DialogResult, bool, bool) MyShowDeleteEdge(string caption, 
            string textFirstAction, string textSecondAction)
        {
            var cf = new ChooseEdgeForm
            {
                Width = 300,
                Height = 290,
                TextForUnderstandingLabel = { Text = caption },
                WasCanceled = false,
                IsFirstAction = false,
                FirstOptionButton = { Text = textFirstAction, Location = new Point(55, 115) },
                SecondOptionButton = { Text = textSecondAction, Location = new Point(55, 170) },
                CancelButton = { Location = new Point(55, 225) },
            };

            var res = cf.ShowDialog();

            return (res, cf.WasCanceled, cf.IsFirstAction);
        }

        /// <summary>
        /// Устанавливаем параметры флагов и закрываем форму.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstActionButtonClick(object sender, EventArgs e)
        {
            IsFirstAction = true;
            WasCanceled = false;

            Close();
        }

        /// <summary>
        /// Устанавливаем параметры флагов и закрываем форму.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondActionButton_Click(object sender, EventArgs e)
        {
            IsFirstAction = false;
            WasCanceled = false;

            Close();
        }

        /// <summary>
        /// Убираем верхнюю панель.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLoad(object sender, EventArgs e)
        {
            ControlBox = false;
            Theme = MetroThemeStyle.Dark;
            Style = MetroColorStyle.Blue;
        }

        /// <summary>
        /// Устанавливаем параметры флагов и закрываем форму.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButtonClick(object sender, EventArgs e)
        {
            WasCanceled = true;

            Close();
        }
    }
}