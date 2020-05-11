using System.Drawing;
using System.Windows.Forms;
using MetroFramework;

namespace WindowsFormsApp1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
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
