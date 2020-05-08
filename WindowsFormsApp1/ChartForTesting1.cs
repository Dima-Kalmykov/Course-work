using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ChartForTesting1 : Form
    {
        public ChartForTesting1(MainForm mf)
        {
            InitializeComponent();
            Set(mf);
        }

        ToolsForDrawingGraph toolsForDrawing;
        private readonly List<Stopwatch> timers = new List<Stopwatch>();
        private readonly List<Edge> points = new List<Edge>();
        private Timer timer = new Timer();
        private Stopwatch sp = new Stopwatch();
        private bool firstVertex;
        private Timer mainTimer = new Timer();
        private MainForm mf;

        private List<Vertex> vertex = new List<Vertex>();
        private List<Edge> edges = new List<Edge>();
        private float coefficient;

        public void Set(MainForm _mf)
        {
            mf = _mf;
            vertex = _mf.Vertex.GetRange(0, _mf.Vertex.Count);
            edges = _mf.Edges.GetRange(0, _mf.Edges.Count);
            coefficient = _mf.coefficient;
            toolsForDrawing = _mf.ToolsForDrawing;
            requireTime = _mf.requireTime;

            foreach (var vertex1 in vertex)
            {
                vertex1.HasPoint = false;
            }

            chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Title = "Time";

            vertex[0].HasPoint = true;
            firstVertex = true;
            var listArr = new List<Edge>[vertex.Count];

            edges = toolsForDrawing.GetOtherGraphWithGivenAmountOfEdgesAndVertex(vertex, edges);
            for (var i = 0; i < vertex.Count; i++)
            {
                List<Edge> curEdges = edges.Where(el => el.Ver1 == i).ToList();
                listArr[i] = new List<Edge>();
                listArr[i].AddRange(curEdges);
            }

            mainTimer = new Timer { Interval = 2 };
            mainTimer.Tick += (x, y) => MainTick(listArr);
            mainTimer.Start();
            timer1.Start();
            sp.Start();
            Show();
        }

        private int totalCount;

        public void MainTick(List<Edge>[] listArr)
        {
            label1.Text = $@"Time in millisecond: {sp.ElapsedMilliseconds}";
            // Если готова выпустить точку.
            var count = points.Count;

            for (var i = 0; i < vertex.Count; i++)
            {
                if (vertex[i].HasPoint)
                {
                    vertex[i].HasPoint = false;

                    if (firstVertex)
                    {
                        firstVertex = false;
                        totalCount += listArr[i].Count;
                    }
                    else
                    {
                        totalCount += listArr[i].Count - 1;
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

                pictureBox1.Image = toolsForDrawing.GetBitmap();
            }

            pictureBox1.Image = toolsForDrawing.GetBitmap();
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
        private void ChartForTesting1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sp.ElapsedMilliseconds > requireTime)
            {
                timer.Stop();
                timer1.Stop();
                mainTimer.Stop();
                ChartForTesting1 cft2 = new ChartForTesting1(mf);
                return;
            }
            chart1.Series["Amount of points"]
                .Points.AddXY((int)(sp.ElapsedMilliseconds) / 1000, totalCount);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Stop();
            timer1.Stop();
            mainTimer.Stop();
        }
    }
}
