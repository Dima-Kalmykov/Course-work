using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework;
using Timer = System.Windows.Forms.Timer;

namespace WindowsFormsApp1
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        // Диалоговые формы.
        private readonly ChooseEdgeForm chooseEdgeFormForm = new ChooseEdgeForm();
        private readonly GetLengthEdgeForm getEdgeLengthForm = new GetLengthEdgeForm();
        private readonly GetRandomGraphForm getRandomGraphForm = new GetRandomGraphForm();

        private readonly CheckGraphForStronglyConnectionForm checkGraphForStronglyConnectionForm =
            new CheckGraphForStronglyConnectionForm();

        private readonly ShowAdjacencyMatrixForm showAdjacencyMatrixForm = new ShowAdjacencyMatrixForm();
        private readonly Chart chartForm = new Chart();
        private readonly Form1 chartDisplay = new Form1();

        private readonly MyMessageBox myMessageBox = new MyMessageBox();

        // Инструменты для рисования.
        public ToolsForDrawingGraph ToolsForDrawing;

        // Списки смежности.
        public List<Vertex> Vertex;
        public List<Edge> Edges;

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

        internal MainForm()
        {
            // Устанавливаем необходимые параметры.
            InitializeComponent();
            Theme = MetroThemeStyle.Light;
            Style = MetroColorStyle.Green;
            ToolsForDrawing = new ToolsForDrawingGraph(Consts.GraphPictureBoxWidth, Consts.GraphPictureBoxHeight);
            Vertex = new List<Vertex>();
            Edges = new List<Edge>();

            chooseEdgeFormForm.Owner = this;
            getEdgeLengthForm.Owner = this;
            getRandomGraphForm.Owner = this;
            checkGraphForStronglyConnectionForm.Owner = this;

            ToolsForDrawing.ClearField();
            field.Image = ToolsForDrawing.GetBitmap();

            CustomizeDesign();
        }

        private void HideAdjacencyMatrix()
        {
            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (Vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Close matrix";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
        }

        private void RedrawSelectedVertex()
        {
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                ToolsForDrawing.DrawVertex(Vertex[indexVertexForMove].X,
                    Vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());

                firstPress = true;
                field.Image = ToolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                ToolsForDrawing.DrawVertex(Vertex[ver1ForConnection].X,
                    Vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = ToolsForDrawing.GetBitmap();
            }
        }

        /// <summary>
        /// Нажатие на кнопку "Рисование вершины".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawVertexButton_Click(object sender, EventArgs e)
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


        public void DeleteGraph()
        {
            Vertex.Clear();
            Edges.Clear();
            showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Clear();
            ToolsForDrawing.ClearField();
            field.Image = ToolsForDrawing.GetBitmap();
        }

        public DialogResult GetConfirmCancellation()
        {
            DrawVertexButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            var message = "Are you sure you want to delete the graph?";
            var caption = "Confirmation";

            // Диалоговое окно для подтверждения удаления.

            var confirmCancellation = myMessageBox.ShowDeleteAllGraph(message, caption);

            return confirmCancellation;
        }

        /// <summary>
        /// Нажатие на кнопку "Удаление всего графа".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAllGraphButton_Click(object sender, EventArgs e)
        {
            RedrawSelectedVertex();

            // Проверяем, есть ли какой-то элемент для удаления.
            if (Vertex.Count != 0)
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
                myMessageBox.ShowGraphIsEmpty("Graph is empty");
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
        private void DrawEdgeButton_Click(object sender, EventArgs e)
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
        private void CheckGraphForStrongConnectionButton_Click(object sender, EventArgs e)
        {
            if (!clickContinue)
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
                    myMessageBox.ShowGraphIsEmpty("Graph is empty");
                }
                else
                {
                    // Представляем граф в удобном для проверки виде.
                    SpecialKindOfGraphForCheckStronglyDirection graphForCheck = new SpecialKindOfGraphForCheckStronglyDirection(Vertex.Count);

                    foreach (var edge in Edges)
                        graphForCheck.AddEdge(edge.Ver1, edge.Ver2);

                    // Проверяем граф на сильносвязность.
                    // В случае, если граф не сильносвязный, то
                    // предлагаем сгенерировать случайный сильносвязный граф,
                    // или продолжить редактирование
                    if (graphForCheck.IsStronglyConnection())
                    {
                        myMessageBox.ShowGraphIsStronglyDirection("Graph is strongly directed");
                    }
                    else
                    {
                        //checkGraphForStronglyConnectionForm.Caption.Text =
                        //    "Граф не является сильносвязным";

                        var res = checkGraphForStronglyConnectionForm.MyShow();

                        checkGraphForStronglyConnectionForm.MustBeGenerated = res.Item2;

                        if (checkGraphForStronglyConnectionForm.MustBeGenerated)
                            GetRandomGraphButton.PerformClick();
                    }
                }

                HideAdjacencyMatrix();
            }
            else
            {
                // Если нечего проверять, выводим сообщение.
                if (adjMatrix is null || adjMatrix.Length == 0)
                    myMessageBox.ShowGraphIsEmpty("Graph is empty");
                else
                {
                    // Представляем граф в удобном для проверки виде.
                    var graphForCheck = new SpecialKindOfGraphForCheckStronglyDirection(Vertex.Count);

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
                        var res = checkGraphForStronglyConnectionForm.MyShow();

                        checkGraphForStronglyConnectionForm.MustBeGenerated = res.Item2;

                        if (checkGraphForStronglyConnectionForm.MustBeGenerated)
                            GetRandomGraphButton.PerformClick();
                    }
                }
            }
        }

        private bool responseSC;

        /// <summary>
        /// Нажатие на кнопку "Сгенерировать граф".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetRandomGraphButton_Click(object sender, EventArgs e)
        {
            RedrawSelectedVertex();

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;
            DeleteElementButton.Enabled = true;

            //getRandomGraphForm.ShowDialog();

            var res = getRandomGraphForm.MyShow();
            getRandomGraphForm.cancelGraph = res.Item2;
            getRandomGraphForm.amount = res.Item3;

            // Если нажали отмену, то ничего не происходит.
            if (!getRandomGraphForm.cancelGraph)
            {
                // Количество вершин.
                int size = getRandomGraphForm.amount;

                ToolsForDrawing.ClearField();

                // Генерируем матрицу смежности.
                //adjMatrix = graph.GetRandomAdjMatrix(size);
                adjMatrix = ToolsForDrawing.GetRandomAdjacencyMatrix(size);

                // Устанавливаем вес рёбер.
                adjMatrix = ToolsForDrawing.SetDistanceEdge(adjMatrix);

                // Генерируем координаты вершин.
                Vertex = ToolsForDrawing.GetRandomVertex(adjMatrix,
                    field.Size.Width - 2 * Consts.VertexRadius,
                    field.Size.Height - 2 * Consts.VertexRadius);

                // Заполняем лист рёбер.
                Edges = ToolsForDrawing.GetRandomEdges(adjMatrix);

                // Отрисовываем полный граф.
                ToolsForDrawing.DrawFullGraph(Edges, Vertex);

                // Выводим матрицу смежности.
                PrintAdjMatrix();

                field.Image = ToolsForDrawing.GetBitmap();
            }

            HideAdjacencyMatrix();
        }

        /// <summary>
        /// Нажатие на кнопку "удалить элемент".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteElementButton_Click(object sender, EventArgs e)
        {
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                ToolsForDrawing.DrawVertex(Vertex[indexVertexForMove].X,
                    Vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = ToolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                ToolsForDrawing.DrawVertex(Vertex[ver1ForConnection].X,
                    Vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = ToolsForDrawing.GetBitmap();
            }

            // Делаем эту кнопку неактивной.
            DeleteElementButton.Enabled = false;

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (Vertex.Count != 0)
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
        private void SaveGraphButton_Click(object sender, EventArgs e)
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
                saveGraphDialog.Title = "Сохранить картинку как...";
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
                myMessageBox.ShowGraphIsEmpty("Graph is empty");

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (Vertex.Count != 0)
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
        private void OpenGraphButton_Click(object sender, EventArgs e)
        {
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                ToolsForDrawing.DrawVertex(Vertex[indexVertexForMove].X,
                    Vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = ToolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                ToolsForDrawing.DrawVertex(Vertex[ver1ForConnection].X,
                    Vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = ToolsForDrawing.GetBitmap();
            }

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;
            DeleteElementButton.Enabled = true;

            OpenFileDialog openGraphDialog = new OpenFileDialog();

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

                    PrintAdjMatrix();

                    ToolsForDrawing.ClearField();
                    ToolsForDrawing.DrawFullGraph(Edges, Vertex);
                    field.Image = ToolsForDrawing.GetBitmap();
                }
                catch (Exception)
                {
                    myMessageBox.ShowError("Unable to open image");

                }
            }

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (Vertex.Count != 0)
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
        private void ChangeLengthButton_Click(object sender, EventArgs e)
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
        private void StopDrawingButton_Click(object sender, EventArgs e)
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
        private void ShowOrHideAdjMatrix_Click(object sender, EventArgs e)
        {
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                ToolsForDrawing.DrawVertex(Vertex[indexVertexForMove].X,
                    Vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = ToolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                ToolsForDrawing.DrawVertex(Vertex[ver1ForConnection].X,
                    Vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

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
        private (string, string, string) SetTextInButtonFromChooseEdgeForm(int ver1, int ver2)
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
        private void field_MouseClick(object sender, MouseEventArgs e)
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
                        for (int i = 0; i < Vertex.Count; i++)
                        {
                            if (Math.Pow(Vertex[i].X - e.X, 2) + Math.Pow(Vertex[i].Y - e.Y, 2) <=
                                Math.Pow(Consts.VertexRadius, 2))
                            {
                                ToolsForDrawing.DrawSelectedVertex(Vertex[i].X, Vertex[i].Y);
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
                            if (Vertex.Where((v, i) => i != indexVertexForMove)
                                      .Any(t => Math.Abs(e.X - t.X) < 2 * Consts.VertexRadius &&
                                                Math.Abs(e.Y - t.Y) < 2 * Consts.VertexRadius))
                            {
                                RedrawSelectedVertex();
                                myMessageBox.ShowVertexNotCross("Vertex must not cross");
                                firstPress = true;
                                return;
                            }

                            // Тогда двигаем точку и перерисовываем граф.
                            Vertex[indexVertexForMove].X = e.X;
                            Vertex[indexVertexForMove].Y = e.Y;

                            ToolsForDrawing.ClearField();

                            indexVertexForMove = -1;

                            ToolsForDrawing.DrawFullGraph(Edges, Vertex);

                            field.Image = ToolsForDrawing.GetBitmap();
                        }
                    }
                }

                // Если нажата кнопка "Изменить длину ребра".
                if (!ChangeEdgeLengthButton.Enabled)
                {
                    // Если мы нажали на вершину, то ничего не происходит.
                    if (Vertex.Any(t => Math.Pow(t.X - e.X, 2) + Math.Pow(t.Y - e.Y, 2) <=
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

                            if ((Math.Pow(Vertex[Edges[i].Ver1].X - Consts.VertexRadius - e.X, 2) +
                                 Math.Pow(Vertex[Edges[i].Ver1].Y - Consts.VertexRadius - e.Y, 2)) <=
                                Math.Pow(Consts.VertexRadius + 4, 2)

                                &&

                                (Math.Pow(Vertex[Edges[i].Ver1].X - Consts.VertexRadius - e.X, 2) +
                                 Math.Pow(Vertex[Edges[i].Ver1].Y - Consts.VertexRadius - e.Y, 2)) >=
                                Math.Pow(Consts.VertexRadius - 4, 2))
                            {
                                //getEdgeLengthForm.ShowDialog();
                                var result = getEdgeLengthForm.MyShow();

                                getEdgeLengthForm.WasCancel = result.Item2;
                                getEdgeLengthForm.Weight = result.Item3;

                                // Если мы не нажади отмену, то меняем вес.
                                if (!getEdgeLengthForm.WasCancel)
                                {
                                    Edges[i].Weight = getEdgeLengthForm.Weight;
                                    PrintAdjMatrix();
                                }
                            }
                        }
                        else
                        {
                            // Если x координаты двух вершин ребра не совпадают.
                            if (Vertex[Edges[i].Ver1].X != Vertex[Edges[i].Ver2].X)
                            {
                                // Вычисляем коэффициенты в уравнении прямой,
                                // проходящей через две вершины этого ребра, вида y = kx + b.
                                double k = MyMath.GetK(
                                    Vertex[Edges[i].Ver1], Vertex[Edges[i].Ver2]);

                                double b = MyMath.GetB(
                                    Vertex[Edges[i].Ver1], Vertex[Edges[i].Ver2]);

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

                                        var res = chooseEdgeFormForm.MyShowChangeLength(
                                            "Choose the edge whose weight\n          you want to change",
                                            $"Change edge weight from {Edges[i].Ver1 + 1} to {Edges[i].Ver2 + 1}",
                                            $"Change edge weight from {Edges[i].Ver2 + 1} to {Edges[i].Ver1 + 1}"
                                            );

                                        chooseEdgeFormForm.WasCancel = res.Item2;
                                        chooseEdgeFormForm.IsFirstAction = res.Item3;
                                        // Если не отменили изменение длины, то меняем.
                                        if (!chooseEdgeFormForm.WasCancel)
                                        {
                                            if (chooseEdgeFormForm.IsFirstAction)
                                            {
                                                //getEdgeLengthForm.ShowDialog();

                                                var result1 = getEdgeLengthForm.MyShow();

                                                getEdgeLengthForm.WasCancel = result1.Item2;
                                                getEdgeLengthForm.Weight = result1.Item3;

                                                if (!getEdgeLengthForm.WasCancel)
                                                {
                                                    Edges[i].Weight = getEdgeLengthForm.Weight;

                                                    PrintAdjMatrix();

                                                    return;
                                                }

                                                return;
                                            }

                                            //getEdgeLengthForm.ShowDialog();
                                            var result = getEdgeLengthForm.MyShow();

                                            getEdgeLengthForm.WasCancel = result.Item2;
                                            getEdgeLengthForm.Weight = result.Item3;
                                            if (!getEdgeLengthForm.WasCancel)
                                            {
                                                for (int j = i; j < Edges.Count; j++)
                                                {
                                                    if (Edges[j].Ver1 == Edges[i].Ver2 &&
                                                        Edges[j].Ver2 == Edges[i].Ver1)
                                                    {
                                                        Edges[j].Weight = getEdgeLengthForm.Weight;

                                                        PrintAdjMatrix();
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

                                        getEdgeLengthForm.WasCancel = result.Item2;
                                        getEdgeLengthForm.Weight = result.Item3;
                                        if (!getEdgeLengthForm.WasCancel)
                                        {
                                            Edges[i].Weight = getEdgeLengthForm.Weight;

                                            PrintAdjMatrix();

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
                                if (e.X <= Vertex[Edges[i].Ver1].X + 4 &&
                                    e.X >= Vertex[Edges[i].Ver1].X - 4 &&
                                    e.Y < Math.Max(
                                        Vertex[Edges[i].Ver1].Y,
                                        Vertex[Edges[i].Ver2].Y) - Consts.VertexRadius &&
                                    e.Y > Math.Min(
                                        Vertex[Edges[i].Ver1].Y,
                                        Vertex[Edges[i].Ver2].Y) + Consts.VertexRadius)
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

                                        var res = chooseEdgeFormForm.MyShowChangeLength(
                                            "Choose the edge whose weight\n          you want to change",
                                            $"Change edge weight from {Edges[i].Ver1 + 1} to {Edges[i].Ver2 + 1}",
                                            $"Change edge weight from {Edges[i].Ver2 + 1} to {Edges[i].Ver1 + 1}"
                                        );

                                        chooseEdgeFormForm.WasCancel = res.Item2;
                                        chooseEdgeFormForm.IsFirstAction = res.Item3;

                                        // Если не отменили, то меняем длину. 
                                        if (!chooseEdgeFormForm.WasCancel)
                                        {
                                            if (chooseEdgeFormForm.IsFirstAction)
                                            {
                                                var result2 = getEdgeLengthForm.MyShow();

                                                getEdgeLengthForm.WasCancel = result2.Item2;
                                                getEdgeLengthForm.Weight = result2.Item3;

                                                if (!getEdgeLengthForm.WasCancel)
                                                {
                                                    Edges[i].Weight = getEdgeLengthForm.Weight;

                                                    PrintAdjMatrix();

                                                    return;
                                                }

                                                return;
                                            }

                                            var result = getEdgeLengthForm.MyShow();

                                            getEdgeLengthForm.WasCancel = result.Item2;
                                            getEdgeLengthForm.Weight = result.Item3;

                                            if (!getEdgeLengthForm.WasCancel)
                                            {
                                                for (int j = i; j < Edges.Count; j++)
                                                {
                                                    if (Edges[j].Ver1 == Edges[i].Ver2 &&
                                                        Edges[j].Ver2 == Edges[i].Ver1)
                                                    {
                                                        Edges[j].Weight = getEdgeLengthForm.Weight;

                                                        PrintAdjMatrix();
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

                                        getEdgeLengthForm.WasCancel = result.Item2;
                                        getEdgeLengthForm.Weight = result.Item3;

                                        if (!getEdgeLengthForm.WasCancel)
                                        {
                                            Edges[i].Weight = getEdgeLengthForm.Weight;

                                            PrintAdjMatrix();

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
                    if (Vertex.Any(t => Math.Abs(e.X - t.X) < 2 * Consts.VertexRadius &&
                                        Math.Abs(e.Y - t.Y) < 2 * Consts.VertexRadius))
                    {
                        myMessageBox.ShowVertexNotCross("Vertex must not cross");
                        return;
                    }

                    var newVertex = new Vertex(e.X, e.Y);

                    if (newVertex.X >= field.Size.Width - 2 * Consts.VertexRadius ||
                        newVertex.X <= 2 * Consts.VertexRadius ||
                        newVertex.Y <= 2 * Consts.VertexRadius ||
                        newVertex.Y >= field.Size.Height - 2 * Consts.VertexRadius)
                    {
                        myMessageBox.ShowInvalidPlace();
                        return;
                    }

                    Vertex.Add(new Vertex(e.X, e.Y));
                    ToolsForDrawing.DrawVertex(e.X, e.Y, Vertex.Count.ToString());
                    field.Image = ToolsForDrawing.GetBitmap();

                    PrintAdjMatrix();
                }

                //Если нажата кнопка "Рисовать ребро".
                if (!DrawEdgeButton.Enabled)
                {
                    // Смотрим, на какую вершину нажали, затем выделяем её.
                    for (int i = 0; i < Vertex.Count; i++)
                    {
                        if (Math.Pow(Vertex[i].X - e.X, 2) + Math.Pow(Vertex[i].Y - e.Y, 2) <=
                            Math.Pow(Consts.VertexRadius, 2))
                        {
                            // Если это первая вершина.
                            if (ver1ForConnection == -1)
                            {
                                ToolsForDrawing.DrawSelectedVertex(Vertex[i].X, Vertex[i].Y);
                                ver1ForConnection = i;
                                field.Image = ToolsForDrawing.GetBitmap();
                                break;
                            }

                            // Если это вторая веришна.
                            if (ver2ForConnection == -1)
                            {
                                ToolsForDrawing.DrawSelectedVertex(Vertex[i].X, Vertex[i].Y);
                                ver2ForConnection = i;
                                field.Image = ToolsForDrawing.GetBitmap();

                                // Если между ними уже есть ребро, то предупреждаем об этом.
                                if (adjMatrix[ver1ForConnection, ver2ForConnection] == 0)
                                {
                                    var result = getEdgeLengthForm.MyShow();

                                    getEdgeLengthForm.WasCancel = result.Item2;
                                    getEdgeLengthForm.Weight = result.Item3;

                                    // Если решили отменить рисование ребра.
                                    if (getEdgeLengthForm.WasCancel)
                                    {
                                        ToolsForDrawing.DrawVertex(
                                            Vertex[ver1ForConnection].X,
                                            Vertex[ver1ForConnection].Y,
                                            (ver1ForConnection + 1).ToString());

                                        ToolsForDrawing.DrawVertex(
                                            Vertex[ver2ForConnection].X,
                                            Vertex[ver2ForConnection].Y,
                                            (ver2ForConnection + 1).ToString());

                                        getEdgeLengthForm.WasCancel = false;

                                        ver1ForConnection = ver2ForConnection = -1;
                                    }
                                    else
                                    {
                                        Edges.Add(new Edge(
                                            ver1ForConnection,
                                            ver2ForConnection,
                                            getEdgeLengthForm.Weight));

                                        ToolsForDrawing.DrawEdge(
                                            Vertex[ver1ForConnection],
                                            Vertex[ver2ForConnection],
                                            Edges[Edges.Count - 1]);

                                        ver1ForConnection = -1;
                                        ver2ForConnection = -1;

                                        PrintAdjMatrix();

                                        DrawVertexButton.Enabled = true;
                                    }

                                    field.Image = ToolsForDrawing.GetBitmap();

                                    break;
                                }

                                myMessageBox.ShowEdgeExists("Edge from vertex" +
                                                            $" {ver1ForConnection + 1} to vertex " +
                                                            $"{ver2ForConnection + 1} \n" +
                                                            GetSpaces(15) + "already exists",
                                    ver1ForConnection + 1, ver2ForConnection + 1);

                                // Убираем выделение с вершин.
                                ToolsForDrawing.DrawVertex(
                                    Vertex[ver1ForConnection].X,
                                    Vertex[ver1ForConnection].Y,
                                    (ver1ForConnection + 1).ToString());

                                ToolsForDrawing.DrawVertex(
                                    Vertex[ver2ForConnection].X,
                                    Vertex[ver2ForConnection].Y,
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
                    for (int i = 0; i < Vertex.Count; i++)
                    {
                        if (Math.Pow(Vertex[i].X - e.X, 2) + Math.Pow(Vertex[i].Y - e.Y, 2) <=
                            Math.Pow(Consts.VertexRadius, 2))
                        {
                            var confirmation = myMessageBox.ShowDeleteElement("The result of this action is permanent\n" +
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

                                Vertex.RemoveAt(i);
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
                                if ((Math.Pow(Vertex[Edges[i].Ver1].X - Consts.VertexRadius - e.X, 2)
                                     +
                                     Math.Pow(Vertex[Edges[i].Ver1].Y - Consts.VertexRadius - e.Y, 2))
                                    <=
                                    Math.Pow(Consts.VertexRadius + 4, 2)

                                    &&

                                    (Math.Pow(Vertex[Edges[i].Ver1].X - Consts.VertexRadius - e.X, 2)
                                     +
                                     Math.Pow(Vertex[Edges[i].Ver1].Y - Consts.VertexRadius - e.Y, 2))
                                    >=
                                    Math.Pow(Consts.VertexRadius - 4, 2))
                                {
                                    var confirm = myMessageBox.ShowDeleteElement("The result of this action is permanent\n" +
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
                                if (Vertex[Edges[i].Ver1].X != Vertex[Edges[i].Ver2].X)
                                {
                                    // Вычисляем коэффициенты в уравнении прямой,
                                    // проходящей через две вершины этого ребра, вида y = kx + b.
                                    double k = MyMath.GetK(
                                        Vertex[Edges[i].Ver1], Vertex[Edges[i].Ver2]);

                                    double b = MyMath.GetB(
                                        Vertex[Edges[i].Ver1], Vertex[Edges[i].Ver2]);

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
                                            var (caption, text1, text2) = SetTextInButtonFromChooseEdgeForm(
                                                 Edges[i].Ver1, Edges[i].Ver2);

                                            //chooseEdgeFormForm.ShowDialog();
                                            var res = chooseEdgeFormForm.MyShow(caption, text1, text2);
                                            chooseEdgeFormForm.WasCancel = res.Item2;
                                            chooseEdgeFormForm.IsFirstAction = res.Item3;
                                            // Если не нажали отмену, то удаляем.
                                            if (!chooseEdgeFormForm.WasCancel)
                                            {
                                                if (chooseEdgeFormForm.IsFirstAction)
                                                {
                                                    Edges.RemoveAt(i);
                                                    somethingDeleted = true;

                                                    chooseEdgeFormForm.IsFirstAction = false;
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

                                        var confirm = myMessageBox.ShowDeleteElement("The result of this action is permanent\n" +
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
                                if (Vertex[Edges[i].Ver1].X == Vertex[Edges[i].Ver2].X)
                                {
                                    // Если x координаты вершин ребра совпали, 
                                    // то проводим между ними прямую, и 
                                    // "расширяем" её вверх и вниз на ширину кисти/2.
                                    if (e.X <= Vertex[Edges[i].Ver1].X + 4 &&
                                        e.X >= Vertex[Edges[i].Ver1].X - 4 &&
                                        e.Y < Math.Max(
                                            Vertex[Edges[i].Ver1].Y,
                                            Vertex[Edges[i].Ver2].Y) - Consts.VertexRadius &&
                                        e.Y > Math.Min(
                                            Vertex[Edges[i].Ver1].Y,
                                            Vertex[Edges[i].Ver2].Y) + Consts.VertexRadius)
                                    {
                                        // Если есть обратный путь, то предлагаем выбрать путь,
                                        // который надо удалить. 
                                        if (adjMatrix[Edges[i].Ver1, Edges[i].Ver2] != 0 &&
                                            adjMatrix[Edges[i].Ver2, Edges[i].Ver1] != 0)
                                        {
                                            var (caption, text1, text2) = SetTextInButtonFromChooseEdgeForm(
                                                Edges[i].Ver1, Edges[i].Ver2);

                                            var res = chooseEdgeFormForm.MyShow(caption, text1, text2);
                                            chooseEdgeFormForm.WasCancel = res.Item2;
                                            chooseEdgeFormForm.IsFirstAction = res.Item3;

                                            // Если не нажали отмену, то удаляем.
                                            if (!chooseEdgeFormForm.WasCancel)
                                            {
                                                if (chooseEdgeFormForm.IsFirstAction)
                                                {
                                                    Edges.RemoveAt(i);
                                                    somethingDeleted = true;
                                                    chooseEdgeFormForm.IsFirstAction = false;
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

                                        var confirm = myMessageBox.ShowDeleteElement("The result of this action is permanent\n" +
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
                        ToolsForDrawing.DrawFullGraph(Edges, Vertex);
                        field.Image = ToolsForDrawing.GetBitmap();
                        PrintAdjMatrix();
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
            File.AppendAllText(pathForSaveFile, Vertex.Count + Environment.NewLine);

            for (int i = 0; i < Vertex.Count; i++)
                File.AppendAllText(pathForSaveFile, Vertex[i] + Environment.NewLine);

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
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Отмена рисования ребра (нажатие ESC).
            if (ver1ForConnection != -1 && ver2ForConnection == -1)
                if (e.KeyChar == (char)Keys.Escape)
                {
                    ToolsForDrawing.DrawVertex(
                        Vertex[ver1ForConnection].X,
                        Vertex[ver1ForConnection].Y,
                        (ver1ForConnection + 1).ToString());
                    ver1ForConnection = -1;
                }

            // Отмена перетаскивания вершины (нажатие ESC).
            if (!firstPress)
            {
                if (e.KeyChar == (char)Keys.Escape)
                {
                    ToolsForDrawing.DrawVertex(
                        Vertex[indexVertexForMove].X,
                        Vertex[indexVertexForMove].Y,
                        (indexVertexForMove + 1).ToString());
                    indexVertexForMove = -1;

                    firstPress = true;
                }
            }

            field.Image = ToolsForDrawing.GetBitmap();
        }

        // Methods for work with files.

        /// <summary>
        /// Устанавливаем необходимые данные из файла.
        /// </summary>
        private void SetDataFromFile()
        {
            int vertexCount;
            int edgesCount;

            Vertex.Clear();
            Edges.Clear();

            string[] lines = File.ReadAllLines(pathForOpenFile);

            // Количество вершин.
            vertexCount = int.Parse(lines[0]);

            // Заполняем список вершин.
            for (int i = 1; i < vertexCount + 1; i++)
            {
                int xCoord = int.Parse(lines[i].Split(' ')[0]);
                int yCoord = int.Parse(lines[i].Split(' ')[1]);

                Vertex.Add(new Vertex(xCoord, yCoord));
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
        private void MainForm_Click(object sender, EventArgs e) =>
            Activate();

        /// <summary>
        /// Вывод матрицы смежности в ListBox.
        /// </summary>
        public void PrintAdjMatrix()
        {
            // Создаём и заполняем матрицу по листам смежности.
            adjMatrix = new double[Vertex.Count, Vertex.Count];

            ToolsForDrawing.FillAdjMatrix(Vertex.Count, Edges, adjMatrix);

            showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Clear();

            string curStrForPrint;
            string caption;

            string strFromHyphen = GetHyphen(5) + "|";

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
            for (int i = 0; i < Vertex.Count; i++)
            {
                // Отдельно высчитываем расстояние между столбцами для двузначных чисел.
                if (i < 9)
                    curStrForPrint = (i + 1) + GetSpaces(3) + "|" + GetSpaces(3);
                else
                    curStrForPrint = (i + 1) + GetSpaces(1) + "|" + GetSpaces(3);

                for (int j = 0; j < Vertex.Count; j++)
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

                                    strFromHyphen += GetHyphen(7);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(3);

                                    strFromHyphen += GetHyphen(10);
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

                                    strFromHyphen += GetHyphen(9);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(5);

                                    strFromHyphen += GetHyphen(12);
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

                                    strFromHyphen += GetHyphen(11);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(7);

                                    strFromHyphen += GetHyphen(13);
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

                                    strFromHyphen += GetHyphen(13);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(8);

                                    strFromHyphen += GetHyphen(15);
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

                                    strFromHyphen += GetHyphen(14);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(9);

                                    strFromHyphen += GetHyphen(16);
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

                                    strFromHyphen += GetHyphen(15);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(10);

                                    strFromHyphen += GetHyphen(18);
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

                                    strFromHyphen += GetHyphen(17);
                                }
                                else
                                {
                                    caption += (j + 1) + GetSpaces(12);

                                    strFromHyphen += GetHyphen(19);
                                }
                            }

                            break;
                    }
                }

                firstRaw = false;

                // Добавляем сформированную строку в ListBox/
                showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Add(curStrForPrint);

                // Делаем пропуск строки, если это не последняя строка.
                if (i != Vertex.Count - 1)
                    showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Add(GetSpaces(5) + "|");

            }

            // Вставляем первую и вторую строки в ListBox.
            showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Insert(0, caption);
            showAdjacencyMatrixForm.AdjacencyMatrixListBox.Items.Insert(1, strFromHyphen);

            // Если вершин нет, закрываем матрицу.
            // Если есть, то выводим матрицу, при этом оставляем фокус на этом окне.
            if (Vertex.Count == 0)
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

        // Methods for print adjacency matrix.

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
        private static string GetHyphen(int amount)
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


        // Обработать случай, когда точки на одной прямой.
        // Когда возвращается в вершину, vertex выходит из диапазона.
        private double changeStep;
        private double step;

        private int totalCount = 0;
        private bool firstVertex = true;

        public float coefficient = 1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            chartForm.chartForm.Series["Amount of points"]
                .Points.AddXY((int)(sp.ElapsedMilliseconds) / 1000, totalCount);
        }


        public void MainTick(List<Edge>[] listArr)
        {
            coefficient = (float)(trackBar1.Value / 10.0);
            // Если готова выпустить точку.
            var count = points.Count;

            for (var i = 0; i < Vertex.Count; i++)
            {
                if (Vertex[i].HasPoint)
                {
                    Vertex[i].HasPoint = false;

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

            ToolsForDrawing.DrawFullGraph(Edges, Vertex);

            for (var i = 0; i < points.Count; i++)
            {
                if (timers[i].ElapsedMilliseconds * coefficient > points[i].Weight * 1000)
                {
                    Vertex[points[i].Ver2].HasPoint = true;
                    points.RemoveAt(i);
                    timers.RemoveAt(i);
                    i--;
                    continue;
                }

                PointF point = GetPoint(Vertex[points[i].Ver1],
                    Vertex[points[i].Ver2], points[i].Weight, timers[i]);

                ToolsForDrawing.DrawPoint(point.X, point.Y);

                field.Image = ToolsForDrawing.GetBitmap();
            }

            field.Image = ToolsForDrawing.GetBitmap();
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

        private readonly List<Stopwatch> timers = new List<Stopwatch>();
        private readonly List<Edge> points = new List<Edge>();

        private bool clickContinue;
        private Timer mainTimer = new Timer();
        Stopwatch sp = new Stopwatch();

        private void button1_Click(object sender, EventArgs e)
        {

            RedrawSelectedVertex();
            HideAdjacencyMatrix();

            chartForm.chartForm.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chartForm.chartForm.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chartForm.chartForm.ChartAreas[0].AxisX.Minimum = 0;
            chartForm.chartForm.ChartAreas[0].AxisX.Title = "Time";

            clickContinue = true;
            CheckGraphForStrongConnectionButton.PerformClick();

            if (responseSC)
            {
                timer3.Stop();
                HideAllLabel();
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

                Vertex[0].HasPoint = true;
                var listArr = new List<Edge>[Vertex.Count];

                for (var i = 0; i < Vertex.Count; i++)
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
                sp.Start();
                chartForm.Show();
                Activate();
            }
            else
            {
                clickContinue = false;
            }
        }

        /// <summary>
        /// Кнопка STOP/Continue.
        /// </summary>
        /// <param name="sender"> Sender </param>
        /// <param name="e"> E </param>
        private void StopProcessButton_Click(object sender, EventArgs e)
        {
            requireTime = sp.ElapsedMilliseconds;

            if (StopProcessButton.Text == "Stop")
            {
                mainTimer.Stop();
                timer1.Stop();
                timer2.Stop();
                sp.Stop();
                timers.ForEach(timer => timer.Stop());
                StopProcessButton.Text = "Continue";
            }
            else
            {
                sp.Start();
                mainTimer.Start();
                timer1.Start();
                timer2.Start();
                timers.ForEach(timer => timer.Start());
                StopProcessButton.Text = "Stop";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RedrawSelectedVertex();

            mainTimer.Stop();
            timer1.Stop();
            timer2.Stop();
            sp.Stop();
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
                saveGraphDialog.Title = "Сохранить график как...";
                saveGraphDialog.OverwritePrompt = true;
                saveGraphDialog.CheckPathExists = true;
                saveGraphDialog.Filter = "Files(*.CSV)|*.CSV";

                requireTime = sp.ElapsedMilliseconds;
                saveGraphDialog.ShowHelp = true;

                // Процесс сохранения файла.
                if (saveGraphDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string sv = saveGraphDialog.FileName;
                        File.WriteAllText(sv, chartData.ToString());
                    }
                    catch (Exception)
                    {
                        myMessageBox.ShowError("Unable to save chart");
                    }
                }
            }
            else
                myMessageBox.ShowGraphIsEmpty("Graph is empty");

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (Vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Close matrix";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
        }

        private static StringBuilder chartData = new StringBuilder("Time;Amount\n");
        private int seconds;

        private void timer2_Tick(object sender, EventArgs e)
        {
            var time = TimeSpan.FromSeconds(seconds++);

            var timeStr = time.ToString(@"hh\:mm\:ss");
            chartData.AppendLine($"{timeStr};{totalCount}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chartDisplay.chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chartDisplay.chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chartDisplay.chart1.ChartAreas[0].AxisX.Minimum = 0;

            RedrawSelectedVertex();

            mainTimer.Stop();
            timer1.Stop();
            timer2.Stop();
            sp.Stop();
            timers.ForEach(timer => timer.Stop());

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;
            DeleteElementButton.Enabled = true;

            var openGraphDialog = new OpenFileDialog
            {
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

                        chartDisplay.chart1.Series["Amount of points"]
                            .Points.AddXY(time, amount);
                    }

                    chartDisplay.Show();
                }
                catch (Exception)
                {
                    myMessageBox.ShowError("Unable to open chart");
                }
            }
        }


        private ChartForTestingProgram chartForTestingForm1;

        private void button4_Click(object sender, EventArgs e)
        {
            mainTimer.Stop();
            timer1.Stop();
            timer2.Stop();
            sp.Stop();
            timers.ForEach(timer => timer.Stop());
            requireTime = sp.ElapsedMilliseconds;
            chartForTestingForm1 = new ChartForTestingProgram(this, 0,
                new List<ChartForTestingProgram>());
        }

        public long requireTime;

        private void HideAllLabel()
        {
            drawEdgeLabel.Visible = false;
            drawVertexLabel.Visible = false;
            deleteElementLabel.Visible = false;
            changeLengthLabel.Visible = false;
            label1.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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
            label1.Location = new Point(55, 451);
            drawEdgeLabel.Location = new Point(39, 112);
            drawVertexLabel.Location = new Point(36, 222);
            deleteElementLabel.Location = new Point(24, 343);
            changeLengthLabel.Location = new Point(30, 431);

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
        }

        private void CustomizeDesign()
        {
            drawingSubPanel.Visible = false;
            changeParametersSubPanel.Visible = false;
        }

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

        private void drawingButton_Click(object sender, EventArgs e)
        {
            ShowSubMenu(drawingSubPanel);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowSubMenu(changeParametersSubPanel);
        }

        private void toolsButton_Click(object sender, EventArgs e)
        {
            ShowSubMenu(toolsSubPanel);
        }

        private void graphInfoButton_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panel2);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            changeLengthLabel.Visible = !ChangeEdgeLengthButton.Enabled;
            drawEdgeLabel.Visible = !DrawEdgeButton.Enabled;
            drawVertexLabel.Visible = !DrawVertexButton.Enabled;
            deleteElementLabel.Visible = !DeleteElementButton.Enabled;
            label1.Visible = !ChangeEdgeLengthButton.Enabled;

            var graphForCheck = new SpecialKindOfGraphForCheckStronglyDirection(Vertex.Count);

            foreach (var edge in Edges)
                graphForCheck.AddEdge(edge.Ver1, edge.Ver2);

            button8.Text = $@"Cyclomatic number: {Edges.Count - Vertex.Count + graphForCheck.FindComps()}";
            button7.Text = $@"Vertex: {Vertex.Count}";
            button6.Text = $@"Edges: {Edges.Count}";
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ShowSubMenu(panelChart);
        }

        private void exitButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exitButton2_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
