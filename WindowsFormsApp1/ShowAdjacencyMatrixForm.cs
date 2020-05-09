using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Форма для отображения матрицы смежности.
    /// </summary>
    public partial class ShowAdjacencyMatrixForm : MetroFramework.Forms.MetroForm
    {
        internal ShowAdjacencyMatrixForm()
        {
            InitializeComponent();

            AdjacencyMatrixListBox.Width = Width;
            AdjacencyMatrixListBox.Height = Height + 10;
            AdjacencyMatrixListBox.Location = new Point(0, 0);

            // Убираем верхнюю панель.
            AdjacencyMatrixListBox.BackColor = Color.FromArgb(31, 30, 68);
            AdjacencyMatrixListBox.ForeColor = Color.LightGray;
            ControlBox = false;
        }

        /// <summary>
        /// Снятие выделения с текста.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdjacencyMatrixListBox_Click(object sender, EventArgs e) =>
            AdjacencyMatrixListBox.ClearSelected();
    }
}