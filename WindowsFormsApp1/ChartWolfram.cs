using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;

namespace WindowsFormsApp1
{
    public partial class ChartWolfram : MetroFramework.Forms.MetroForm
    {
        public ChartWolfram()
        {
            InitializeComponent();
            Theme = MetroThemeStyle.Dark;
            Style = MetroColorStyle.Yellow;
            pictureBox1.Width = Width - 30;
            pictureBox1.Height = Height - 5;
            pictureBox1.Location = new Point(0, 5);
        }
    }
}
