using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Chart : Form
    {
        public Chart()
        {
            InitializeComponent();
        }

        private void Chart_Load(object sender, EventArgs e)
        {
            Width = Consts.ChartFormWidth;
            Height = Consts.ChartFormHeight;
            chartForm.Width = Width;
            chartForm.Height = Height - 15;

            chartForm.Location = new Point(0, 0);
        }
    }
}
