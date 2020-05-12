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
        public bool WasCancel;

        // Флаг, который показывает, какой вариант мы выбрали.
        public bool IsFirstAction;

        public ChooseEdgeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Устанавливаем параметры флагов и закрываем форму.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstActionButton_Click(object sender, EventArgs e)
        {
            IsFirstAction = true;
            WasCancel = false;

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
            WasCancel = false;

            Close();
        }

        public (DialogResult, bool, bool) MyShowChangeLength(string caption, string text1, string text2)
        {
            var cf = new ChooseEdgeForm
            {
                Width = 280,
                Height = 310,
                TextForUnderstandingLabel = { Text = caption },
                WasCancel = false,
                IsFirstAction = false,
                FirstOptionButton = { Text = text1, Location = new Point(45, 135) },
                SecondOptionButton = { Text = text2, Location = new Point(45, 190) },
                CancelButton = { Location = new Point(45, 245) },
            };

            var res = cf.ShowDialog();

            return (res, cf.WasCancel, cf.IsFirstAction);
        }

        public (DialogResult, bool, bool) MyShow(string caption, string text1, string text2)
        {
            var cf = new ChooseEdgeForm
            {
                Width = 300,
                Height = 290,
                TextForUnderstandingLabel = { Text = caption },
                WasCancel = false,
                IsFirstAction = false,
                FirstOptionButton = { Text = text1, Location = new Point(55, 115) },
                SecondOptionButton = { Text = text2, Location = new Point(55, 170) },
                CancelButton = { Location = new Point(55, 225) },
            };

            var res = cf.ShowDialog();

            return (res, cf.WasCancel, cf.IsFirstAction);
        }

        /// <summary>
        /// Убираем верхнюю панель.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseEdgeForm_Load(object sender, EventArgs e)
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
        private void CancelButton_Click(object sender, EventArgs e)
        {
            WasCancel = true;

            Close();
        }
    }
}