using MetroFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WindowsFormsApp1
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        public double Alpha;
        public double CyclomaticNumber;
        public DisplayMainPartByTimeForm DisplayMainPartByTimeForm;

        public long RequireTimeForTesting;

        // Инструменты для рисования.
        public ToolsForDrawing ToolsForDrawing;

        // Списки смежности.
        public List<Vertex> Vertices;

        public List<Edge> Edges;

        public float SpeedCoefficient = 1;

        // Диалоговые формы.
        private readonly ChooseEdgeForm chooseEdgeForm = new ChooseEdgeForm();

        private readonly GetWeightEdgeForm getEdgeLengthForm = new GetWeightEdgeForm();
        private readonly GetRandomGraphForm getRandomGraphForm = new GetRandomGraphForm();

        private readonly CheckGraphForStronglyDirectionForm checkGraphForStronglyDirectionForm =
            new CheckGraphForStronglyDirectionForm();

        private readonly ShowAdjacencyMatrixForm showAdjacencyMatrixForm = new ShowAdjacencyMatrixForm();
        private readonly Chart chartForm = new Chart();
        private readonly ChartForOpenFromFileForm chartForOpenFromFileForm = new ChartForOpenFromFileForm();

        private readonly MyMessageBox myMessageBox = new MyMessageBox();

        // Номера вершин, между которым проводим ребро.
        private int ver1ForConnection = -1;

        private int ver2ForConnection = -1;

        // Матрица смежности.
        private double[,] adjMatrix;

        // Пути для сохранения и считывания файла.
        private string pathForSaveFile;

        private string pathForOpenFile;

        // Если нажали первый раз, чтобы перетащить вершину.
        private bool firstPress = true;

        // Индекс вершины, которая переносится.
        private int indexVertexForMove = -1;

        private bool responseSC;

        private int totalPointsCount;
        private bool isFirstVertex = true;

        private readonly List<MyPoint> pointsForComputeFormula = new List<MyPoint>();

        private readonly List<Stopwatch> timers = new List<Stopwatch>();
        private readonly List<Edge> points = new List<Edge>();

        private bool wasClickedContinue;
        private Timer mainTimer = new Timer();
        private readonly Stopwatch timerForPlotting = new Stopwatch();

        private static readonly StringBuilder ChartFormatData = new StringBuilder("Time;Amount\n");
        private int pastSecondsCount;

        public MainForm()
        {
            // Устанавливаем необходимые параметры.
            InitializeComponent();
            Theme = MetroThemeStyle.Light;
            Style = MetroColorStyle.Green;
            ToolsForDrawing = new ToolsForDrawing(Consts.GraphPictureBoxWidth, Consts.GraphPictureBoxHeight);
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();

            chooseEdgeForm.Owner = this;
            getEdgeLengthForm.Owner = this;
            getEdgeLengthForm.Text = "Get edge weight";
            getRandomGraphForm.Owner = this;
            checkGraphForStronglyDirectionForm.Owner = this;

            ToolsForDrawing.ClearField();
            field.Image = ToolsForDrawing.GetBitmap();

            HideSubMenu();
        }

        /// <summary>
        /// Спрятать матрицу смежности.
        /// </summary>
        private void HideAdjacencyMatrix()
        {
            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (Vertices.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Close matrix";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
        }

        /// <summary>
        /// Перерисовать все вершины.
        /// </summary>
        private void RedrawSelectedVertex()
        {
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                ToolsForDrawing.DrawVertex(Vertices[indexVertexForMove].X,
                    Vertices[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());

                firstPress = true;
                field.Image = ToolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                ToolsForDrawing.DrawVertex(Vertices[ver1ForConnection].X,
                    Vertices[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = ToolsForDrawing.GetBitmap();
            }
        }

        /// <summary>
        /// Нажатие на кнопку "Рисование вершины".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawVertexButtonClick(object sender, EventArgs e)
        {
            // Делаем эту кнопку неактивной.
            DrawVertexButton.Enabled = false;

            RedrawSelectedVertex();

            // Делаем остальные кнопки активными.
            DeleteElementButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            HideAdjacencyMatrix();
        }

        /// <summary>
        /// Удалить граф.
        /// </summary>
        public void DeleteGraph()
        {
            Vertices.Clear();
            Edges.Clear();
            showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Clear();
            ToolsForDrawing.ClearField();
            field.Image = ToolsForDrawing.GetBitmap();
        }

        /// <summary>
        /// Подтвердить удаление.
        /// </summary>
        /// <returns> Результат диалога </returns>
        public DialogResult GetConfirmCancellation()
        {
            DrawVertexButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            var message = "Are you sure you want to delete the graph?";
            var caption = "Confirmation";

            // Диалоговое окно для подтверждения удаления.

            var confirmCancellation = myMessageBox.NotifyDeleteAllGraph(message, caption);

            return confirmCancellation;
        }

        /// <summary>
        /// Нажатие на кнопку "Удаление всего графа".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteFullGraphButtonClick(object sender, EventArgs e)
        {
            RedrawSelectedVertex();

            // Проверяем, есть ли какой-то элемент для удаления.
            if (Vertices.Count != 0)
            {
                var confirmCancellation = GetConfirmCancellation();

                // Если подтверилось удаление, удаляем.
                if (confirmCancellation == DialogResult.Yes)
                {
                    DeleteGraph();
                }
            }
            else
            {
                myMessageBox.NotifyGraphIsEmpty("Graph is empty");
            }

            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            HideAdjacencyMatrix();
        }

        /// <summary>
        /// Нажатие на кнопку "Рисование веришны".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawEdgeButtonClick(object sender, EventArgs e)
        {
            // Делаем эту кнопку неактивной.
            DrawEdgeButton.Enabled = false;

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            RedrawSelectedVertex();

            // Номера вершин, между которыми будем проводить ребро.
            ver1ForConnection = -1;
            ver2ForConnection = -1;

            HideAdjacencyMatrix();
        }

        /// <summary>
        /// Нажатие на кнопку "Проверить граф на сильносвязность".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckGraphForStrongDirectionButtonClick(object sender, EventArgs e)
        {
            if (!wasClickedContinue)
            {
                RedrawSelectedVertex();

                // Делаем остальные кнопки активными.
                DrawVertexButton.Enabled = true;
                DeleteElementButton.Enabled = true;
                DrawEdgeButton.Enabled = true;
                ChangeEdgeLengthButton.Enabled = true;

                // Если нечего проверять, выводим сообщение.
                if (adjMatrix is null || adjMatrix.Length == 0)
                {
                    myMessageBox.NotifyGraphIsEmpty("Graph is empty");
                }
                else
                {
                    // Представляем граф в удобном для проверки виде.
                    SpecialKindOfGraphForCheckStronglyDirection graphForCheck = new SpecialKindOfGraphForCheckStronglyDirection(Vertices.Count);

                    foreach (var edge in Edges)
                        graphForCheck.AddEdge(edge.Ver1, edge.Ver2);

                    // Проверяем граф на сильносвязность.
                    // В случае, если граф не сильносвязный, то
                    // предлагаем сгенерировать случайный сильносвязный граф,
                    // или продолжить редактирование
                    if (graphForCheck.IsStronglyConnection())
                    {
                        myMessageBox.NotifyGraphIsStronglyDirection("Graph is strongly directed");
                    }
                    else
                    {
                        //checkGraphForStronglyConnectionForm.Caption.Text =
                        //    "Граф не является сильносвязным";

                        var res = checkGraphForStronglyDirectionForm.MyShow();

                        checkGraphForStronglyDirectionForm.MustBeGenerated = res.Item2;

                        if (checkGraphForStronglyDirectionForm.MustBeGenerated)
                            GetRandomGraphButton.PerformClick();
                    }
                }

                HideAdjacencyMatrix();
            }
            else
            {
                // Если нечего проверять, выводим сообщение.
                if (adjMatrix is null || adjMatrix.Length == 0)
                    myMessageBox.NotifyGraphIsEmpty("Graph is empty");
                else
                {
                    // Представляем граф в удобном для проверки виде.
                    var graphForCheck = new SpecialKindOfGraphForCheckStronglyDirection(Vertices.Count);

                    foreach (var edge in Edges)
                        graphForCheck.AddEdge(edge.Ver1, edge.Ver2);

                    // Проверяем граф на сильносвязность.
                    // В случае, если граф не сильносвязный, то
                    // предлагаем сгенерировать случайный сильносвязный граф,
                    // или продолжить редактирование
                    if (graphForCheck.IsStronglyConnection())
                        responseSC = true;
                    else
                    {
                        DrawVertexButton.Enabled = true;
                        DeleteElementButton.Enabled = true;
                        DrawEdgeButton.Enabled = true;
                        ChangeEdgeLengthButton.Enabled = true;
                        responseSC = false;
                        var res = checkGraphForStronglyDirectionForm.MyShow();

                        checkGraphForStronglyDirectionForm.MustBeGenerated = res.Item2;

                        if (checkGraphForStronglyDirectionForm.MustBeGenerated)
                            GetRandomGraphButton.PerformClick();
                    }
                }
            }
        }

        /// <summary>
        /// Нажатие на кнопку "Сгенерировать граф".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetRandomGraphButtonClick(object sender, EventArgs e)
        {
            RedrawSelectedVertex();

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;
            DeleteElementButton.Enabled = true;

            //getRandomGraphForm.ShowDialog();

            var res = getRandomGraphForm.MyShow();
            getRandomGraphForm.CanceledGeneration = res.Item2;
            getRandomGraphForm.VertexAmount = res.Item3;

            // Если нажали отмену, то ничего не происходит.
            if (!getRandomGraphForm.CanceledGeneration)
            {
                // Количество вершин.
                int size = getRandomGraphForm.VertexAmount;

                ToolsForDrawing.ClearField();

                // Генерируем матрицу смежности.
                //adjMatrix = graph.GetRandomAdjMatrix(size);
                adjMatrix = ToolsForDrawing.GetRandomAdjacencyMatrix(size);

                // Устанавливаем вес рёбер.
                adjMatrix = ToolsForDrawing.SetDistanceEdge(adjMatrix);

                // Генерируем координаты вершин.
                Vertices = ToolsForDrawing.GetRandomVertices(adjMatrix,
                    field.Size.Width - 2 * Consts.VertexRadius,
                    field.Size.Height - 2 * Consts.VertexRadius);

                // Заполняем лист рёбер.
                Edges = ToolsForDrawing.GetRandomEdges(adjMatrix);

                // Отрисовываем полный граф.
                ToolsForDrawing.DrawFullGraph(Edges, Vertices);

                // Выводим матрицу смежности.
                PrintAdjacencyMatrix();

                field.Image = ToolsForDrawing.GetBitmap();
            }

            HideAdjacencyMatrix();
        }

        /// <summary>
        /// Нажатие на кнопку "удалить элемент".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteElementButtonClick(object sender, EventArgs e)
        {
            RedrawSelectedVertex();

            // Делаем эту кнопку неактивной.
            DeleteElementButton.Enabled = false;

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (Vertices.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Close matrix";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
        }

        /// <summary>
        /// Нажатие на кнопку "Сохранить граф".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveGraphButtonClick(object sender, EventArgs e)
        {
            RedrawSelectedVertex();

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;
            DeleteElementButton.Enabled = true;

            // Проверяем, есть ли, что сохранять.
            if (field.Image != null && !(adjMatrix is null) && adjMatrix.Length != 0)
            {
                SaveFileDialog saveGraphDialog = new SaveFileDialog();
                saveGraphDialog.Title = "Save graph as...";
                saveGraphDialog.OverwritePrompt = true;
                saveGraphDialog.CheckPathExists = true;
                saveGraphDialog.Filter = "Imagine Files(*.BMP)|*.BMP|" +
                                         "Imagine Files(*.JPG)|*.JPG|" +
                                         "Imagine Files(*.PNG)|*.PNG";

                saveGraphDialog.ShowHelp = true;

                // Процесс сохранения файла.
                if (saveGraphDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        field.Image.Save(saveGraphDialog.FileName, ImageFormat.Jpeg);
                        pathForSaveFile = saveGraphDialog.FileName;

                        pathForSaveFile = pathForSaveFile.Substring(0,
                                              pathForSaveFile.LastIndexOf(".") + 1) +
                                          "bin";

                        SaveToolsForGraph();
                    }
                    catch (Exception)
                    {
                        myMessageBox.ShowError("Unable to save image");
                    }
                }
            }
            else
                myMessageBox.NotifyGraphIsEmpty("Graph is empty");

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (Vertices.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Close matrix";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
        }

        /// <summary>
        /// Нажатие на кнопку "Открыть граф".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenGraphFromFileButtonClick(object sender, EventArgs e)
        {
            RedrawSelectedVertex();

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;
            DeleteElementButton.Enabled = true;

            OpenFileDialog openGraphDialog = new OpenFileDialog();
            openGraphDialog.Title = "Open graph...";
            openGraphDialog.Filter = "Imagine Files(*.BMP)|*.BMP|" +
                                     "Imagine Files(*.JPG)|*.JPG|" +
                                     "Imagine Files(*.PNG)|*.PNG";

            openGraphDialog.ShowHelp = true;

            // Процесс считывания информации из файла файла.
            if (openGraphDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    field.Image = Image.FromFile(openGraphDialog.SafeFileName);

                    pathForOpenFile = openGraphDialog.FileName;

                    pathForOpenFile = pathForOpenFile.Substring(0,
                                          pathForOpenFile.LastIndexOf(".") + 1) + "bin";

                    SetDataFromFile();

                    PrintAdjacencyMatrix();

                    ToolsForDrawing.ClearField();
                    ToolsForDrawing.DrawFullGraph(Edges, Vertices);
                    field.Image = ToolsForDrawing.GetBitmap();
                }
                catch (Exception)
                {
                    myMessageBox.ShowError("Unable to open image");
                }
            }

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (Vertices.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Close matrix";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
        }

        /// <summary>
        /// Нажатие на кнопку "Изменить длину ребра".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeWeightButtonClick(object sender, EventArgs e)
        {
            RedrawSelectedVertex();

            // Если нет рёбер, пишем об этом.
            if (Edges.Count != 0)
            {
                // Делаем эту кнопку активной.
                ChangeEdgeLengthButton.Enabled = false;

                // Делаем остальные кнопки неактивными.
                DrawVertexButton.Enabled = true;
                DrawEdgeButton.Enabled = true;
                DeleteElementButton.Enabled = true;
            }
            else
                myMessageBox.ShowHasNoEdges("No edges");

            HideAdjacencyMatrix();
        }

        /// <summary>
        /// Нажатие на кнопку "Прекратить рисование".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopDrawingButtonClick(object sender, EventArgs e)
        {
            RedrawSelectedVertex();

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            HideAdjacencyMatrix();
        }

        /// <summary>
        /// Нажатие на кнопку "Открыть/закрыть матрицу смежности".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOrHideAdjacencyMatrixButtonClick(object sender, EventArgs e)
        {
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                ToolsForDrawing.DrawVertex(Vertices[indexVertexForMove].X,
                    Vertices[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = ToolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                ToolsForDrawing.DrawVertex(Vertices[ver1ForConnection].X,
                    Vertices[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = ToolsForDrawing.GetBitmap();
            }

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            // В зависимости от текста открываем/закрываем матрицу смежности.
            if (ShowOrHideAdjMatrix.Text == "Close matrix")
            {
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Text = "Open matrix";
            }
            else
            {
                showAdjacencyMatrixForm.Show();
                ShowOrHideAdjMatrix.Text = "Close matrix";
            }
        }

        /// <summary>
        /// Устанавливаем текст в кнопки перед открытием
        /// формы для выбора пути для удаления.
        /// </summary>
        /// <param name="ver1"> Номер первой вершины </param>
        /// <param name="ver2"> Номер второй веришны </param>
        private (string, string, string) GetTextInButtonForChooseEdgeForm(int ver1, int ver2)
        {
            //chooseEdgeFormForm.TextForUnderstandingLabel.Text = @"Select the edge you want to delete";
            //chooseEdgeFormForm.FirstOptionButton.Text = $@"Delete edge from {ver1 + 1} to {ver2 + 1}";
            //chooseEdgeFormForm.SecondOptionButton.Text = $@"Delete edge from {ver2 + 1} to {ver1 + 1}";

            return (@"Select the edge you want to delete",
                $@"Delete edge from {ver1 + 1} to {ver2 + 1}",
                $@"Delete edge from {ver2 + 1} to {ver1 + 1}");
        }

        /// <summary>
        /// Обработка любого нажатия на поле для рисования.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldMouseClick(object sender, MouseEventArgs e)
        {
            // Всё происходит только при нажатии ЛКМ.
            if (e.Button == MouseButtons.Left)
            {
                // Если ничего не нажато, то мы можем перетащить вершину.
                if (DrawEdgeButton.Enabled && DrawVertexButton.Enabled &&
                    DeleteElementButton.Enabled && ChangeEdgeLengthButton.Enabled)
                {
                    if (firstPress)
                    {
                        // Ищем вершину, на которую нажали и выделяем её.
                        for (int i = 0; i < Vertices.Count; i++)
                        {
                            if (Math.Pow(Vertices[i].X - e.X, 2) + Math.Pow(Vertices[i].Y - e.Y, 2) <=
                                Math.Pow(Consts.VertexRadius, 2))
                            {
                                ToolsForDrawing.DrawSelectedVertex(Vertices[i].X, Vertices[i].Y);
                                field.Image = ToolsForDrawing.GetBitmap();

                                indexVertexForMove = i;
                                firstPress = false;
                            }
                        }
                    }
                    else
                    {
                        {
                            firstPress = true;

                            // Проверяем, чтобы вершина при переносе не перекрывала другую.
                            if (Vertices.Where((v, i) => i != indexVertexForMove)
                                      .Any(t => Math.Abs(e.X - t.X) < 2 * Consts.VertexRadius &&
                                                Math.Abs(e.Y - t.Y) < 2 * Consts.VertexRadius))
                            {
                                RedrawSelectedVertex();
                                myMessageBox.NotifyVertexMustNotCross("Vertex must not cross");
                                firstPress = true;
                                return;
                            }

                            var newVertex = new Vertex(e.X, e.Y);

                            if (newVertex.X >= field.Size.Width - 2 * Consts.VertexRadius ||
                                newVertex.X <= 2 * Consts.VertexRadius ||
                                newVertex.Y <= 2 * Consts.VertexRadius ||
                                newVertex.Y >= field.Size.Height - 2 * Consts.VertexRadius)
                            {
                                RedrawSelectedVertex();
                                myMessageBox.NotifyInvalidPlaceForVertex();
                                firstPress = true;
                                return;
                            }

                            // Тогда двигаем точку и перерисовываем граф.
                            Vertices[indexVertexForMove].X = e.X;
                            Vertices[indexVertexForMove].Y = e.Y;

                            ToolsForDrawing.ClearField();

                            indexVertexForMove = -1;

                            ToolsForDrawing.DrawFullGraph(Edges, Vertices);

                            field.Image = ToolsForDrawing.GetBitmap();
                        }
                    }
                }

                // Если нажата кнопка "Изменить длину ребра".
                if (!ChangeEdgeLengthButton.Enabled)
                {
                    // Если мы нажали на вершину, то ничего не происходит.
                    if (Vertices.Any(t => Math.Pow(t.X - e.X, 2) + Math.Pow(t.Y - e.Y, 2) <=
                                        Math.Pow(Consts.VertexRadius, 2)))
                    {
                        return;
                    }

                    for (int i = 0; i < Edges.Count; i++)
                    {
                        // Если это петля
                        if (Edges[i].Ver1 == Edges[i].Ver2)
                        {
                            // Петля - это круг. и +- ширина кисти/2 к радиусу.
                            // У нас центр окружности петли смещён на -R -R.
                            // Если клик попал в тор, то предлагаем изменить длину.

                            if ((Math.Pow(Vertices[Edges[i].Ver1].X - Consts.VertexRadius - e.X, 2) +
                                 Math.Pow(Vertices[Edges[i].Ver1].Y - Consts.VertexRadius - e.Y, 2)) <=
                                Math.Pow(Consts.VertexRadius + 4, 2)

                                &&

                                (Math.Pow(Vertices[Edges[i].Ver1].X - Consts.VertexRadius - e.X, 2) +
                                 Math.Pow(Vertices[Edges[i].Ver1].Y - Consts.VertexRadius - e.Y, 2)) >=
                                Math.Pow(Consts.VertexRadius - 4, 2))
                            {
                                //getEdgeLengthForm.ShowDialog();
                                var result = getEdgeLengthForm.MyShow();

                                getEdgeLengthForm.WasCanceled = result.Item2;
                                getEdgeLengthForm.Weight = result.Item3;

                                // Если мы не нажади отмену, то меняем вес.
                                if (!getEdgeLengthForm.WasCanceled)
                                {
                                    Edges[i].Weight = getEdgeLengthForm.Weight;
                                    PrintAdjacencyMatrix();
                                }
                            }
                        }
                        else
                        {
                            // Если x координаты двух вершин ребра не совпадают.
                            if (Vertices[Edges[i].Ver1].X != Vertices[Edges[i].Ver2].X)
                            {
                                // Вычисляем коэффициенты в уравнении прямой,
                                // проходящей через две вершины этого ребра, вида y = kx + b.
                                double k = MyMath.GetK(
                                    Vertices[Edges[i].Ver1], Vertices[Edges[i].Ver2]);

                                double b = MyMath.GetB(
                                    Vertices[Edges[i].Ver1], Vertices[Edges[i].Ver2]);

                                // Проводим прямую между двумя веришнами ребра,
                                // и "расширяем" её на ширину кисти/2.
                                // Если попали в область, то меняем длину этого ребра.
                                if (e.Y <= k * e.X + b + 4 && e.Y >= k * e.X + b - 4)
                                {
                                    // Если есть обратный путь, то выбираем, какой меняем.
                                    if (adjMatrix[Edges[i].Ver1, Edges[i].Ver2] != 0 &&
                                        adjMatrix[Edges[i].Ver2, Edges[i].Ver1] != 0)
                                    {
                                        //chooseEdgeFormForm.TextForUnderstandingLabel.Text =
                                        //    "Длину какого пути Вы хотите изменить:";

                                        //chooseEdgeFormForm.FirstOptionButton.Text =
                                        //    $"Изменить длину пути из {Edges[i].Ver1 + 1} в " +
                                        //    $"{Edges[i].Ver2 + 1}";

                                        //chooseEdgeFormForm.SecondOptionButton.Text =
                                        //    $"Изменить длину пути из {Edges[i].Ver2 + 1} в " +
                                        //    $"{Edges[i].Ver1 + 1}";

                                        //chooseEdgeFormForm.ShowDialog();

                                        var res = chooseEdgeForm.MyShowChangeLength(
                                            "Choose the edge whose weight\n          you want to change",
                                            $"Change edge weight from {Edges[i].Ver1 + 1} to {Edges[i].Ver2 + 1}",
                                            $"Change edge weight from {Edges[i].Ver2 + 1} to {Edges[i].Ver1 + 1}"
                                            );

                                        chooseEdgeForm.WasCanceled = res.Item2;
                                        chooseEdgeForm.IsFirstAction = res.Item3;
                                        // Если не отменили изменение длины, то меняем.
                                        if (!chooseEdgeForm.WasCanceled)
                                        {
                                            if (chooseEdgeForm.IsFirstAction)
                                            {
                                                //getEdgeLengthForm.ShowDialog();

                                                var result1 = getEdgeLengthForm.MyShow();

                                                getEdgeLengthForm.WasCanceled = result1.Item2;
                                                getEdgeLengthForm.Weight = result1.Item3;

                                                if (!getEdgeLengthForm.WasCanceled)
                                                {
                                                    Edges[i].Weight = getEdgeLengthForm.Weight;

                                                    PrintAdjacencyMatrix();

                                                    return;
                                                }

                                                return;
                                            }

                                            //getEdgeLengthForm.ShowDialog();
                                            var result = getEdgeLengthForm.MyShow();

                                            getEdgeLengthForm.WasCanceled = result.Item2;
                                            getEdgeLengthForm.Weight = result.Item3;
                                            if (!getEdgeLengthForm.WasCanceled)
                                            {
                                                for (int j = i; j < Edges.Count; j++)
                                                {
                                                    if (Edges[j].Ver1 == Edges[i].Ver2 &&
                                                        Edges[j].Ver2 == Edges[i].Ver1)
                                                    {
                                                        Edges[j].Weight = getEdgeLengthForm.Weight;

                                                        PrintAdjacencyMatrix();
                                                        return;
                                                    }
                                                }
                                            }
                                            else
                                                return;
                                        }
                                        else
                                            return;
                                    }
                                    else
                                    {
                                        //getEdgeLengthForm.ShowDialog();
                                        var result = getEdgeLengthForm.MyShow();

                                        getEdgeLengthForm.WasCanceled = result.Item2;
                                        getEdgeLengthForm.Weight = result.Item3;
                                        if (!getEdgeLengthForm.WasCanceled)
                                        {
                                            Edges[i].Weight = getEdgeLengthForm.Weight;

                                            PrintAdjacencyMatrix();

                                            return;
                                        }

                                        return;
                                    }
                                }
                            }
                            else
                            {
                                // Если x координаты вершин ребра совпали,
                                // то проводим между ними прямую, и
                                // "расширяем" её вверх и вниз на ширину кисти/2.
                                if (e.X <= Vertices[Edges[i].Ver1].X + 4 &&
                                    e.X >= Vertices[Edges[i].Ver1].X - 4 &&
                                    e.Y < Math.Max(
                                        Vertices[Edges[i].Ver1].Y,
                                        Vertices[Edges[i].Ver2].Y) - Consts.VertexRadius &&
                                    e.Y > Math.Min(
                                        Vertices[Edges[i].Ver1].Y,
                                        Vertices[Edges[i].Ver2].Y) + Consts.VertexRadius)
                                {
                                    // Если есть обратное ребро, то даём выбрать,
                                    // длину какого пути надо изменить.
                                    if (adjMatrix[Edges[i].Ver1, Edges[i].Ver2] != 0 &&
                                        adjMatrix[Edges[i].Ver2, Edges[i].Ver1] != 0)
                                    {
                                        //chooseEdgeFormForm.TextForUnderstandingLabel.Text =
                                        //    "Длину какого пути Вы хотите изменить:";

                                        //chooseEdgeFormForm.FirstOptionButton.Text =
                                        //    $"Изменить длину пути из {Edges[i].Ver1 + 1} в " +
                                        //    $"{Edges[i].Ver2 + 1}";

                                        //chooseEdgeFormForm.SecondOptionButton.Text =
                                        //    $"Изменить длину пути из {Edges[i].Ver2 + 1} в " +
                                        //    $"{Edges[i].Ver1 + 1}";

                                        var res = chooseEdgeForm.MyShowChangeLength(
                                            "Choose the edge whose weight\n          you want to change",
                                            $"Change edge weight from {Edges[i].Ver1 + 1} to {Edges[i].Ver2 + 1}",
                                            $"Change edge weight from {Edges[i].Ver2 + 1} to {Edges[i].Ver1 + 1}"
                                        );

                                        chooseEdgeForm.WasCanceled = res.Item2;
                                        chooseEdgeForm.IsFirstAction = res.Item3;

                                        // Если не отменили, то меняем длину.
                                        if (!chooseEdgeForm.WasCanceled)
                                        {
                                            if (chooseEdgeForm.IsFirstAction)
                                            {
                                                var result2 = getEdgeLengthForm.MyShow();

                                                getEdgeLengthForm.WasCanceled = result2.Item2;
                                                getEdgeLengthForm.Weight = result2.Item3;

                                                if (!getEdgeLengthForm.WasCanceled)
                                                {
                                                    Edges[i].Weight = getEdgeLengthForm.Weight;

                                                    PrintAdjacencyMatrix();

                                                    return;
                                                }

                                                return;
                                            }

                                            var result = getEdgeLengthForm.MyShow();

                                            getEdgeLengthForm.WasCanceled = result.Item2;
                                            getEdgeLengthForm.Weight = result.Item3;

                                            if (!getEdgeLengthForm.WasCanceled)
                                            {
                                                for (int j = i; j < Edges.Count; j++)
                                                {
                                                    if (Edges[j].Ver1 == Edges[i].Ver2 &&
                                                        Edges[j].Ver2 == Edges[i].Ver1)
                                                    {
                                                        Edges[j].Weight = getEdgeLengthForm.Weight;

                                                        PrintAdjacencyMatrix();
                                                        return;
                                                    }
                                                }
                                            }
                                            else
                                                return;
                                        }
                                        else
                                            return;
                                    }
                                    else
                                    {
                                        var result = getEdgeLengthForm.MyShow();

                                        getEdgeLengthForm.WasCanceled = result.Item2;
                                        getEdgeLengthForm.Weight = result.Item3;

                                        if (!getEdgeLengthForm.WasCanceled)
                                        {
                                            Edges[i].Weight = getEdgeLengthForm.Weight;

                                            PrintAdjacencyMatrix();

                                            return;
                                        }

                                        return;
                                    }
                                }
                            }
                        }
                    }
                }

                // Если нажата кнопка "Рисовать вершину".
                if (!DrawVertexButton.Enabled)
                {
                    // Проверяем, не попадаем ли мы на другую вершину.
                    if (Vertices.Any(t => Math.Abs(e.X - t.X) < 2 * Consts.VertexRadius &&
                                        Math.Abs(e.Y - t.Y) < 2 * Consts.VertexRadius))
                    {
                        myMessageBox.NotifyVertexMustNotCross("Vertex must not cross");
                        return;
                    }

                    var newVertex = new Vertex(e.X, e.Y);

                    if (newVertex.X >= field.Size.Width - 2 * Consts.VertexRadius ||
                        newVertex.X <= 2 * Consts.VertexRadius ||
                        newVertex.Y <= 2 * Consts.VertexRadius ||
                        newVertex.Y >= field.Size.Height - 2 * Consts.VertexRadius)
                    {
                        myMessageBox.NotifyInvalidPlaceForVertex();
                        return;
                    }

                    Vertices.Add(new Vertex(e.X, e.Y));
                    ToolsForDrawing.DrawVertex(e.X, e.Y, Vertices.Count.ToString());
                    field.Image = ToolsForDrawing.GetBitmap();

                    PrintAdjacencyMatrix();
                }

                //Если нажата кнопка "Рисовать ребро".
                if (!DrawEdgeButton.Enabled)
                {
                    // Смотрим, на какую вершину нажали, затем выделяем её.
                    for (int i = 0; i < Vertices.Count; i++)
                    {
                        if (Math.Pow(Vertices[i].X - e.X, 2) + Math.Pow(Vertices[i].Y - e.Y, 2) <=
                            Math.Pow(Consts.VertexRadius, 2))
                        {
                            // Если это первая вершина.
                            if (ver1ForConnection == -1)
                            {
                                ToolsForDrawing.DrawSelectedVertex(Vertices[i].X, Vertices[i].Y);
                                ver1ForConnection = i;
                                field.Image = ToolsForDrawing.GetBitmap();
                                break;
                            }

                            // Если это вторая веришна.
                            if (ver2ForConnection == -1)
                            {
                                ToolsForDrawing.DrawSelectedVertex(Vertices[i].X, Vertices[i].Y);
                                ver2ForConnection = i;
                                field.Image = ToolsForDrawing.GetBitmap();

                                // Если между ними уже есть ребро, то предупреждаем об этом.
                                if (adjMatrix[ver1ForConnection, ver2ForConnection] == 0)
                                {
                                    var result = getEdgeLengthForm.MyShow();

                                    getEdgeLengthForm.WasCanceled = result.Item2;
                                    getEdgeLengthForm.Weight = result.Item3;

                                    // Если решили отменить рисование ребра.
                                    if (getEdgeLengthForm.WasCanceled)
                                    {
                                        ToolsForDrawing.DrawVertex(
                                            Vertices[ver1ForConnection].X,
                                            Vertices[ver1ForConnection].Y,
                                            (ver1ForConnection + 1).ToString());

                                        ToolsForDrawing.DrawVertex(
                                            Vertices[ver2ForConnection].X,
                                            Vertices[ver2ForConnection].Y,
                                            (ver2ForConnection + 1).ToString());

                                        getEdgeLengthForm.WasCanceled = false;

                                        ver1ForConnection = ver2ForConnection = -1;
                                    }
                                    else
                                    {
                                        Edges.Add(new Edge(
                                            ver1ForConnection,
                                            ver2ForConnection,
                                            getEdgeLengthForm.Weight));

                                        ToolsForDrawing.DrawEdge(
                                            Vertices[ver1ForConnection],
                                            Vertices[ver2ForConnection],
                                            Edges[Edges.Count - 1]);

                                        ver1ForConnection = -1;
                                        ver2ForConnection = -1;

                                        PrintAdjacencyMatrix();

                                        DrawVertexButton.Enabled = true;
                                    }

                                    field.Image = ToolsForDrawing.GetBitmap();

                                    break;
                                }

                                myMessageBox.NotifyEdgeExists("Edge from vertex" +
                                                            $" {ver1ForConnection + 1} to vertex " +
                                                            $"{ver2ForConnection + 1} \n" +
                                                            GetSpaces(15) + "already exists",
                                    ver1ForConnection + 1, ver2ForConnection + 1);

                                // Убираем выделение с вершин.
                                ToolsForDrawing.DrawVertex(
                                    Vertices[ver1ForConnection].X,
                                    Vertices[ver1ForConnection].Y,
                                    (ver1ForConnection + 1).ToString());

                                ToolsForDrawing.DrawVertex(
                                    Vertices[ver2ForConnection].X,
                                    Vertices[ver2ForConnection].Y,
                                    (ver2ForConnection + 1).ToString());

                                ver1ForConnection = ver2ForConnection = -1;

                                field.Image = ToolsForDrawing.GetBitmap();
                            }
                        }
                    }
                }

                // Нажата кнопка "Удалить элемент".
                if (!DeleteElementButton.Enabled)
                {
                    bool somethingDeleted = false;

                    // Если удаляем вершину.
                    // Предлагаем также отказаться от удаления.
                    for (int i = 0; i < Vertices.Count; i++)
                    {
                        if (Math.Pow(Vertices[i].X - e.X, 2) + Math.Pow(Vertices[i].Y - e.Y, 2) <=
                            Math.Pow(Consts.VertexRadius, 2))
                        {
                            var confirmation = myMessageBox.NotifyDeleteElement("The result of this action is permanent\n" +
                                                                              GetSpaces(2) +
                                                                              "Are you sure you want to continue?", "Confirmation");

                            if (confirmation == DialogResult.Yes)
                            {
                                for (int j = 0; j < Edges.Count; j++)
                                {
                                    if (Edges[j].Ver1 == i || Edges[j].Ver2 == i)
                                    {
                                        Edges.RemoveAt(j);
                                        j--;
                                    }
                                    else
                                    {
                                        // Уменьшаем номер веришны у рёбер,
                                        // которые соединены с вершинами,
                                        // у которых ноер больше
                                        if (Edges[j].Ver1 > i)
                                            Edges[j].Ver1--;

                                        if (Edges[j].Ver2 > i)
                                            Edges[j].Ver2--;
                                    }
                                }

                                Vertices.RemoveAt(i);
                                somethingDeleted = true;
                                break;
                            }

                            somethingDeleted = true;
                            break;
                        }
                    }

                    // Если удаляем ребро.
                    // Предлагаем также отказаться от удаления.
                    if (!somethingDeleted)
                    {
                        for (var i = 0; i < Edges.Count; i++)
                        {
                            // Если это петля
                            if (Edges[i].Ver1 == Edges[i].Ver2)
                            {
                                // Петля - это круг. и +- ширина кисти/2 к радиусу.
                                // У нас центр окружности петли смещён на -R -R.
                                // Если клик попал в тор, то предлагаем изменить длину.
                                if ((Math.Pow(Vertices[Edges[i].Ver1].X - Consts.VertexRadius - e.X, 2)
                                     +
                                     Math.Pow(Vertices[Edges[i].Ver1].Y - Consts.VertexRadius - e.Y, 2))
                                    <=
                                    Math.Pow(Consts.VertexRadius + 4, 2)

                                    &&

                                    (Math.Pow(Vertices[Edges[i].Ver1].X - Consts.VertexRadius - e.X, 2)
                                     +
                                     Math.Pow(Vertices[Edges[i].Ver1].Y - Consts.VertexRadius - e.Y, 2))
                                    >=
                                    Math.Pow(Consts.VertexRadius - 4, 2))
                                {
                                    var confirm = myMessageBox.NotifyDeleteElement("The result of this action is permanent\n" +
                                                                   GetSpaces(2) +
                                                                   "Are you sure you want to continue?", "Confirmation");

                                    if (confirm == DialogResult.Yes)
                                    {
                                        Edges.RemoveAt(i);
                                        somethingDeleted = true;
                                    }

                                    break;
                                }
                            }
                            else
                            {
                                // Если x координаты вершин ребра не совпдадают.
                                if (Vertices[Edges[i].Ver1].X != Vertices[Edges[i].Ver2].X)
                                {
                                    // Вычисляем коэффициенты в уравнении прямой,
                                    // проходящей через две вершины этого ребра, вида y = kx + b.
                                    double k = MyMath.GetK(
                                        Vertices[Edges[i].Ver1], Vertices[Edges[i].Ver2]);

                                    double b = MyMath.GetB(
                                        Vertices[Edges[i].Ver1], Vertices[Edges[i].Ver2]);

                                    // Проводим прямую между двумя веришнами ребра,
                                    // и "расширяем" её на ширину кисти/2.
                                    // Если попали в область, то предлагаем удалить это ребро.
                                    if (e.Y <= k * e.X + b + 4 && e.Y >= k * e.X + b - 4)
                                    {
                                        // Если есть обратный путь, то предлагаем выбрать путь,
                                        // который надо удалить.
                                        if (adjMatrix[Edges[i].Ver1, Edges[i].Ver2] != 0 &&
                                            adjMatrix[Edges[i].Ver2, Edges[i].Ver1] != 0)
                                        {
                                            var (caption, text1, text2) = GetTextInButtonForChooseEdgeForm(
                                                 Edges[i].Ver1, Edges[i].Ver2);

                                            //chooseEdgeFormForm.ShowDialog();
                                            var res = chooseEdgeForm.MyShowDeleteEdge(caption, text1, text2);
                                            chooseEdgeForm.WasCanceled = res.Item2;
                                            chooseEdgeForm.IsFirstAction = res.Item3;
                                            // Если не нажали отмену, то удаляем.
                                            if (!chooseEdgeForm.WasCanceled)
                                            {
                                                if (chooseEdgeForm.IsFirstAction)
                                                {
                                                    Edges.RemoveAt(i);
                                                    somethingDeleted = true;

                                                    chooseEdgeForm.IsFirstAction = false;
                                                    break;
                                                }

                                                for (int j = 0; j < Edges.Count; j++)
                                                {
                                                    if (Edges[j].Ver1 == Edges[i].Ver2 &&
                                                        Edges[j].Ver2 == Edges[i].Ver1)
                                                    {
                                                        Edges.RemoveAt(j);
                                                        break;
                                                    }
                                                }

                                                somethingDeleted = true;
                                                break;
                                            }

                                            return;
                                        }

                                        var confirm = myMessageBox.NotifyDeleteElement("The result of this action is permanent\n" +
                                                                                     GetSpaces(2) +
                                                                                     "Are you sure you want to continue?", "Confirmation");

                                        if (confirm == DialogResult.Yes)
                                        {
                                            Edges.RemoveAt(i);
                                            somethingDeleted = true;
                                        }

                                        break;
                                    }
                                }

                                // Если x координаты вершин ребра совпадают.
                                if (Vertices[Edges[i].Ver1].X == Vertices[Edges[i].Ver2].X)
                                {
                                    // Если x координаты вершин ребра совпали,
                                    // то проводим между ними прямую, и
                                    // "расширяем" её вверх и вниз на ширину кисти/2.
                                    if (e.X <= Vertices[Edges[i].Ver1].X + 4 &&
                                        e.X >= Vertices[Edges[i].Ver1].X - 4 &&
                                        e.Y < Math.Max(
                                            Vertices[Edges[i].Ver1].Y,
                                            Vertices[Edges[i].Ver2].Y) - Consts.VertexRadius &&
                                        e.Y > Math.Min(
                                            Vertices[Edges[i].Ver1].Y,
                                            Vertices[Edges[i].Ver2].Y) + Consts.VertexRadius)
                                    {
                                        // Если есть обратный путь, то предлагаем выбрать путь,
                                        // который надо удалить.
                                        if (adjMatrix[Edges[i].Ver1, Edges[i].Ver2] != 0 &&
                                            adjMatrix[Edges[i].Ver2, Edges[i].Ver1] != 0)
                                        {
                                            var (caption, text1, text2) = GetTextInButtonForChooseEdgeForm(
                                                Edges[i].Ver1, Edges[i].Ver2);

                                            var res = chooseEdgeForm.MyShowDeleteEdge(caption, text1, text2);
                                            chooseEdgeForm.WasCanceled = res.Item2;
                                            chooseEdgeForm.IsFirstAction = res.Item3;

                                            // Если не нажали отмену, то удаляем.
                                            if (!chooseEdgeForm.WasCanceled)
                                            {
                                                if (chooseEdgeForm.IsFirstAction)
                                                {
                                                    Edges.RemoveAt(i);
                                                    somethingDeleted = true;
                                                    chooseEdgeForm.IsFirstAction = false;
                                                    break;
                                                }

                                                for (int j = 0; j < Edges.Count; j++)
                                                {
                                                    if (Edges[j].Ver1 == Edges[i].Ver2)
                                                    {
                                                        Edges.RemoveAt(j);
                                                        break;
                                                    }
                                                }

                                                somethingDeleted = true;
                                                break;
                                            }

                                            return;
                                        }

                                        var confirm = myMessageBox.NotifyDeleteElement("The result of this action is permanent\n" +
                                                                                     GetSpaces(2) +
                                                                                     "Are you sure you want to continue?", "Confirmation");

                                        if (confirm == DialogResult.Yes)
                                        {
                                            Edges.RemoveAt(i);
                                            somethingDeleted = true;
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }

                    // Если что-то удалили, то перерисовываем граф.
                    if (somethingDeleted)
                    {
                        ToolsForDrawing.ClearField();
                        ToolsForDrawing.DrawFullGraph(Edges, Vertices);
                        field.Image = ToolsForDrawing.GetBitmap();
                        PrintAdjacencyMatrix();
                    }
                }

                HideAdjacencyMatrix();
            }
        }

        /// <summary>
        /// Записываем в файл необходимые данные.
        /// </summary>
        private void SaveToolsForGraph()
        {
            File.WriteAllText(pathForSaveFile, default);

            // Сначала число вершин, затем сами вершины.
            File.AppendAllText(pathForSaveFile, Vertices.Count + Environment.NewLine);

            foreach (var vertex in Vertices)
                File.AppendAllText(pathForSaveFile, vertex + Environment.NewLine);

            // Сначала число рёбер, потом сами рёбра.
            File.AppendAllText(pathForSaveFile, Edges.Count + Environment.NewLine);

            for (int i = 0; i < Edges.Count; i++)
                if (i != Edges.Count - 1)
                    File.AppendAllText(pathForSaveFile, Edges[i] + Environment.NewLine);
                else
                    File.AppendAllText(pathForSaveFile, Edges[i].ToString());
        }

        /// <summary>
        /// Кнопка для отмены рисования ребра или перетаскивания вершины,
        /// когда выделена 1 вершина.
        /// </summary>
        /// <param name="sender"> Ссылка на объект, вызвавший событие </param>
        /// <param name="e"> Нажатая кнопка </param>
        private void MainFormKeyPress(object sender, KeyPressEventArgs e)
        {
            // Отмена рисования ребра (нажатие ESC).
            if (ver1ForConnection != -1 && ver2ForConnection == -1)
                if (e.KeyChar == (char)Keys.Escape)
                {
                    ToolsForDrawing.DrawVertex(
                        Vertices[ver1ForConnection].X,
                        Vertices[ver1ForConnection].Y,
                        (ver1ForConnection + 1).ToString());
                    ver1ForConnection = -1;
                }

            // Отмена перетаскивания вершины (нажатие ESC).
            if (!firstPress)
            {
                if (e.KeyChar == (char)Keys.Escape)
                {
                    ToolsForDrawing.DrawVertex(
                        Vertices[indexVertexForMove].X,
                        Vertices[indexVertexForMove].Y,
                        (indexVertexForMove + 1).ToString());
                    indexVertexForMove = -1;

                    firstPress = true;
                }
            }

            field.Image = ToolsForDrawing.GetBitmap();
        }

        /// <summary>
        /// Устанавливаем необходимые данные из файла.
        /// </summary>
        private void SetDataFromFile()
        {
            int vertexCount;
            int edgesCount;

            Vertices.Clear();
            Edges.Clear();

            string[] lines = File.ReadAllLines(pathForOpenFile);

            // Количество вершин.
            vertexCount = int.Parse(lines[0]);

            // Заполняем список вершин.
            for (int i = 1; i < vertexCount + 1; i++)
            {
                int xCoord = int.Parse(lines[i].Split(' ')[0]);
                int yCoord = int.Parse(lines[i].Split(' ')[1]);

                Vertices.Add(new Vertex(xCoord, yCoord));
            }

            // Количество рёбер.
            edgesCount = int.Parse(lines[1 + vertexCount]);

            // Заполняем список рёбер.
            for (int i = 2 + vertexCount; i < 2 + vertexCount + edgesCount; i++)
            {
                int ver1 = int.Parse(lines[i].Split(' ')[0]);
                int ver2 = int.Parse(lines[i].Split(' ')[1]);
                double disctance = double.Parse(lines[i].Split(' ')[2]);

                Edges.Add(new Edge(ver1, ver2, disctance));
            }
        }

        /// <summary>
        /// При нажатии на эту форму переключаемся на неё.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormClick(object sender, EventArgs e) =>
            Activate();

        /// <summary>
        /// Вывод матрицы смежности в ListBox.
        /// </summary>
        public void PrintAdjacencyMatrix()
        {
            // Создаём и заполняем матрицу по листам смежности.
            adjMatrix = new double[Vertices.Count, Vertices.Count];

            ToolsForDrawing.FillAdjacencyMatrix(Vertices.Count, Edges, adjMatrix);

            showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Clear();

            string curStrForPrint;
            string caption;

            string strFromHyphen = GetHyphens(5) + "|";

            caption = GetSpaces(5) + "|" + GetSpaces(3);

            // Число разрядов в самом "длинном" числе, с учётом запятой.
            int maxDigitsInMatrix;

            // Когда заполняем первую строку, мы проходимся по всем столбцам,
            // и во время прохождения нумеруем эти столбцы.
            // Переменная отвечает за то, чтобы мы нумеровали
            // столбцы только при первом проходе.
            bool firstRaw = true;

            maxDigitsInMatrix = GetMaxAmountDigits();

            // В зависимости от maxDigitsInMatrix и от длины
            // текущего числа формируем матрицу смежности.
            for (int i = 0; i < Vertices.Count; i++)
            {
                // Отдельно высчитываем расстояние между столбцами для двузначных чисел.
                if (i < 9)
                    curStrForPrint = (i + 1) + GetSpaces(3) + "|" + GetSpaces(3);
                else
                    curStrForPrint = (i + 1) + GetSpaces(1) + "|" + GetSpaces(3);

                for (int j = 0; j < Vertices.Count; j++)
                {
                    switch (maxDigitsInMatrix)
                    {
                        case 0:
                        case 1:
                            curStrForPrint += adjMatrix[i, j] + GetSpaces(5);

                            // Формируем первую и вторую строки матрицы.
                            if (firstRaw)
                            {
                                if (j < 9)
                                {
                                    caption += (j + 1) + GetSpaces(5);

                                    strFromHyphen += GetHyphens(7);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(3);

                                    strFromHyphen += GetHyphens(10);
                                }
                            }

                            break;

                        case 2:
                            switch (MyMath.GetAmountOfDigits(adjMatrix[i, j]))
                            {
                                case 0:
                                case 1:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(7);
                                    break;

                                case 2:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(5);
                                    break;
                            }

                            // Формируем первую и вторую строки матрицы.
                            if (firstRaw)
                            {
                                if (j < 9)
                                {
                                    caption += (j + 1) + GetSpaces(7);

                                    strFromHyphen += GetHyphens(9);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(5);

                                    strFromHyphen += GetHyphens(12);
                                }
                            }

                            break;

                        case 3:
                            switch (MyMath.GetAmountOfDigits(adjMatrix[i, j]))
                            {
                                case 0:
                                case 1:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(9);
                                    break;

                                case 2:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(7);
                                    break;

                                case 3:
                                    if (IsInt(adjMatrix[i, j]))
                                        curStrForPrint += adjMatrix[i, j] + GetSpaces(5);
                                    else
                                        curStrForPrint += adjMatrix[i, j] + GetSpaces(6);
                                    break;
                            }

                            // Формируем первую и вторую строки матрицы.
                            if (firstRaw)
                            {
                                if (j < 9)
                                {
                                    caption += (j + 1) + GetSpaces(9);

                                    strFromHyphen += GetHyphens(11);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(7);

                                    strFromHyphen += GetHyphens(13);
                                }
                            }

                            break;

                        case 4:
                            switch (MyMath.GetAmountOfDigits(adjMatrix[i, j]))
                            {
                                case 0:
                                case 1:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(10);
                                    break;

                                case 2:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(8);
                                    break;

                                case 3:
                                    if (IsInt(adjMatrix[i, j]))
                                        curStrForPrint += adjMatrix[i, j] + GetSpaces(6);
                                    else
                                        curStrForPrint += adjMatrix[i, j] + GetSpaces(7);
                                    break;

                                case 4:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(5);
                                    break;
                            }

                            // Формируем первую и вторую строки матрицы.
                            if (firstRaw)
                            {
                                if (j < 9)
                                {
                                    caption += (j + 1) + GetSpaces(10);

                                    strFromHyphen += GetHyphens(13);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(8);

                                    strFromHyphen += GetHyphens(15);
                                }
                            }

                            break;

                        case 5:
                            switch (MyMath.GetAmountOfDigits(adjMatrix[i, j]))
                            {
                                case 0:
                                case 1:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(11);
                                    break;

                                case 2:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(9);
                                    break;

                                case 3:
                                    if (IsInt(adjMatrix[i, j]))
                                        curStrForPrint += adjMatrix[i, j] + GetSpaces(7);
                                    else
                                        curStrForPrint += adjMatrix[i, j] + GetSpaces(8);
                                    break;

                                case 4:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(6);
                                    break;

                                case 5:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(4);
                                    break;
                            }

                            // Формируем первую и вторую строки матрицы.
                            if (firstRaw)
                            {
                                if (j < 9)
                                {
                                    caption += (j + 1) + GetSpaces(11);

                                    strFromHyphen += GetHyphens(14);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(9);

                                    strFromHyphen += GetHyphens(16);
                                }
                            }

                            break;

                        case 6:
                            switch (MyMath.GetAmountOfDigits(adjMatrix[i, j]))
                            {
                                case 0:
                                case 1:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(12);
                                    break;

                                case 2:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(10);
                                    break;

                                case 3:
                                    if (IsInt(adjMatrix[i, j]))
                                        curStrForPrint += adjMatrix[i, j] + GetSpaces(8);
                                    else
                                        curStrForPrint += adjMatrix[i, j] + GetSpaces(9);
                                    break;

                                case 4:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(7);
                                    break;

                                case 5:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(5);
                                    break;

                                case 6:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(3);
                                    break;
                            }

                            // Формируем первую и вторую строки матрицы.
                            if (firstRaw)
                            {
                                if (j < 9)
                                {
                                    caption += (j + 1) + GetSpaces(12);

                                    strFromHyphen += GetHyphens(15);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(10);

                                    strFromHyphen += GetHyphens(18);
                                }
                            }

                            break;

                        case 7:
                            switch (MyMath.GetAmountOfDigits(adjMatrix[i, j]))
                            {
                                case 0:
                                case 1:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(14);
                                    break;

                                case 2:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(12);
                                    break;

                                case 3:
                                    if (IsInt(adjMatrix[i, j]))
                                        curStrForPrint += adjMatrix[i, j] + GetSpaces(10);
                                    else
                                        curStrForPrint += adjMatrix[i, j] + GetSpaces(11);
                                    break;

                                case 4:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(9);
                                    break;

                                case 5:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(7);
                                    break;

                                case 6:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(5);
                                    break;

                                case 7:
                                    curStrForPrint += adjMatrix[i, j] + GetSpaces(3);
                                    break;
                            }

                            // Формируем первую и вторую строки матрицы.
                            if (firstRaw)
                            {
                                if (j < 9)
                                {
                                    caption += (j + 1) + GetSpaces(14);

                                    strFromHyphen += GetHyphens(17);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(12);

                                    strFromHyphen += GetHyphens(19);
                                }
                            }

                            break;
                    }
                }

                firstRaw = false;

                // Добавляем сформированную строку в ListBox/
                showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Add(curStrForPrint);

                // Делаем пропуск строки, если это не последняя строка.
                if (i != Vertices.Count - 1)
                    showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Add(GetSpaces(5) + "|");
            }

            // Вставляем первую и вторую строки в ListBox.
            showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Insert(0, caption);
            showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Insert(1, strFromHyphen);

            // Если вершин нет, закрываем матрицу.
            // Если есть, то выводим матрицу, при этом оставляем фокус на этом окне.
            if (Vertices.Count == 0)
            {
                showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Clear();
                showAdjacencyMatrixForm.Hide();
            }
            else if (ShowOrHideAdjMatrix.Text == "Close matrix")
            {
                showAdjacencyMatrixForm.Show();
                Activate();
            }
        }

        /// <summary>
        /// Добавляем требуемое число пробелов к строке.
        /// </summary>
        /// <param name="amount"> Количество пробелов </param>
        /// <returns> Строка из пробелов </returns>
        private static string GetSpaces(int amount)
        {
            string result = default;

            for (var i = 0; i < amount; i++)
                result += " ";

            return result;
        }

        /// <summary>
        /// Добавляем требуемое число дефисов к строке.
        /// </summary>
        /// <param name="amount"> Количество дефисов </param>
        /// <returns> Строка из дефисов </returns>
        private static string GetHyphens(int amount)
        {
            string result = default;

            for (var i = 0; i < amount; i++)
                result += "-";

            return result;
        }

        /// <summary>
        /// Ищем самое "длинное" число в матрице смежности,
        /// и считаем количество разрядов этого числа, с учётом запятой.
        /// </summary>
        /// <returns> Число разрядов самого "длинного" числа в матрице смежности </returns>
        private int GetMaxAmountDigits()
        {
            int maxDigitsCount = int.MinValue;

            for (int i = 0; i < adjMatrix.GetLength(1); i++)
                for (int j = 0; j < adjMatrix.GetLength(0); j++)
                    if (MyMath.GetAmountOfDigits(adjMatrix[i, j]) > maxDigitsCount)
                        maxDigitsCount = MyMath.GetAmountOfDigits(adjMatrix[i, j]);

            return maxDigitsCount;
        }

        /// <summary>
        /// Проверка, является ли число целым.
        /// </summary>
        /// <param name="val"> Число </param>
        /// <returns> true, если число целое,
        ///           false, если нет </returns>
        private static bool IsInt(double val) =>
            int.TryParse(val.ToString(), out var temp);

        /// <summary>
        /// Построение графика.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerForPlottingTick(object sender, EventArgs e)
        {
            var xValue = (timerForPlotting.ElapsedMilliseconds) / 1000;
            var yValue = totalPointsCount;

            if (pointsForComputeFormula.All(p => p.X != xValue))
            {
                pointsForComputeFormula.Add(new MyPoint(xValue, yValue));
            }
            chartForm.chartForm.Series["Amount of points"].Points.AddXY(xValue, yValue);
        }

        /// <summary>
        /// Моделирование случайных блужданий.
        /// </summary>
        /// <param name="adjacencyList"></param>
        public void MainTick(List<Edge>[] adjacencyList)
        {
            SpeedCoefficient = (float)(trackBar1.Value / 10.0);
            // Если готова выпустить точку.
            var count = points.Count;

            for (var i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i].HasPoint)
                {
                    Vertices[i].HasPoint = false;

                    if (isFirstVertex)
                    {
                        isFirstVertex = false;
                        totalPointsCount += adjacencyList[i].Count;
                    }
                    else
                    {
                        totalPointsCount += adjacencyList[i].Count - 1;
                    }

                    points.AddRange(adjacencyList[i]);

                    timers.AddRange(adjacencyList[i].ConvertAll(el => new Stopwatch()));
                }
            }

            for (var i = count; i < timers.Count; i++)
            {
                timers[i].Start();
            }

            ToolsForDrawing.DrawFullGraph(Edges, Vertices);

            for (var i = 0; i < points.Count; i++)
            {
                if (timers[i].ElapsedMilliseconds * SpeedCoefficient > points[i].Weight * 1000)
                {
                    Vertices[points[i].Ver2].HasPoint = true;
                    points.RemoveAt(i);
                    timers.RemoveAt(i);
                    i--;
                    continue;
                }

                PointF point = GetCurPointPosition(Vertices[points[i].Ver1],
                    Vertices[points[i].Ver2], points[i].Weight, timers[i]);

                ToolsForDrawing.DrawPoint(point.X, point.Y);

                field.Image = ToolsForDrawing.GetBitmap();
            }

            field.Image = ToolsForDrawing.GetBitmap();
        }

        /// <summary>
        /// Получить текущую позицию точки.
        /// </summary>
        /// <param name="ver1"> Первая вершина </param>
        /// <param name="ver2"> Вторая вершина </param>
        /// <param name="fullTime"> Суммарное время </param>
        /// <param name="timer"> Текущее время </param>
        /// <returns></returns>
        private PointF GetCurPointPosition(Vertex ver1, Vertex ver2, double fullTime, Stopwatch timer)
        {
            var distanceX = Math.Abs(ver2.X - ver1.X);
            var distanceY = Math.Abs(ver2.Y - ver1.Y);

            float x = 0;
            float y = 0;

            // Loop. x^2 + y^2 = 400
            if (ver1 == ver2)
            {
                // Левая нижняя дуга.
                if (timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0 <= fullTime / 4)
                {
                    x = (float)(100 / fullTime * timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0);
                    y = (float)Math.Sqrt(400 - x * x);

                    if (x * x >= 400)
                    //return new PointF(ver1.X - 20, ver1.Y);
                    {
                        x = (float)(100 / fullTime * (timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0 - fullTime / 4));
                        y = -(float)Math.Sqrt(400 - x * x);

                        x += 20;
                        y -= 20;
                        return new PointF(y + ver1.X, ver1.Y - x);
                    }

                    x += 20;
                    y -= 20;
                    return new PointF(-x + ver1.X, ver1.Y + y);
                }

                if (timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0 <= fullTime / 2)
                {
                    //Верхняя правая дуга.
                    x = (float)(100 / fullTime * (timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0 - fullTime / 4));
                    y = -(float)Math.Sqrt(400 - x * x);

                    if (x * x >= 400)
                    {
                        x = -(float)(100 / fullTime * (timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0 - fullTime / 2));
                        y = -(float)Math.Sqrt(400 - x * x);

                        x += 20;
                        y -= 20;
                        return new PointF(-x + ver1.X, ver1.Y + y);
                    }

                    x += 20;
                    y -= 20;
                    return new PointF(y + ver1.X, ver1.Y - x);
                }

                if (timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0 <= fullTime * 3 / 4)
                {
                    // Правая верхняя дуга
                    x = -(float)(100 / fullTime * (timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0 - fullTime / 2));
                    y = -(float)Math.Sqrt(400 - x * x);

                    if (x * x >= 400)
                    {
                        x = -(float)(100 / fullTime * (timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0 - fullTime * 3 / 4));
                        y = -(float)Math.Sqrt(400 - x * x);

                        x += 20;
                        y += 20;
                        return new PointF(-y + ver1.X, ver1.Y - x);
                    }

                    x += 20;
                    y -= 20;
                    return new PointF(-x + ver1.X, ver1.Y + y);
                }

                if (timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0 <= fullTime)
                {
                    x = -(float)(100 / fullTime * (timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0 - fullTime * 3 / 4));
                    y = -(float)Math.Sqrt(400 - x * x);

                    if (x * x >= 400)
                    {
                        x = (float)(100 / fullTime * timer.ElapsedMilliseconds * SpeedCoefficient / 1000.0);
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
                        x = (float)(distanceX / fullTime * timer.ElapsedMilliseconds * SpeedCoefficient / 1000) + ver1.X;
                        y = -(float)(distanceY / fullTime * timer.ElapsedMilliseconds * SpeedCoefficient / 1000) + ver1.Y;
                    }
                    else
                    {
                        x = (float)(distanceX / fullTime * timer.ElapsedMilliseconds * SpeedCoefficient / 1000) + ver1.X;
                        y = (float)(distanceY / fullTime * timer.ElapsedMilliseconds * SpeedCoefficient / 1000) + ver1.Y;
                    }
                }
                else
                {
                    if (ver1.Y > ver2.Y)
                    {
                        x = -(float)(distanceX / fullTime * timer.ElapsedMilliseconds * SpeedCoefficient / 1000) + ver1.X;
                        y = -(float)(distanceY / fullTime * timer.ElapsedMilliseconds * SpeedCoefficient / 1000) + ver1.Y;
                    }
                    else
                    {
                        x = -(float)(distanceX / fullTime * timer.ElapsedMilliseconds * SpeedCoefficient / 1000) + ver1.X;
                        y = (float)(distanceY / fullTime * timer.ElapsedMilliseconds * SpeedCoefficient / 1000) + ver1.Y;
                    }
                }
            }

            return new PointF(x, y);
        }

        /// <summary>
        /// Начало исследования.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartReseachButtonClick(object sender, EventArgs e)
        {
            RedrawSelectedVertex();
            HideAdjacencyMatrix();

            chartForm.chartForm.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chartForm.chartForm.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chartForm.chartForm.ChartAreas[0].AxisX.Minimum = 0;
            chartForm.chartForm.ChartAreas[0].AxisX.Title = "Time";

            wasClickedContinue = true;
            CheckGraphForStrongConnectionButton.PerformClick();

            if (responseSC)
            {
                timer3.Stop();
                HideAllLabel();
                x2.Visible = true;
                x05.Visible = true;
                panelChart.Visible = true;
                panel3.Visible = true;
                drawingPanel.Visible = false;
                saveChartButton.Visible = true;
                openChartButton.Visible = true;
                StopProcessButton.Visible = true;
                testingProgrammButton.Visible = true;
                panel1.Visible = false;
                trackBar1.Visible = true;
                startReseachButton.Visible = false;
                Width -= 157;
                panel3.Location = new Point(Consts.GraphPictureBoxWidth, 5);
                trackBar1.Location = new Point(150, Consts.GraphPictureBoxHeight + 30);

                Vertices[0].HasPoint = true;
                var listArr = new List<Edge>[Vertices.Count];

                for (var i = 0; i < Vertices.Count; i++)
                {
                    List<Edge> curEdges = Edges.Where(el => el.Ver1 == i).ToList();
                    listArr[i] = new List<Edge>();
                    listArr[i].AddRange(curEdges);
                }

                field.Location = new Point(0, 5);

                mainTimer = new Timer { Interval = 2 };
                mainTimer.Tick += (x, y) => MainTick(listArr);
                mainTimer.Start();
                timer1.Start();
                timer2.Start();
                timerForPlotting.Start();
                chartForm.Show();
                Activate();
            }
            else
            {
                wasClickedContinue = false;
            }
        }

        /// <summary>
        /// Кнопка STOP/Continue.
        /// </summary>
        /// <param name="sender"> Sender </param>
        /// <param name="e"> E </param>
        private void StopProcessButton_Click(object sender, EventArgs e)
        {
            RequireTimeForTesting = timerForPlotting.ElapsedMilliseconds;

            if (StopProcessButton.Text == "Get main part")
            {
                mainTimer.Stop();
                timer1.Stop();
                timer2.Stop();
                timerForPlotting.Stop();
                timers.ForEach(timer => timer.Stop());
                StopProcessButton.Text = "Continue testing";
                StopProcessButton.Enabled = false;
                ShowResultOfMainPartByTime();
            }
            else
            {
                timerForPlotting.Start();
                mainTimer.Start();
                timer1.Start();
                timer2.Start();
                timers.ForEach(timer => timer.Start());
                StopProcessButton.Text = "Get main part";
            }
        }

        /// <summary>
        /// Показывает график с главной частью.
        /// </summary>
        private void SetAndShowPlotWithMainPartForm()
        {
            DisplayMainPartByTimeForm = new DisplayMainPartByTimeForm { pictureBox1 = { Image = null } };
            mathKernel1.GraphicsHeight = DisplayMainPartByTimeForm.pictureBox1.Height;
            mathKernel1.GraphicsWidth = DisplayMainPartByTimeForm.pictureBox1.Width;
            mathKernel1.Compute("Plot[f[x], {x, 0, " + $"{pointsForComputeFormula.Last().X}" + "}, " +
                                "AxesStyle -> Directive[Black, FontColor -> White], " +
                                "Background -> Gray, " +
                                "PlotStyle -> Red]");

            if (mathKernel1.Graphics.Length > 0)
            {
                DisplayMainPartByTimeForm.pictureBox1.Image = mathKernel1.Graphics[0];
                DisplayMainPartByTimeForm.Show();
                DisplayMainPartByTimeForm.Activate();
            }
        }

        /// <summary>
        /// Показывает график без главной части
        /// </summary>
        /// <param name="data"></param>
        /// <param name="value"></param>
        private void SetAndShowPlotWithoutMainPartForm(string data, double value)
        {
            DisplayMainPartByTimeForm = new DisplayMainPartByTimeForm { pictureBox1 = { Image = null } };
            mathKernel1.GraphicsHeight = DisplayMainPartByTimeForm.pictureBox1.Height;
            mathKernel1.GraphicsWidth = DisplayMainPartByTimeForm.pictureBox1.Width;
            mathKernel1.Compute($"g[x_] := {value}*x^{Alpha}");
            mathKernel1.Compute(data);
            mathKernel1.Compute("k= FindFormula[data, x]");
            mathKernel1.Compute("kf[x_]:=k");
            mathKernel1.Compute("Plot[{kf[x], g[x]}, {x, 0, " + $"{pointsForComputeFormula.Last().X}" + "}, " +
                                "AxesStyle -> Directive[Black, FontColor -> White], " +
                                "Background -> Gray, " +
                                "PlotStyle -> {Red, Yellow}]");

            if (mathKernel1.Graphics.Length > 0)
            {
                DisplayMainPartByTimeForm.pictureBox1.Image = mathKernel1.Graphics[0];
                DisplayMainPartByTimeForm.Show();
                DisplayMainPartByTimeForm.Activate();
            }
        }

        /// <summary>
        /// Строит годин из двух графиков:
        /// Либо с главной частью, либо - без.
        /// </summary>
        private void ShowResultOfMainPartByTime()
        {
            Alpha = CyclomaticNumber - 1;

            var (_, item2, item3) = getEdgeLengthForm.MyShow(true);

            getEdgeLengthForm.WasCanceled = item2;
            getEdgeLengthForm.Weight = item3;

            if (!getEdgeLengthForm.WasCanceled)
            {
                Alpha = getEdgeLengthForm.Weight;
            }

            var data = pointsForComputeFormula
                .Distinct()
                .Aggregate("{", (current, next) => current + ", " + next, res => res + "}")
                .TrimStart('{', ',', ' ');

            data = "data = {{" + data;

            mathKernel1.Compute(data);
            mathKernel1.Compute("formula = FindFormula[data, x] / (x^" + $"{Alpha})");
            mathKernel1.Compute("f[x_] := formula");
            mathKernel1.Compute("Limit[formula, x -> Infinity]");

            try
            {
                var value = double.Parse(mathKernel1.Result.ToString().Replace('.', ','));
                if (Math.Abs(value) < double.Epsilon)
                {
                    throw new ArgumentException();
                }

                SetAndShowPlotWithoutMainPartForm(data, value);
            }
            catch (Exception)
            {
                SetAndShowPlotWithMainPartForm();
            }

            StopProcessButton.Enabled = true;
        }
        
        /// <summary>
        /// Сохранить график в файл.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChartToCsvFileButtonClick(object sender, EventArgs e)
        {
            RedrawSelectedVertex();

            mainTimer.Stop();
            timer1.Stop();
            timer2.Stop();
            timerForPlotting.Stop();
            timers.ForEach(timer => timer.Stop());

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;
            DeleteElementButton.Enabled = true;

            // Проверяем, есть ли, что сохранять.
            if (field.Image != null && !(adjMatrix is null) && adjMatrix.Length != 0)
            {
                SaveFileDialog saveGraphDialog = new SaveFileDialog();
                saveGraphDialog.Title = "Save chart as...";
                saveGraphDialog.OverwritePrompt = true;
                saveGraphDialog.CheckPathExists = true;
                saveGraphDialog.Filter = "Files(*.CSV)|*.CSV";

                RequireTimeForTesting = timerForPlotting.ElapsedMilliseconds;
                saveGraphDialog.ShowHelp = true;

                // Процесс сохранения файла.
                if (saveGraphDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string sv = saveGraphDialog.FileName;
                        File.WriteAllText(sv, ChartFormatData.ToString());
                    }
                    catch (Exception)
                    {
                        myMessageBox.ShowError("Unable to save chart");
                    }
                }
            }
            else
                myMessageBox.NotifyGraphIsEmpty("Graph is empty");

            mainTimer.Start();
            timer1.Start();
            timer2.Start();
            timerForPlotting.Start();
            timers.ForEach(timer => timer.Start());
            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (Vertices.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Close matrix";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
        }

        /// <summary>
        /// Преобразование данных для сохранения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConvertDataToCsvFormatTimerTick(object sender, EventArgs e)
        {
            var time = TimeSpan.FromSeconds(pastSecondsCount++);

            var timeStr = time.ToString(@"hh\:mm\:ss");
            ChartFormatData.AppendLine($"{timeStr};{totalPointsCount}");
        }

        /// <summary>
        /// Открыть график из файла.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenChartFromCsvFileButtonClick(object sender, EventArgs e)
        {
            chartForOpenFromFileForm.chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chartForOpenFromFileForm.chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chartForOpenFromFileForm.chart1.ChartAreas[0].AxisX.Minimum = 0;

            RedrawSelectedVertex();

            mainTimer.Stop();
            timer1.Stop();
            timer2.Stop();
            timerForPlotting.Stop();
            timers.ForEach(timer => timer.Stop());

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;
            DeleteElementButton.Enabled = true;

            var openGraphDialog = new OpenFileDialog
            {
                Title = "Open chart...",
                Filter = "Imagine Files(*.CSV)|*.CSV",
                ShowHelp = true
            };

            // Процесс считывания информации из файла файла.
            if (openGraphDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pathForOpenFile = openGraphDialog.FileName;
                    List<string[]> lines = File.ReadAllLines(pathForOpenFile)
                                               .Select(el => el.Split(';'))
                                               .Skip(1)
                                               .ToList();

                    foreach (string[] line in lines)
                    {
                        var time = TimeSpan.Parse(line[0]).TotalSeconds;
                        var amount = int.Parse(line[1]);

                        chartForOpenFromFileForm.chart1.Series["Amount of points"]
                            .Points.AddXY(time, amount);
                    }

                    chartForOpenFromFileForm.Show();
                }
                catch (Exception)
                {
                    myMessageBox.ShowError("Unable to open chart");
                }

                mainTimer.Start();
                timer1.Start();
                timer2.Start();
                timerForPlotting.Start();
                timers.ForEach(timer => timer.Start());
            }
        }

        /// <summary>
        /// Начало тестирования программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartTestingProgramButtonClick(object sender, EventArgs e)
        {
            Hide();
            chartForm.Hide();
            showAdjacencyMatrixForm.Hide();
            mainTimer.Stop();
            timer1.Stop();
            timer2.Stop();
            timerForPlotting.Stop();
            timers.ForEach(timer => timer.Stop());
            RequireTimeForTesting = timerForPlotting.ElapsedMilliseconds;
            new ChartDisplayForTestingProgramForm(this, 0,
                new List<ChartDisplayForTestingProgramForm>());
        }

        /// <summary>
        /// Убрать все тексты.
        /// </summary>
        private void HideAllLabel()
        {
            drawEdgeLabel.Visible = false;
            drawVertexLabel.Visible = false;
            deleteElementLabel.Visible = false;
            changeLengthLabel.Visible = false;
            label1.Visible = false;
        }

        /// <summary>
        /// Загрузка формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormLoad(object sender, EventArgs e)
        {
            x05.Visible = false;
            x2.Visible = false;
            x2.Location = new Point(696, 616);
            x05.Location = new Point(123, 616);
            saveChartButton.Visible = false;
            openChartButton.Visible = false;
            testingProgrammButton.Visible = false;
            StopProcessButton.Visible = false;
            trackBar1.Visible = false;
            ShowAllSubMenu();
            panelChart.Visible = false;
            panel3.Visible = false;
            drawingPanel.Visible = true;
            drawEdgeLabel.BackColor = Color.FromArgb(35, 32, 39);
            drawVertexLabel.BackColor = Color.FromArgb(35, 32, 39);
            deleteElementLabel.BackColor = Color.FromArgb(35, 32, 39);
            changeLengthLabel.BackColor = Color.FromArgb(35, 32, 39);
            label1.BackColor = Color.FromArgb(35, 32, 39);

            HideAllLabel();
            changeLengthLabel.Text = "Change edge";
            label1.Location = new Point(51, 451);
            drawEdgeLabel.Location = new Point(36, 112);
            drawVertexLabel.Location = new Point(33, 222);
            deleteElementLabel.Location = new Point(20, 343);
            changeLengthLabel.Location = new Point(26, 431);

            startReseachButton.Location = new Point(150, Consts.GraphPictureBoxHeight + 5);
            startReseachButton.Height = 75;
            startReseachButton.BackColor = Color.FromArgb(35, 32, 39);
            timer3.Start();
            Theme = MetroThemeStyle.Dark;
            field.Location = new Point(Consts.FieldInitialPositionX, Consts.FieldInitialPositionY + 5);

            panel3.BackColor = Color.FromArgb(11, 17, 20);
            panelChart.BackColor = Color.FromArgb(35, 32, 39);

            StopProcessButton.BackColor = Color.FromArgb(35, 32, 39);
            testingProgrammButton.BackColor = Color.FromArgb(35, 32, 39);
            saveChartButton.BackColor = Color.FromArgb(35, 32, 39);
            openChartButton.BackColor = Color.FromArgb(35, 32, 39);
            drawingPanel.BackColor = Color.FromArgb(11, 17, 20);
            drawingSubPanel.BackColor = Color.FromArgb(35, 32, 39);
            changeParametersSubPanel.BackColor = Color.FromArgb(35, 32, 39);

            panel1.BackColor = Color.FromArgb(11, 17, 20);
            toolsSubPanel.BackColor = Color.FromArgb(35, 32, 39);
            panel2.BackColor = Color.FromArgb(35, 32, 39);
            drawingPanel.Location = new Point(0, 5);
            panel1.Location = new Point(Consts.GraphPictureBoxWidth + 150, 5);

            Width = 1170;
            Height = 665;
            field.Width = Consts.GraphPictureBoxWidth;
            field.Height = Consts.GraphPictureBoxHeight;

            Activate();
        }

        /// <summary>
        /// Спрятать подменю.
        /// </summary>
        private void HideSubMenu()
        {
            drawingSubPanel.Visible = false;
            changeParametersSubPanel.Visible = false;
        }

        /// <summary>
        /// Показать все подменю
        /// </summary>
        private void ShowAllSubMenu()
        {
            panel2.Visible = true;
            toolsSubPanel.Visible = true;
            changeParametersSubPanel.Visible = true;
            drawingSubPanel.Visible = true;
        }

        private static void ShowSubMenu(Panel subMenu)
        {
            subMenu.Visible = !subMenu.Visible;
        }

        private void DrawingButtonClick(object sender, EventArgs e)
        {
            ShowSubMenu(drawingSubPanel);
        }

        private void ChangeParametersButtonClick(object sender, EventArgs e)
        {
            ShowSubMenu(changeParametersSubPanel);
        }

        private void ToolsButtonClick(object sender, EventArgs e)
        {
            ShowSubMenu(toolsSubPanel);
        }

        private void GraphInfoButtonClick(object sender, EventArgs e)
        {
            ShowSubMenu(panel2);
        }

        /// <summary>
        /// Вывод информации о графе.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphInfoTimerTick(object sender, EventArgs e)
        {
            changeLengthLabel.Visible = !ChangeEdgeLengthButton.Enabled;
            drawEdgeLabel.Visible = !DrawEdgeButton.Enabled;
            drawVertexLabel.Visible = !DrawVertexButton.Enabled;
            deleteElementLabel.Visible = !DeleteElementButton.Enabled;
            label1.Visible = !ChangeEdgeLengthButton.Enabled;

            var graphForCheck = new SpecialKindOfGraphForCheckStronglyDirection(Vertices.Count);

            foreach (var edge in Edges)
                graphForCheck.AddEdge(edge.Ver1, edge.Ver2);

            CyclomaticNumber = Edges.Count - Vertices.Count + graphForCheck.GetConnectedComponentsAmount();
            button8.Text = $@"Cyclomatic number: {CyclomaticNumber}";
            button7.Text = $@"Vertex: {Vertices.Count}";
            button6.Text = $@"Edges: {Edges.Count}";
        }

        private void FirstExitButtonClick(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception)
            {
                Environment.Exit(1);
            }
        }

        private void ToolsForChartButtonClick(object sender, EventArgs e)
        {
            ShowSubMenu(panelChart);
        }

        private void SecondExitButtonClick(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception)
            {
                Environment.Exit(1);
            }
        }
    }
}