using MetroFramework;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class DisplayMainPartByTimeForm : MetroFramework.Forms.MetroForm
    {
        public DisplayMainPartByTimeForm()
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
