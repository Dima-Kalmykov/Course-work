using MetroFramework;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class ChartForOpenFromFileForm : MetroFramework.Forms.MetroForm
    {
        public ChartForOpenFromFileForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLoad(object sender, System.EventArgs e)
        {
            Style = MetroColorStyle.Magenta;
            chart1.Legends[0].BackColor = Color.SlateGray;
            chart1.Series["Amount of points"].Color = Color.Yellow;
            chart1.ChartAreas["ChartArea1"].BackColor = Color.SlateGray;

            chart1.BackColor = Color.SlateGray;
            Width = Consts.ChartFormWidth;
            Height = Consts.ChartFormHeight;
            chart1.Width = Width;
            chart1.Height = Height - 1;

            chart1.Location = new Point(0, 5);
        }
    }
}
