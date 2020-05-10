using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class Chart : MetroFramework.Forms.MetroForm
    {
        public Chart()
        {
            InitializeComponent();
        }

        private void Chart_Load(object sender, EventArgs e)
        {
            chartForm.Legends[0].BackColor = Color.SlateGray;
            chartForm.Series["Amount of points"].Color = Color.Yellow;
            chartForm.ChartAreas["ChartArea1"].BackColor = Color.SlateGray;

            chartForm.BackColor = Color.SlateGray;
            Width = Consts.ChartFormWidth;
            Height = Consts.ChartFormHeight;
            chartForm.Width = Width;
            chartForm.Height = Height - 1;

            chartForm.Location = new Point(0, 5);
        }
    }
}
