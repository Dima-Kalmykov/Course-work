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
    /// Форма оповещения пользователя о сильносвязности графа,
    /// и предлагающая варианты развития событий.
    /// </summary>
    public partial class CheckGraphForStronglyConnectionForm : Form
    {
        // Флаг для определения того, нужно генерировать граф, или нет.
        internal bool generate;

        internal CheckGraphForStronglyConnectionForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Закрываем форму, и ставим в generate значение true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateGraphButton_Click(object sender, EventArgs e)
        {
            generate = true;
            Close();
        }

        /// <summary>
        /// Закрываем форму, и ставим в generate значение false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContinueButton_Click(object sender, EventArgs e)
        {
            generate = false;
            Close();
        }

        /// <summary>
        /// Убираем верхнюю панель.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckGraphForStronglyConnection_Load(object sender, EventArgs e) =>
            ControlBox = false;
    }
}