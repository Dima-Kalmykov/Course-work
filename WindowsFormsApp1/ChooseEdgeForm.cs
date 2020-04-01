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
    /// <summary>
    /// Форма для выбора ребра, если существует обратное к данному.
    /// </summary>
    public partial class ChooseEdgeForm : Form
    {
        // Флаг, который показывает, нажали мы кнопку отмены, или нет.
        public bool cancel;

        // Флаг, который показывает, какой вариант мы выбрали.
        public bool firstOption;
        public ChooseEdgeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Устанавливаем параметры флагов и закрываем форму.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstOptionButton_Click(object sender, EventArgs e)
        {
            firstOption = true;
            cancel = false;

            Close();
        }

        /// <summary>
        /// Устанавливаем параметры флагов и закрываем форму.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondOptionButton_Click(object sender, EventArgs e)
        {
            firstOption = false;
            cancel = false;

            Close();
        }

        /// <summary>
        /// Убираем верхнюю панель.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseEdgeForm_Load(object sender, EventArgs e)=>
            ControlBox = false;

        /// <summary>
        /// Устанавливаем параметры флагов и закрываем форму.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            cancel = true;

            Close();
        }
    }
}