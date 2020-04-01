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
    /// Форма для отображения матрицы смежности.
    /// </summary>
    public partial class ShowAdjacencyMatrixForm : Form
    {
        internal ShowAdjacencyMatrixForm()
        {
            InitializeComponent();

            // Убираем верхнюю панель.
            ControlBox = false;
        }

        /// <summary>
        /// Снятие выделения с текста.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdjMatrixListBox_Click(object sender, EventArgs e) =>
            AdjMatrixListBox.ClearSelected();
    }
}