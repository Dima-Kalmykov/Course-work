using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Форма для выбора ребра, если существует обратное к данному.
    /// </summary>
    public partial class ChooseEdgeForm : Form
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

        /// <summary>
        /// Убираем верхнюю панель.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseEdgeForm_Load(object sender, EventArgs e) =>
            ControlBox = false;

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