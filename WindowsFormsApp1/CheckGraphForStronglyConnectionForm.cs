using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Форма оповещения пользователя о сильносвязности графа,
    /// и предлагающая варианты развития событий.
    /// </summary>
    public partial class CheckGraphForStronglyConnectionForm : MetroFramework.Forms.MetroForm
    {
        // Флаг для определения того, нужно генерировать граф, или нет.
        internal bool MustBeGenerated;

        internal CheckGraphForStronglyConnectionForm()
        {
            InitializeComponent();
        }

        public (DialogResult, bool) MyShow()
        {
            var cf = new CheckGraphForStronglyConnectionForm
            {
                Text = "Warning", 
                Caption = {Text = "Graph is not strongly directed"},
                MustBeGenerated = false
            };

            var res = cf.ShowDialog();

            return (res, cf.MustBeGenerated);
        }

        /// <summary>
        /// Закрываем форму, и ставим в generate значение true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateGraphButton_Click(object sender, EventArgs e)
        {
            MustBeGenerated = true;
            Close();
        }

        /// <summary>
        /// Закрываем форму, и ставим в generate значение false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContinueButton_Click(object sender, EventArgs e)
        {
            MustBeGenerated = false;
            Close();
        }

        /// <summary>
        /// Убираем верхнюю панель.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckGraphForStronglyConnection_Load(object sender, EventArgs e)
        {
            GenerateGraphButton.ForeColor = Color.Orange;
            ContinueButton.ForeColor = Color.Orange;

            Style = MetroColorStyle.Orange;
            Theme = MetroThemeStyle.Dark;
            ControlBox = false;
        }
    }
}