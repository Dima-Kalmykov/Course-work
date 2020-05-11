using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;

namespace WindowsFormsApp1
{
    public partial class ChartForTestingProgram : MetroFramework.Forms.MetroForm
    {
        public ChartForTestingProgram(MainForm mf, int formNumber, List<ChartForTestingProgram> forms)
        {
            InitializeComponent();
            label5.Location = new Point(2000, 2000);
            textBox1.Text = string.Empty;
            FormCounter = formNumber;
            textBox1.KeyPress += textBox1_KeyPress;
            this.forms = forms;
            this.forms.Add(this);
            ListOfFormClass.SetSwitcherTools(this.forms, formNumber);
            foreach (var form in ListOfFormClass.chartForTestingProgramList)
            {
                form.label3.Text = ListOfFormClass.Pointer.ToString();
            }

            label4.Text = $"Number form: {formNumber}";
            var res = $"Array: ";
            res = ListOfFormClass.chartForTestingProgramList
                .Aggregate(res, (current, form) => current + form.label4 + "    ");

            label5.Text = res;

            SetAllTools(mf);
        }

        public int FormCounter;
        private List<ChartForTestingProgram> forms;
        private ToolsForDrawingGraph toolsForDrawing;
        private readonly List<Stopwatch> timers = new List<Stopwatch>();
        private readonly List<Edge> points = new List<Edge>();
        private readonly Stopwatch pastTimeTimer = new Stopwatch();
        private bool firstVertex;
        private Timer mainTimer = new Timer();
        private MainForm mainForm;

        private List<Vertex> vertex = new List<Vertex>();
        private List<Edge> edges = new List<Edge>();

        private float coefficient;
        private int totalCountOfPoints;

        private void SetUpChart()
        {
            chart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.Title = "Time";
        }

        private void SetUpMainTools(MainForm mf)
        {
            mainForm = mf;
            vertex = mf.Vertex.GetRange(0, mf.Vertex.Count);
            edges = mf.Edges.GetRange(0, mf.Edges.Count);
            coefficient = mf.coefficient;
            toolsForDrawing = mf.ToolsForDrawing;
            requireTime = mf.requireTime;
        }

        public void SetAllTools(MainForm mf)
        {
            SetUpMainTools(mf);

            SetUpChart();

            foreach (var item in vertex)
            {
                item.HasPoint = false;
            }

            vertex[0].HasPoint = true;
            firstVertex = true;

            edges = toolsForDrawing.GetOtherGraphWithGivenAmountOfEdgesAndVertex(vertex, edges);

            SetUpTimers();
        }

        private void SetUpTimers()
        {
            var listArr = new List<Edge>[vertex.Count];

            for (var i = 0; i < vertex.Count; i++)
            {
                List<Edge> curEdges = edges.Where(el => el.Ver1 == i).ToList();
                listArr[i] = new List<Edge>();
                listArr[i].AddRange(curEdges);
            }

            RunTimers(listArr);
        }

        private void RunTimers(List<Edge>[] listArr)
        {
            mainTimer = new Timer { Interval = 2 };
            mainTimer.Tick += (x, y) => MainTick(listArr);
            mainTimer.Start();



            timerForPlotting.Start();
            pastTimeTimer.Start();
            Show();
        }

        public void MainTick(List<Edge>[] listArr)
        {
            timeLabel.Text = $@"Time in millisecond: {pastTimeTimer.ElapsedMilliseconds}";
            // Если готова выпустить точку.
            var count = points.Count;
            coefficient = (float)(coefficientTrackBar.Value / 10.0);

            for (var i = 0; i < vertex.Count; i++)
            {
                if (vertex[i].HasPoint)
                {
                    vertex[i].HasPoint = false;

                    if (firstVertex)
                    {
                        firstVertex = false;
                        totalCountOfPoints += listArr[i].Count;
                    }
                    else
                    {
                        totalCountOfPoints += listArr[i].Count - 1;
                    }

                    points.AddRange(listArr[i]);

                    timers.AddRange(listArr[i].ConvertAll(el => new Stopwatch()));
                }
            }

            for (var i = count; i < timers.Count; i++)
            {
                timers[i].Start();
            }

            toolsForDrawing.DrawFullGraph(edges, vertex);

            for (var i = 0; i < points.Count; i++)
            {
                if (timers[i].ElapsedMilliseconds * coefficient > points[i].Weight * 1000)
                {
                    vertex[points[i].Ver2].HasPoint = true;
                    points.RemoveAt(i);
                    timers.RemoveAt(i);
                    i--;
                    continue;
                }

                PointF point = GetPoint(vertex[points[i].Ver1],
                    vertex[points[i].Ver2], points[i].Weight, timers[i]);

                toolsForDrawing.DrawPoint(point.X, point.Y);

                field.Image = toolsForDrawing.GetBitmap();
            }

            field.Image = toolsForDrawing.GetBitmap();
        }

        private PointF GetPoint(Vertex ver1, Vertex ver2, double allTime, Stopwatch timer)
        {
            var distanceX = Math.Abs(ver2.X - ver1.X);
            var distanceY = Math.Abs(ver2.Y - ver1.Y);

            float x = 0;
            float y = 0;

            // Loop. x^2 + y^2 = 400
            if (ver1 == ver2)
            {
                // Левая нижняя дуга.
                if (timer.ElapsedMilliseconds * coefficient / 1000.0 <= allTime / 4)
                {
                    x = (float)(100 / allTime * timer.ElapsedMilliseconds * coefficient / 1000.0);
                    y = (float)Math.Sqrt(400 - x * x);

                    if (x * x >= 400)
                    //return new PointF(ver1.X - 20, ver1.Y);
                    {
                        x = (float)(100 / allTime * (timer.ElapsedMilliseconds * coefficient / 1000.0 - allTime / 4));
                        y = -(float)Math.Sqrt(400 - x * x);

                        x += 20;
                        y -= 20;
                        return new PointF(y + ver1.X, ver1.Y - x);
                    }

                    x += 20;
                    y -= 20;
                    return new PointF(-x + ver1.X, ver1.Y + y);
                }

                if (timer.ElapsedMilliseconds * coefficient / 1000.0 <= allTime / 2)
                {
                    //Верхняя правая дуга.
                    x = (float)(100 / allTime * (timer.ElapsedMilliseconds * coefficient / 1000.0 - allTime / 4));
                    y = -(float)Math.Sqrt(400 - x * x);

                    if (x * x >= 400)
                    {
                        x = -(float)(100 / allTime * (timer.ElapsedMilliseconds * coefficient / 1000.0 - allTime / 2));
                        y = -(float)Math.Sqrt(400 - x * x);


                        x += 20;
                        y -= 20;
                        return new PointF(-x + ver1.X, ver1.Y + y);
                    }

                    x += 20;
                    y -= 20;
                    return new PointF(y + ver1.X, ver1.Y - x);
                }


                if (timer.ElapsedMilliseconds * coefficient / 1000.0 <= allTime * 3 / 4)
                {
                    // Правая верхняя дуга
                    x = -(float)(100 / allTime * (timer.ElapsedMilliseconds * coefficient / 1000.0 - allTime / 2));
                    y = -(float)Math.Sqrt(400 - x * x);

                    if (x * x >= 400)
                    {
                        x = -(float)(100 / allTime * (timer.ElapsedMilliseconds * coefficient / 1000.0 - allTime * 3 / 4));
                        y = -(float)Math.Sqrt(400 - x * x);

                        x += 20;
                        y += 20;
                        return new PointF(-y + ver1.X, ver1.Y - x);
                    }

                    x += 20;
                    y -= 20;
                    return new PointF(-x + ver1.X, ver1.Y + y);
                }



                if (timer.ElapsedMilliseconds * coefficient / 1000.0 <= allTime)
                {
                    x = -(float)(100 / allTime * (timer.ElapsedMilliseconds * coefficient / 1000.0 - allTime * 3 / 4));
                    y = -(float)Math.Sqrt(400 - x * x);

                    if (x * x >= 400)
                    {
                        x = (float)(100 / allTime * timer.ElapsedMilliseconds * coefficient / 1000.0);
                        y = (float)Math.Sqrt(400 - x * x);

                        return new PointF(ver1.X - 20, ver1.Y);
                    }

                    x += 20;
                    y += 20;
                    return new PointF(-y + ver1.X, ver1.Y - x);
                }
            }
            else
            {
                // Normal edge.
                if (ver1.X < ver2.X)
                {
                    if (ver1.Y > ver2.Y)
                    {
                        x = (float)(distanceX / allTime * timer.ElapsedMilliseconds * coefficient / 1000) + ver1.X;
                        y = -(float)(distanceY / allTime * timer.ElapsedMilliseconds * coefficient / 1000) + ver1.Y;
                    }
                    else
                    {
                        x = (float)(distanceX / allTime * timer.ElapsedMilliseconds * coefficient / 1000) + ver1.X;
                        y = (float)(distanceY / allTime * timer.ElapsedMilliseconds * coefficient / 1000) + ver1.Y;
                    }
                }
                else
                {
                    if (ver1.Y > ver2.Y)
                    {
                        x = -(float)(distanceX / allTime * timer.ElapsedMilliseconds * coefficient / 1000) + ver1.X;
                        y = -(float)(distanceY / allTime * timer.ElapsedMilliseconds * coefficient / 1000) + ver1.Y;
                    }
                    else
                    {
                        x = -(float)(distanceX / allTime * timer.ElapsedMilliseconds * coefficient / 1000) + ver1.X;
                        y = (float)(distanceY / allTime * timer.ElapsedMilliseconds * coefficient / 1000) + ver1.Y;
                    }
                }
            }

            return new PointF(x, y);
        }

        private long requireTime;

        private void TimerForPlotting_Tick(object sender, EventArgs e)
        {
            if (pastTimeTimer.ElapsedMilliseconds > requireTime)
            {
                timerForPlotting.Stop();
                mainTimer.Stop();
                Hide();
                var _ = new ChartForTestingProgram(mainForm, ++FormCounter, forms);
                return;
            }

            var xValue = (int)pastTimeTimer.ElapsedMilliseconds / 1000;

            chart.Series["Amount of points"].Points.AddXY(xValue, totalCountOfPoints);
        }

        private void StopTestingButton_Click(object sender, EventArgs e)
        {
            foreach (var form in forms)
            {
                form.timerForPlotting.Stop();
                form.mainTimer.Stop();
            }
            timerForPlotting.Stop();
            mainTimer.Stop();
        }

        private void ChartForTestingProgram_Load(object sender, EventArgs e)
        {
            ControlBox = true;
            chart.Legends[0].BackColor = Color.SlateGray;
            chart.Series["Amount of points"].Color = Color.Yellow;
            chart.ChartAreas["ChartArea1"].BackColor = Color.SlateGray;

            chart.BackColor = Color.SlateGray;
            Theme = MetroThemeStyle.Dark;
            Style = MetroColorStyle.Yellow;
            TopMost = true;
            Width = 1500;
            Height = 800;
            chart.Width = Consts.ChartTestingFormWidth - 220;
            chart.Height = Consts.ChartTestingFormHeight;

            field.Location = new Point(0, 5);
            field.Width = Consts.GraphPictureBoxWidth;
            field.Height = Consts.GraphPictureBoxHeight;

            chart.Location = new Point(field.Width, 5);

            timeLabel.Location = new Point(Consts.TimeLabelLocationX, Consts.TimeLabelLocationY);

            label1.Location = new Point(Consts.LeftValueTrackBarLocationX, Consts.LeftValueTrackBarLocationY);
            label2.Location = new Point(Consts.RightValueTrackBarLocationX, Consts.RightValueTrackBarLocationY);

            coefficientTrackBar.Location = new Point(Consts.CoefficientTrackBarLocationX,
                                                     Consts.CoefficientTrackBarLocationY);
            stopTestingButton.Location = new Point(Consts.StopTestingProgramButtonLocationX,
                                                   Consts.StopTestingProgramButtonLocationY);
        }

        // ToDo когда останавливают испытание можно делать листать.
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (ListOfFormClass.Pointer > 0)
            {
                ListOfFormClass.Pointer--;
            }
            else
            {
                return;
            }

            foreach (var chartForTestingProgram in ListOfFormClass.chartForTestingProgramList)
            {
                chartForTestingProgram.textBox1.Text = (ListOfFormClass.Pointer + 1).ToString();
            }

            foreach (var form in ListOfFormClass.chartForTestingProgramList)
            {
                form.label3.Text = ListOfFormClass.Pointer.ToString();
            }

            HideAllForms();
            ListOfFormClass.chartForTestingProgramList[ListOfFormClass.Pointer].Show();
            ListOfFormClass.chartForTestingProgramList[ListOfFormClass.Pointer].Activate();
        }


        private void HideAllForms()
        {
            foreach (var form in ListOfFormClass.chartForTestingProgramList)
            {
                form.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ListOfFormClass.Pointer < ListOfFormClass.chartForTestingProgramList.Count - 1)
            {
                ListOfFormClass.Pointer++;
            }
            else
            {
                return;
            }

            foreach (var chartForTestingProgram in ListOfFormClass.chartForTestingProgramList)
            {
                chartForTestingProgram.textBox1.Text = (ListOfFormClass.Pointer + 1).ToString();
            }

            foreach (var form in ListOfFormClass.chartForTestingProgramList)
            {
                form.label3.Text = ListOfFormClass.Pointer.ToString();
            }

            HideAllForms();

            ListOfFormClass.chartForTestingProgramList[ListOfFormClass.Pointer].Show();
            ListOfFormClass.chartForTestingProgramList[ListOfFormClass.Pointer].Activate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            ListOfFormClass.Pointer = 0;

            foreach (var chartForTestingProgram in ListOfFormClass.chartForTestingProgramList)
            {
                chartForTestingProgram.textBox1.Text = "1";
            }

            foreach (var form in ListOfFormClass.chartForTestingProgramList)
            {
                form.label3.Text = ListOfFormClass.Pointer.ToString();
            }

            ListOfFormClass.chartForTestingProgramList[0].Show();
            ListOfFormClass.chartForTestingProgramList[0].Activate();
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            var lastForm = ListOfFormClass.chartForTestingProgramList.LastOrDefault();

            if (lastForm is null)
            {
                return;
            }


            foreach (var form in ListOfFormClass.chartForTestingProgramList)
            {
                form.label3.Text = ListOfFormClass.Pointer.ToString();
            }


            

            ListOfFormClass.Pointer = ListOfFormClass.chartForTestingProgramList.Count - 1;
            foreach (var chartForTestingProgram in ListOfFormClass.chartForTestingProgramList)
            {
                chartForTestingProgram.textBox1.Text = (ListOfFormClass.Pointer + 1).ToString();
            }

            foreach (var form in ListOfFormClass.chartForTestingProgramList)
            {
                form.label3.Text = ListOfFormClass.Pointer.ToString();
            }

            HideAllForms();

            lastForm.Show();
            lastForm.Activate();
        }

        private void ChartForTestingProgram_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var curForm = ListOfFormClass.chartForTestingProgramList[ListOfFormClass.Pointer];

            if (curForm.textBox1.Focused && e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    var point = int.Parse(textBox1.Text);
                    point--;

                    if (point > ListOfFormClass.chartForTestingProgramList.Count ||
                        point < 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    ListOfFormClass.Pointer = point;
                    foreach (var form in ListOfFormClass.chartForTestingProgramList)
                    {
                        form.textBox1.Text = (point + 1).ToString();
                    }
                    HideAllForms();
                    ListOfFormClass.chartForTestingProgramList[point].Show();
                    ListOfFormClass.chartForTestingProgramList[point].Activate();
                }
                catch (Exception)
                {
                    label5.Focus();
                    HideAllForms();
                    var ms = new MyMessageBox();
                    var res = ms.ShowPlot(1, ListOfFormClass.chartForTestingProgramList.Count);

                    if (res == DialogResult.OK)
                    {
                        curForm.Show();

                        foreach (var form in ListOfFormClass.chartForTestingProgramList)
                        {
                            form.textBox1.Text = string.Empty;
                        }
                    }
                }
            }
        }
    }
}
