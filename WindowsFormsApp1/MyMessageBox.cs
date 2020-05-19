using MetroFramework;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MyMessageBox : MetroFramework.Forms.MetroForm
    {
        public MyMessageBox()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            ControlBox = false;
        }

        public DialogResult NotifyDeleteAllGraph(string text, string caption)
        {
            var ms = new MyMessageBox
            {
                Style = MetroColorStyle.Orange,
                buttonOk = { Visible = false },
                buttonYes = { Visible = true, ForeColor = Color.Orange },
                buttonNo = { Visible = true, ForeColor = Color.Orange },
                Width = 400,
                Height = 210,
                label1 = { Location = new Point(17, 70) }
            };


            ms.buttonYes.Location = new Point(70, 130);
            ms.buttonNo.Location = new Point(240, 130);
            ms.label1.Text = text;
            ms.Text = caption;
            return ms.ShowDialog();
        }

        public DialogResult NotifyNumberMustBeInRange(string text)
        {
            var ms = new MyMessageBox
            {
                Style = MetroColorStyle.Orange,
                Text = "Warning",
                buttonOk = { Visible = true, ForeColor = Color.Orange },
                buttonYes = { Visible = false },
                buttonNo = { Visible = false },
                Width = 300
            };

            ms.buttonOk.Location = new Point(105, 145);
            ms.Height = 200;
            ms.label1.Location = new Point(30, 70);
            ms.label1.Text = text;

            return ms.ShowDialog();
        }

        public DialogResult NotifyDeleteElement(string message, string caption)
        {
            var ms = new MyMessageBox
            {
                Style = MetroColorStyle.Orange,
                buttonOk = { Visible = false },
                buttonYes = { Visible = true, ForeColor = Color.Orange },
                buttonNo = { Visible = true, ForeColor = Color.Orange },
                Width = 410,
                Height = 230,
                label1 = { Location = new Point(40, 70) }
            };


            ms.buttonYes.Location = new Point(80, 150);
            ms.buttonNo.Location = new Point(250, 150);
            ms.label1.Text = message;
            ms.Text = caption;
            return ms.ShowDialog();
        }

        public DialogResult NotifyWrongNumberOfForm(int min, int max)
        {
            var ms = new MyMessageBox
            {
                Width = max > 9 ? 335 : 320,
                Height = 170,
                Style = MetroColorStyle.Orange,
                buttonOk = { Visible = true, Location = new Point(115, 120), ForeColor = Color.Orange },
                buttonNo = { Visible = false },
                buttonYes = { Visible = false },
                Text = "Warning",
                label1 = {Text = $"Number must be in range [{min}; {max}]",
                    Location = new Point(20, 70) }
            };

            return ms.ShowDialog();
        }

        public DialogResult NotifyInvalidPlaceForVertex()
        {
            var ms = new MyMessageBox
            {
                Style = MetroColorStyle.Orange,
                Text = "Warning",
                buttonOk = { Visible = true, ForeColor = Color.Orange },
                buttonYes = { Visible = false },
                buttonNo = { Visible = false },
                Width = 260
            };

            ms.buttonOk.Location = new Point(90, 120);
            ms.Height = 170;
            ms.label1.Location = new Point(30, 70);
            ms.label1.Text = "Invalid place for vertex";

            return ms.ShowDialog();
        }

        public DialogResult ShowError(string message)
        {
            var ms = new MyMessageBox
            {
                Style = MetroColorStyle.Red,
                Text = "Error",
                buttonOk = { Visible = true, ForeColor = Color.Red },
                buttonYes = { Visible = false },
                buttonNo = { Visible = false },
                Width = 250
            };

            ms.buttonOk.Location = new Point(80, 120);
            ms.Height = 170;
            ms.label1.Location = new Point(30, 70);
            ms.label1.Text = message;

            return ms.ShowDialog();
        }

        public DialogResult NotifyEdgeExists(string message, int ver1, int ver2)
        {
            var ms = new MyMessageBox
            {
                Style = MetroColorStyle.Orange,
                Text = "Warning",
                buttonOk = { Visible = true, ForeColor = Color.Orange },
                buttonYes = { Visible = false },
                buttonNo = { Visible = false }
            };

            if (ver1 < 10 && ver2 < 10)
            {
                ms.Width = 320;
            }
            else if (ver1 >= 10 && ver2 >= 10)
            {
                ms.Width = 345;
            }
            else
            {
                ms.Width = 330;
            }

            ms.buttonOk.Location = new Point(125, 140);
            ms.Height = 200;
            ms.label1.Location = new Point(30, 70);
            ms.label1.Text = message;

            return ms.ShowDialog();
        }

        public DialogResult NotifyVertexMustNotCross(string text)
        {
            var ms = new MyMessageBox
            {
                Style = MetroColorStyle.Orange,
                Text = "Warning",
                buttonOk = { Visible = true, ForeColor = Color.Orange },
                buttonYes = { Visible = false },
                buttonNo = { Visible = false },
                Width = 250
            };


            ms.buttonOk.Location = new Point(85, 120);
            ms.Height = 180;
            ms.label1.Location = new Point(30, 70);
            ms.label1.Text = text;

            return ms.ShowDialog();
        }

        public DialogResult ShowHasNoEdges(string text)
        {
            var ms = new MyMessageBox
            {
                Style = MetroColorStyle.Orange,
                Text = "Warning",
                buttonOk = { Visible = true, ForeColor = Color.Orange },
                buttonYes = { Visible = false },
                buttonNo = { Visible = false },
                Width = 200
            };


            ms.buttonOk.Location = new Point(60, 120);
            ms.Height = 180;
            ms.label1.Location = new Point(55, 70);
            ms.label1.Text = text;

            return ms.ShowDialog();
        }

        public DialogResult NotifyNothingSelected(string message)
        {
            var ms = new MyMessageBox
            {
                Style = MetroColorStyle.Orange,
                Text = "Warning",
                buttonOk = { Visible = true, ForeColor = Color.Orange },
                buttonNo = { Visible = false },
                buttonYes = { Visible = false },
                Width = 210
            };

            ms.buttonOk.Location = new Point(60, 120);
            ms.Height = 170;
            ms.label1.Location = new Point(30, 70);
            ms.label1.Text = message;
            return ms.ShowDialog();
        }

        public DialogResult NotifyGraphIsEmpty(string message)
        {
            var ms = new MyMessageBox
            {
                Style = MetroColorStyle.Orange,
                Text = "Warning",
                buttonOk = { Visible = true, ForeColor = Color.Orange },
                buttonNo = { Visible = false },
                buttonYes = { Visible = false },
                Width = 200
            };

            ms.buttonOk.Location = new Point(60, 120);
            ms.Height = 170;
            ms.label1.Location = new Point(30, 70);
            ms.label1.Text = message;
            return ms.ShowDialog();
        }

        public DialogResult NotifyGraphIsStronglyDirection(string message)
        {
            var ms = new MyMessageBox
            {
                Style = MetroColorStyle.Green,
                Text = "Information",
                buttonOk = { Visible = true, ForeColor = Color.LimeGreen },
                buttonNo = { Visible = false },
                buttonYes = { Visible = false },
                Width = 290
            };

            ms.buttonOk.Location = new Point(100, 120);
            ms.Height = 170;
            ms.label1.Location = new Point(30, 70);
            ms.label1.Text = message;
            return ms.ShowDialog();
        }

        private void ButtonYesClick(object sender, EventArgs e)
        {
            Close();
        }

        private void MyMessageBox_Load(object sender, EventArgs e)
        {
            metroStyleManager1.Theme = MetroThemeStyle.Dark;
            StyleManager = metroStyleManager1;
        }

        private void ButtonNoClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
