using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ChartForTestingProgram : Form
    {
        public ChartForTestingProgram(MainForm mf)
        {
            InitializeComponent();
            SetAllTools(mf);
        }

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
                var newForm = new ChartForTestingProgram(mainForm);
                return;
            }

            var xValue = (int)pastTimeTimer.ElapsedMilliseconds / 1000;

            chart.Series["Amount of points"].Points.AddXY(xValue, totalCountOfPoints);
        }

        private void StopTestingButton_Click(object sender, EventArgs e)
        {
            timerForPlotting.Stop();
            mainTimer.Stop();
        }

        private void ChartForTestingProgram_Load(object sender, EventArgs e)
        {
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            chart.Width = Consts.ChartTestingFormWidth;
            chart.Height = Consts.ChartTestingFormHeight;

            field.Location = new Point(0, 0);
            field.Width = Consts.GraphPictureBoxWidth;
            field.Height = Consts.GraphPictureBoxHeight;

            chart.Location = new Point(field.Width, 0);

            timeLabel.Location = new Point(Consts.TimeLabelLocationX, Consts.TimeLabelLocationY);

            label1.Location = new Point(Consts.LeftValueTrackBarLocationX, Consts.LeftValueTrackBarLocationY);
            label2.Location = new Point(Consts.RightValueTrackBarLocationX, Consts.RightValueTrackBarLocationY);

            coefficientTrackBar.Location = new Point(Consts.CoefficientTrackBarLocationX,
                                                     Consts.CoefficientTrackBarLocationY);
            stopTestingButton.Location = new Point(Consts.StopTestingProgramButtonLocationX,
                                                   Consts.StopTestingProgramButtonLocationY);
        }
    }
}
