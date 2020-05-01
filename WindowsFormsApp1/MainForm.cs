﻿using System;
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
    public partial class MainForm : Form
    {
        // Диалоговые формы.
        private ChooseEdgeForm chooseEdgeFormForm = new ChooseEdgeForm();
        private GetLengthEdgeForm getEdgeLengthForm = new GetLengthEdgeForm();
        private GetRandomGraphForm getRandomGraphForm = new GetRandomGraphForm();

        private CheckGraphForStronglyConnectionForm checkGraphForStronglyConnectionForm =
            new CheckGraphForStronglyConnectionForm();

        private ShowAdjacencyMatrixForm showAdjacencyMatrixForm = new ShowAdjacencyMatrixForm();
        private Chart chartForm = new Chart();
        private Form1 chartDisplay = new Form1();

        // Инструменты для рисования.
        private ToolsForDrawingGraph toolsForDrawing;



        // Списки смежности.
        private List<Vertex> vertex;
        private List<Edge> edges;

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
            toolsForDrawing = new ToolsForDrawingGraph(field.Width, field.Height);
            vertex = new List<Vertex>();
            edges = new List<Edge>();

            chooseEdgeFormForm.Owner = this;
            getEdgeLengthForm.Owner = this;
            getRandomGraphForm.Owner = this;
            checkGraphForStronglyConnectionForm.Owner = this;

            toolsForDrawing.ClearField();
            field.Image = toolsForDrawing.GetBitmap();
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

            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());

                firstPress = true;
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
            }

            // Делаем остальные кнопки активными.
            DeleteElementButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
        }

        /// <summary>
        /// Нажатие на кнопку "Удаление всего графа".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAllGraphButton_Click(object sender, EventArgs e)
        {
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;

                toolsForDrawing.DrawFullGraph(edges, vertex);
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
            }

            // Проверяем, есть ли какой-то элемент для удаления.
            if (vertex.Count != 0)
            {
                // Делаем остальные кнопки активными.
                DrawVertexButton.Enabled = true;
                DeleteElementButton.Enabled = true;
                DrawEdgeButton.Enabled = true;
                ChangeEdgeLengthButton.Enabled = true;

                string message = "Вы действительно хотите удалить весь граф?";
                string caption = "Delete";

                // Диалоговое окно для подтверждения удаления.
                var ConfirmCancel = MessageBox.Show(message, caption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Если подтверилось удаление, удаляем.
                if (ConfirmCancel == DialogResult.Yes)
                {
                    vertex.Clear();
                    edges.Clear();
                    showAdjacencyMatrixForm.AdjMatrixListBox.Items.Clear();
                    toolsForDrawing.ClearField();
                    field.Image = toolsForDrawing.GetBitmap();
                }
            }
            else
            {
                MessageBox.Show("Вам нечего удалять");
            }

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
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
            if (indexVertexForMove != -1)
            {
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
            }

            // Номера вершин, между которыми будем проводить ребро.
            ver1ForConnection = -1;
            ver2ForConnection = -1;

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
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
                // Если была выбрана вершина для перемещения, то перекрашиваем её.
                if (indexVertexForMove != -1)
                {
                    toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                        vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                    firstPress = true;
                    field.Image = toolsForDrawing.GetBitmap();
                }

                // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
                if (ver1ForConnection != -1)
                {
                    toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                        vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                    ver1ForConnection = -1;

                    field.Image = toolsForDrawing.GetBitmap();
                }

                // Делаем остальные кнопки активными.
                DrawVertexButton.Enabled = true;
                DeleteElementButton.Enabled = true;
                DrawEdgeButton.Enabled = true;
                ChangeEdgeLengthButton.Enabled = true;

                // Если нечего проверять, выводим сообщение.
                if (adjMatrix is null || adjMatrix.Length == 0)
                    MessageBox.Show("Постройте граф");
                else
                {
                    // Представляем граф в удобном для проверки виде.
                    GraphForAlg graphForCheck = new GraphForAlg(vertex.Count);

                    foreach (var edge in edges)
                        graphForCheck.AddEdge(edge.Ver1, edge.Ver2);

                    // Проверяем граф на сильносвязность.
                    // В случае, если граф не сильносвязный, то
                    // предлагаем сгенерировать случайный сильносвязный граф,
                    // или продолжить редактирование
                    if (graphForCheck.IsStronglyConnection())
                        MessageBox.Show("Граф сильносвязный!");
                    else
                    {
                        checkGraphForStronglyConnectionForm.Caption.Text =
                            "Граф не является сильносвязным";

                        checkGraphForStronglyConnectionForm.ShowDialog();

                        if (checkGraphForStronglyConnectionForm.generate)
                            GetRandomGraphButton.PerformClick();
                    }
                }

                // Если есть веришны, то появляется возможность открыть матрицу смежности.
                if (vertex.Count != 0)
                    ShowOrHideAdjMatrix.Enabled = true;
                else
                {
                    ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
                    showAdjacencyMatrixForm.Hide();
                    ShowOrHideAdjMatrix.Enabled = false;
                }
            }
            else
            {
                // Если нечего проверять, выводим сообщение.
                if (adjMatrix is null || adjMatrix.Length == 0)
                    MessageBox.Show("Постройте граф");
                else
                {
                    // Представляем граф в удобном для проверки виде.
                    GraphForAlg graphForCheck = new GraphForAlg(vertex.Count);

                    foreach (var edge in edges)
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
                        checkGraphForStronglyConnectionForm.Caption.Text =
                            "Граф не является сильносвязным";

                        checkGraphForStronglyConnectionForm.ShowDialog();

                        if (checkGraphForStronglyConnectionForm.generate)
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
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
            }

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;
            DeleteElementButton.Enabled = true;

            getRandomGraphForm.ShowDialog();

            // Если нажали отмену, то ничего не происходит.
            if (!getRandomGraphForm.cancelGraph)
            {
                // Количество вершин.
                int size = getRandomGraphForm.amount;

                toolsForDrawing.ClearField();

                // Генерируем матрицу смежности.
                //adjMatrix = graph.GetRandomAdjMatrix(size);
                adjMatrix = toolsForDrawing.GetRandomAdjMatrix(size);

                // Устанавливаем вес рёбер.
                adjMatrix = toolsForDrawing.SetDistanceEdge(adjMatrix);

                // Генерируем координаты вершин.
                vertex = toolsForDrawing.GetRandomVertex(adjMatrix,
                    field.Size.Width - 2 * toolsForDrawing.R,
                    field.Size.Height - 2 * toolsForDrawing.R);

                // Заполняем лист рёбер.
                edges = toolsForDrawing.GetRandomEdges(adjMatrix);

                // Отрисовываем полный граф.
                toolsForDrawing.DrawFullGraph(edges, vertex);

                // Выводим матрицу смежности.
                PrintAdjMatrix();

                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
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
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
            }

            // Делаем эту кнопку неактивной.
            DeleteElementButton.Enabled = false;

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
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
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
            }

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
                                              pathForSaveFile.LastIndexOf(".") + 1) + "bin";

                        SaveToolsForGraph();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Нарисуйте что-нибудь");

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
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
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
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

                    toolsForDrawing.ClearField();
                    toolsForDrawing.DrawFullGraph(edges, vertex);
                    field.Image = toolsForDrawing.GetBitmap();
                }
                catch (Exception)
                {
                    MessageBox.Show("Невозможно открыть изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
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
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если нет рёбер, пишем об этом.
            if (edges.Count != 0)
            {
                // Делаем эту кнопку активной.
                ChangeEdgeLengthButton.Enabled = false;

                // Делаем остальные кнопки неактивными.
                DrawVertexButton.Enabled = true;
                DrawEdgeButton.Enabled = true;
                DeleteElementButton.Enabled = true;
            }
            else
                MessageBox.Show("Рёбер нет");

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
        }

        /// <summary>
        /// Нажатие на кнопку "Прекратить рисование".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopDrawingButton_Click(object sender, EventArgs e)
        {
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
            }

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Enabled = false;
            }
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
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
            }

            // Делаем остальные кнопки активными.
            DrawVertexButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            ChangeEdgeLengthButton.Enabled = true;

            // В зависимости от текста открываем/закрываем матрицу смежности.
            if (ShowOrHideAdjMatrix.Text == "Закрыть матрицу")
            {
                showAdjacencyMatrixForm.Hide();
                ShowOrHideAdjMatrix.Text = "Открыть матрицу";
            }
            else
            {
                showAdjacencyMatrixForm.Show();
                ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
            }
        }

        /// <summary>
        /// Устанавливаем текст в кнопки перед открытием
        /// формы для выбора пути для удаления.
        /// </summary>
        /// <param name="ver1"> Номер первой вершины </param>
        /// <param name="ver2"> Номер второй веришны </param>
        private void SetTextInButtonFromChooseEdgeForm(int ver1, int ver2)
        {
            chooseEdgeFormForm.TextForUnderstandingLabel.Text = "Выберите путь, который хотите удалить:";
            chooseEdgeFormForm.FirstOptionButton.Text = $"Удалить путь из {ver1 + 1} в {ver2 + 1}";
            chooseEdgeFormForm.SecondOptionButton.Text = $"Удалить путь из {ver2 + 1} в {ver1 + 1}";
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
                        for (int i = 0; i < vertex.Count; i++)
                        {
                            if (Math.Pow(vertex[i].X - e.X, 2) + Math.Pow(vertex[i].Y - e.Y, 2) <=
                                toolsForDrawing.R * toolsForDrawing.R)
                            {
                                toolsForDrawing.DrawSelectedVertex(vertex[i].X, vertex[i].Y);
                                field.Image = toolsForDrawing.GetBitmap();

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
                            for (int i = 0; i < vertex.Count; i++)
                            {
                                if (i != indexVertexForMove)
                                    if (Math.Abs(e.X - vertex[i].X) < 2 * toolsForDrawing.R
                                        && Math.Abs(e.Y - vertex[i].Y) < 2 * toolsForDrawing.R)
                                    {
                                        MessageBox.Show("Вершины не должны пересекаться", "Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                        firstPress = true;
                                        return;
                                    }
                            }

                            // Тогда двигаем точку и перерисовываем граф.
                            vertex[indexVertexForMove].X = e.X;
                            vertex[indexVertexForMove].Y = e.Y;

                            toolsForDrawing.ClearField();

                            indexVertexForMove = -1;

                            toolsForDrawing.DrawFullGraph(edges, vertex);

                            field.Image = toolsForDrawing.GetBitmap();
                        }
                    }
                }

                // Если нажата кнопка "Изменить длину ребра".
                if (!ChangeEdgeLengthButton.Enabled)
                {
                    // Если мы нажали на вершину, то ничего не происходит.
                    for (int i = 0; i < vertex.Count; i++)
                    {
                        if (Math.Pow(vertex[i].X - e.X, 2) + Math.Pow(vertex[i].Y - e.Y, 2) <=
                            toolsForDrawing.R * toolsForDrawing.R)
                            return;
                    }

                    for (int i = 0; i < edges.Count; i++)
                    {
                        // Если это петля
                        if (edges[i].Ver1 == edges[i].Ver2)
                        {
                            // Петля - это круг. и +- ширина кисти/2 к радиусу. 
                            // У нас центр окружности петли смещён на -R -R.
                            // Если клик попал в тор, то предлагаем изменить длину.

                            if ((Math.Pow(vertex[edges[i].Ver1].X - toolsForDrawing.R - e.X, 2) +
                                 Math.Pow(vertex[edges[i].Ver1].Y - toolsForDrawing.R - e.Y, 2)) <=
                                Math.Pow(toolsForDrawing.R + 4, 2)

                                &&

                                (Math.Pow(vertex[edges[i].Ver1].X - toolsForDrawing.R - e.X, 2) +
                                 Math.Pow(vertex[edges[i].Ver1].Y - toolsForDrawing.R - e.Y, 2)) >=
                                Math.Pow(toolsForDrawing.R - 4, 2))
                            {
                                getEdgeLengthForm.ShowDialog();

                                // Если мы не нажади отмену, то меняем вес.
                                if (!getEdgeLengthForm.cancel)
                                {
                                    edges[i].Weight = getEdgeLengthForm.number;
                                    PrintAdjMatrix();
                                }
                            }
                        }
                        else
                        {
                            // Если x координаты двух вершин ребра не совпадают.
                            if (vertex[edges[i].Ver1].X != vertex[edges[i].Ver2].X)
                            {
                                // Вычисляем коэффициенты в уравнении прямой,
                                // проходящей через две вершины этого ребра, вида y = kx + b.
                                double k = Calculate.GetK(
                                    vertex[edges[i].Ver1], vertex[edges[i].Ver2]);

                                double b = Calculate.GetB(
                                    vertex[edges[i].Ver1], vertex[edges[i].Ver2]);

                                // Проводим прямую между двумя веришнами ребра,
                                // и "расширяем" её на ширину кисти/2.
                                // Если попали в область, то меняем длину этого ребра.
                                if (e.Y <= k * e.X + b + 4 && e.Y >= k * e.X + b - 4)
                                {
                                    // Если есть обратный путь, то выбираем, какой меняем.
                                    if (adjMatrix[edges[i].Ver1, edges[i].Ver2] != 0 &&
                                        adjMatrix[edges[i].Ver2, edges[i].Ver1] != 0)
                                    {
                                        chooseEdgeFormForm.TextForUnderstandingLabel.Text =
                                            "Длину какого пути Вы хотите изменить:";

                                        chooseEdgeFormForm.FirstOptionButton.Text =
                                            $"Изменить длину пути из {edges[i].Ver1 + 1} в " +
                                            $"{edges[i].Ver2 + 1}";

                                        chooseEdgeFormForm.SecondOptionButton.Text =
                                            $"Изменить длину пути из {edges[i].Ver2 + 1} в " +
                                            $"{edges[i].Ver1 + 1}";

                                        chooseEdgeFormForm.ShowDialog();

                                        // Если не отменили изменение длины, то меняем.
                                        if (!chooseEdgeFormForm.cancel)
                                        {
                                            if (chooseEdgeFormForm.firstOption)
                                            {
                                                getEdgeLengthForm.ShowDialog();

                                                if (!getEdgeLengthForm.cancel)
                                                {
                                                    edges[i].Weight = getEdgeLengthForm.number;

                                                    PrintAdjMatrix();

                                                    return;
                                                }

                                                return;
                                            }

                                            getEdgeLengthForm.ShowDialog();

                                            if (!getEdgeLengthForm.cancel)
                                            {
                                                for (int j = i; j < edges.Count; j++)
                                                {
                                                    if (edges[j].Ver1 == edges[i].Ver2 &&
                                                        edges[j].Ver2 == edges[i].Ver1)
                                                    {
                                                        edges[j].Weight = getEdgeLengthForm.number;

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
                                        getEdgeLengthForm.ShowDialog();

                                        if (!getEdgeLengthForm.cancel)
                                        {
                                            edges[i].Weight = getEdgeLengthForm.number;

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
                                if (e.X <= vertex[edges[i].Ver1].X + 4 &&
                                    e.X >= vertex[edges[i].Ver1].X - 4 &&
                                    e.Y < Math.Max(
                                        vertex[edges[i].Ver1].Y,
                                        vertex[edges[i].Ver2].Y) - toolsForDrawing.R &&
                                    e.Y > Math.Min(
                                        vertex[edges[i].Ver1].Y,
                                        vertex[edges[i].Ver2].Y) + toolsForDrawing.R)
                                {
                                    // Если есть обратное ребро, то даём выбрать, 
                                    // длину какого пути надо изменить.
                                    if (adjMatrix[edges[i].Ver1, edges[i].Ver2] != 0 &&
                                        adjMatrix[edges[i].Ver2, edges[i].Ver1] != 0)
                                    {
                                        chooseEdgeFormForm.TextForUnderstandingLabel.Text =
                                            "Длину какого пути Вы хотите изменить:";

                                        chooseEdgeFormForm.FirstOptionButton.Text =
                                            $"Изменить длину пути из {edges[i].Ver1 + 1} в " +
                                            $"{edges[i].Ver2 + 1}";

                                        chooseEdgeFormForm.SecondOptionButton.Text =
                                            $"Изменить длину пути из {edges[i].Ver2 + 1} в " +
                                            $"{edges[i].Ver1 + 1}";

                                        chooseEdgeFormForm.ShowDialog();

                                        // Если не отменили, то меняем длину. 
                                        if (!chooseEdgeFormForm.cancel)
                                        {
                                            if (chooseEdgeFormForm.firstOption)
                                            {
                                                getEdgeLengthForm.ShowDialog();

                                                if (!getEdgeLengthForm.cancel)
                                                {
                                                    edges[i].Weight = getEdgeLengthForm.number;

                                                    PrintAdjMatrix();

                                                    return;
                                                }

                                                return;
                                            }

                                            getEdgeLengthForm.ShowDialog();

                                            if (!getEdgeLengthForm.cancel)
                                            {
                                                for (int j = i; j < edges.Count; j++)
                                                {
                                                    if (edges[j].Ver1 == edges[i].Ver2 &&
                                                        edges[j].Ver2 == edges[i].Ver1)
                                                    {
                                                        edges[j].Weight = getEdgeLengthForm.number;

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
                                        getEdgeLengthForm.ShowDialog();

                                        if (!getEdgeLengthForm.cancel)
                                        {
                                            edges[i].Weight = getEdgeLengthForm.number;

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
                    for (int i = 0; i < vertex.Count; i++)
                    {
                        if (Math.Abs(e.X - vertex[i].X) < 2 * toolsForDrawing.R
                            && Math.Abs(e.Y - vertex[i].Y) < 2 * toolsForDrawing.R)
                        {
                            MessageBox.Show("Вершины не должны пересекаться", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    vertex.Add(new Vertex(e.X, e.Y));
                    toolsForDrawing.DrawVertex(e.X, e.Y, vertex.Count.ToString());
                    field.Image = toolsForDrawing.GetBitmap();

                    PrintAdjMatrix();
                }

                //Если нажата кнопка "Рисовать ребро".
                if (!DrawEdgeButton.Enabled)
                {
                    // Смотрим, на какую вершину нажали, затем выделяем её.
                    for (int i = 0; i < vertex.Count; i++)
                    {
                        if (Math.Pow(vertex[i].X - e.X, 2) + Math.Pow(vertex[i].Y - e.Y, 2) <=
                            toolsForDrawing.R * toolsForDrawing.R)
                        {
                            // Если это первая вершина.
                            if (ver1ForConnection == -1)
                            {
                                toolsForDrawing.DrawSelectedVertex(vertex[i].X, vertex[i].Y);
                                ver1ForConnection = i;
                                field.Image = toolsForDrawing.GetBitmap();
                                break;
                            }

                            // Если это вторая веришна.
                            if (ver2ForConnection == -1)
                            {
                                toolsForDrawing.DrawSelectedVertex(vertex[i].X, vertex[i].Y);
                                ver2ForConnection = i;
                                field.Image = toolsForDrawing.GetBitmap();

                                // Если между ними уже есть ребро, то предупреждаем об этом.
                                if (adjMatrix[ver1ForConnection, ver2ForConnection] == 0)
                                {
                                    getEdgeLengthForm.ShowDialog();

                                    // Если решили отменить рисование ребра.
                                    if (getEdgeLengthForm.cancel)
                                    {
                                        toolsForDrawing.DrawVertex(
                                            vertex[ver1ForConnection].X,
                                            vertex[ver1ForConnection].Y,
                                            (ver1ForConnection + 1).ToString());

                                        toolsForDrawing.DrawVertex(
                                            vertex[ver2ForConnection].X,
                                            vertex[ver2ForConnection].Y,
                                            (ver2ForConnection + 1).ToString());

                                        getEdgeLengthForm.cancel = false;

                                        ver1ForConnection = ver2ForConnection = -1;
                                    }
                                    else
                                    {
                                        edges.Add(new Edge(
                                            ver1ForConnection,
                                            ver2ForConnection,
                                            getEdgeLengthForm.number));

                                        toolsForDrawing.DrawEdge(
                                            vertex[ver1ForConnection],
                                            vertex[ver2ForConnection],
                                            edges[edges.Count - 1]);

                                        ver1ForConnection = -1;
                                        ver2ForConnection = -1;

                                        PrintAdjMatrix();

                                        DrawVertexButton.Enabled = true;
                                    }

                                    field.Image = toolsForDrawing.GetBitmap();

                                    break;
                                }

                                MessageBox.Show($"Ребро из вершины {ver1ForConnection + 1} " +
                                                $"в вершину {ver2ForConnection + 1} уже есть!");

                                // Убираем выделение с вершин.
                                toolsForDrawing.DrawVertex(
                                    vertex[ver1ForConnection].X,
                                    vertex[ver1ForConnection].Y,
                                    (ver1ForConnection + 1).ToString());

                                toolsForDrawing.DrawVertex(
                                    vertex[ver2ForConnection].X,
                                    vertex[ver2ForConnection].Y,
                                    (ver2ForConnection + 1).ToString());

                                ver1ForConnection = ver2ForConnection = -1;

                                field.Image = toolsForDrawing.GetBitmap();
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
                    for (int i = 0; i < vertex.Count; i++)
                    {
                        if (Math.Pow(vertex[i].X - e.X, 2) + Math.Pow(vertex[i].Y - e.Y, 2) <=
                            toolsForDrawing.R * toolsForDrawing.R)
                        {
                            string message = "Это действие является необратимым," +
                                             " Вы уверены, что хотите продолжить?";

                            string caption = "Confirm";

                            var confirm = MessageBox.Show(message, caption,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (confirm == DialogResult.Yes)
                            {
                                for (int j = 0; j < edges.Count; j++)
                                {
                                    if (edges[j].Ver1 == i || edges[j].Ver2 == i)
                                    {
                                        edges.RemoveAt(j);
                                        j--;
                                    }
                                    else
                                    {
                                        // Уменьшаем номер веришны у рёбер,
                                        // которые соединены с вершинами,
                                        // у которых ноер больше
                                        if (edges[j].Ver1 > i)
                                            edges[j].Ver1--;

                                        if (edges[j].Ver2 > i)
                                            edges[j].Ver2--;
                                    }
                                }

                                vertex.RemoveAt(i);
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
                        for (var i = 0; i < edges.Count; i++)
                        {
                            // Если это петля
                            if (edges[i].Ver1 == edges[i].Ver2)
                            {
                                // Петля - это круг. и +- ширина кисти/2 к радиусу. 
                                // У нас центр окружности петли смещён на -R -R.
                                // Если клик попал в тор, то предлагаем изменить длину.
                                if ((Math.Pow(vertex[edges[i].Ver1].X - toolsForDrawing.R - e.X, 2)
                                     +
                                     Math.Pow(vertex[edges[i].Ver1].Y - toolsForDrawing.R - e.Y, 2))
                                    <=
                                    Math.Pow(toolsForDrawing.R + 4, 2)

                                    &&

                                    (Math.Pow(vertex[edges[i].Ver1].X - toolsForDrawing.R - e.X, 2)
                                     +
                                     Math.Pow(vertex[edges[i].Ver1].Y - toolsForDrawing.R - e.Y, 2))
                                    >=
                                    Math.Pow(toolsForDrawing.R - 4, 2))
                                {
                                    var message = "Это действие является необратимым," +
                                                  " Вы уверены, что хотите продолжить?";
                                    var caption = "Confirm";

                                    var confirm = MessageBox.Show(message, caption,
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (confirm == DialogResult.Yes)
                                    {
                                        edges.RemoveAt(i);
                                        somethingDeleted = true;
                                    }

                                    break;
                                }
                            }
                            else
                            {
                                // Если x координаты вершин ребра не совпдадают.
                                if (vertex[edges[i].Ver1].X != vertex[edges[i].Ver2].X)
                                {
                                    // Вычисляем коэффициенты в уравнении прямой,
                                    // проходящей через две вершины этого ребра, вида y = kx + b.
                                    double k = Calculate.GetK(
                                        vertex[edges[i].Ver1], vertex[edges[i].Ver2]);

                                    double b = Calculate.GetB(
                                        vertex[edges[i].Ver1], vertex[edges[i].Ver2]);

                                    // Проводим прямую между двумя веришнами ребра,
                                    // и "расширяем" её на ширину кисти/2.
                                    // Если попали в область, то предлагаем удалить это ребро.
                                    if (e.Y <= k * e.X + b + 4 && e.Y >= k * e.X + b - 4)
                                    {
                                        // Если есть обратный путь, то предлагаем выбрать путь,
                                        // который надо удалить.
                                        if (adjMatrix[edges[i].Ver1, edges[i].Ver2] != 0 &&
                                            adjMatrix[edges[i].Ver2, edges[i].Ver1] != 0)
                                        {
                                            SetTextInButtonFromChooseEdgeForm(
                                                edges[i].Ver1, edges[i].Ver2);

                                            chooseEdgeFormForm.ShowDialog();

                                            // Если не нажали отмену, то удаляем.
                                            if (!chooseEdgeFormForm.cancel)
                                            {
                                                if (chooseEdgeFormForm.firstOption)
                                                {
                                                    edges.RemoveAt(i);
                                                    somethingDeleted = true;

                                                    chooseEdgeFormForm.firstOption = false;
                                                    break;
                                                }

                                                for (int j = 0; j < edges.Count; j++)
                                                {
                                                    if (edges[j].Ver1 == edges[i].Ver2 &&
                                                        edges[j].Ver2 == edges[i].Ver1)
                                                    {
                                                        edges.RemoveAt(j);
                                                        break;
                                                    }
                                                }

                                                somethingDeleted = true;
                                                break;
                                            }

                                            return;
                                        }

                                        string message = "Это действие является необратимым," +
                                                         " Вы уверены, что хотите продолжить?";
                                        string caption = "Confirm";

                                        var confirm = MessageBox.Show(message, caption,
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        if (confirm == DialogResult.Yes)
                                        {
                                            edges.RemoveAt(i);
                                            somethingDeleted = true;
                                        }

                                        break;
                                    }
                                }

                                // Если x координаты вершин ребра совпадают.
                                if (vertex[edges[i].Ver1].X == vertex[edges[i].Ver2].X)
                                {
                                    // Если x координаты вершин ребра совпали, 
                                    // то проводим между ними прямую, и 
                                    // "расширяем" её вверх и вниз на ширину кисти/2.
                                    if (e.X <= vertex[edges[i].Ver1].X + 4 &&
                                        e.X >= vertex[edges[i].Ver1].X - 4 &&
                                        e.Y < Math.Max(
                                            vertex[edges[i].Ver1].Y,
                                            vertex[edges[i].Ver2].Y) - toolsForDrawing.R &&
                                        e.Y > Math.Min(
                                            vertex[edges[i].Ver1].Y,
                                            vertex[edges[i].Ver2].Y) + toolsForDrawing.R)
                                    {
                                        // Если есть обратный путь, то предлагаем выбрать путь,
                                        // который надо удалить. 
                                        if (adjMatrix[edges[i].Ver1, edges[i].Ver2] != 0 &&
                                            adjMatrix[edges[i].Ver2, edges[i].Ver1] != 0)
                                        {
                                            SetTextInButtonFromChooseEdgeForm(
                                                edges[i].Ver1, edges[i].Ver2);

                                            chooseEdgeFormForm.ShowDialog();

                                            // Если не нажали отмену, то удаляем.
                                            if (!chooseEdgeFormForm.cancel)
                                            {
                                                if (chooseEdgeFormForm.firstOption)
                                                {
                                                    edges.RemoveAt(i);
                                                    somethingDeleted = true;
                                                    chooseEdgeFormForm.firstOption = false;
                                                    break;
                                                }

                                                for (int j = 0; j < edges.Count; j++)
                                                {
                                                    if (edges[j].Ver1 == edges[i].Ver2)
                                                    {
                                                        edges.RemoveAt(j);
                                                        break;
                                                    }
                                                }

                                                somethingDeleted = true;
                                                break;
                                            }

                                            return;
                                        }

                                        string message = "Это действие является необратимым," +
                                                         " Вы уверены, что хотите продолжить?";
                                        string caption = "Confirm";

                                        var confirm = MessageBox.Show(message, caption,
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);

                                        if (confirm == DialogResult.Yes)
                                        {
                                            edges.RemoveAt(i);
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
                        toolsForDrawing.ClearField();
                        toolsForDrawing.DrawFullGraph(edges, vertex);
                        field.Image = toolsForDrawing.GetBitmap();
                        PrintAdjMatrix();
                    }
                }

                // В зависимости от текста открываем/закрываем матрицу смежности.
                if (vertex.Count != 0)
                    ShowOrHideAdjMatrix.Enabled = true;
                else
                {
                    ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
                    showAdjacencyMatrixForm.Hide();
                    ShowOrHideAdjMatrix.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Записываем в файл необходимые данные.
        /// </summary>
        private void SaveToolsForGraph()
        {
            File.WriteAllText(pathForSaveFile, default);

            // Сначала число вершин, затем сами вершины.
            File.AppendAllText(pathForSaveFile, vertex.Count + Environment.NewLine);

            for (int i = 0; i < vertex.Count; i++)
                File.AppendAllText(pathForSaveFile, vertex[i] + Environment.NewLine);

            // Сначала число рёбер, потом сами рёбра.
            File.AppendAllText(pathForSaveFile, edges.Count + Environment.NewLine);

            for (int i = 0; i < edges.Count; i++)
                if (i != edges.Count - 1)
                    File.AppendAllText(pathForSaveFile, edges[i] + Environment.NewLine);
                else
                    File.AppendAllText(pathForSaveFile, edges[i].ToString());
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
                    toolsForDrawing.DrawVertex(
                        vertex[ver1ForConnection].X,
                        vertex[ver1ForConnection].Y,
                        (ver1ForConnection + 1).ToString());
                    ver1ForConnection = -1;
                }

            // Отмена перетаскивания вершины (нажатие ESC).
            if (!firstPress)
            {
                if (e.KeyChar == (char)Keys.Escape)
                {
                    toolsForDrawing.DrawVertex(
                        vertex[indexVertexForMove].X,
                        vertex[indexVertexForMove].Y,
                        (indexVertexForMove + 1).ToString());
                    indexVertexForMove = -1;

                    firstPress = true;
                }
            }

            field.Image = toolsForDrawing.GetBitmap();
        }

        // Methods for work with files.

        /// <summary>
        /// Устанавливаем необходимые данные из файла.
        /// </summary>
        private void SetDataFromFile()
        {
            int vertexCount;
            int edgesCount;

            vertex.Clear();
            edges.Clear();

            string[] lines = File.ReadAllLines(pathForOpenFile);

            // Количество вершин.
            vertexCount = int.Parse(lines[0]);

            // Заполняем список вершин.
            for (int i = 1; i < vertexCount + 1; i++)
            {
                int xCoord = int.Parse(lines[i].Split(' ')[0]);
                int yCoord = int.Parse(lines[i].Split(' ')[1]);

                vertex.Add(new Vertex(xCoord, yCoord));
            }

            // Количество рёбер.
            edgesCount = int.Parse(lines[1 + vertexCount]);

            // Заполняем список рёбер.
            for (int i = 2 + vertexCount; i < 2 + vertexCount + edgesCount; i++)
            {
                int ver1 = int.Parse(lines[i].Split(' ')[0]);
                int ver2 = int.Parse(lines[i].Split(' ')[1]);
                double disctance = double.Parse(lines[i].Split(' ')[2]);

                edges.Add(new Edge(ver1, ver2, disctance));
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
            adjMatrix = new double[vertex.Count, vertex.Count];

            toolsForDrawing.FillAdjMatrix(vertex.Count, edges, adjMatrix);

            showAdjacencyMatrixForm.AdjMatrixListBox.Items.Clear();

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
            for (int i = 0; i < vertex.Count; i++)
            {
                // Отдельно высчитываем расстояние между столбцами для двузначных чисел.
                if (i < 9)
                    curStrForPrint = (i + 1) + GetSpaces(3) + "|" + GetSpaces(3);
                else
                    curStrForPrint = (i + 1) + GetSpaces(1) + "|" + GetSpaces(3);

                for (int j = 0; j < vertex.Count; j++)
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
                            switch (Calculate.GetDigits(adjMatrix[i, j]))
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
                            switch (Calculate.GetDigits(adjMatrix[i, j]))
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
                            switch (Calculate.GetDigits(adjMatrix[i, j]))
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
                            switch (Calculate.GetDigits(adjMatrix[i, j]))
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
                            switch (Calculate.GetDigits(adjMatrix[i, j]))
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
                            switch (Calculate.GetDigits(adjMatrix[i, j]))
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
                showAdjacencyMatrixForm.AdjMatrixListBox.Items.Add(curStrForPrint);

                // Делаем пропуск строки, если это не последняя строка.
                if (i != vertex.Count - 1)
                    showAdjacencyMatrixForm.AdjMatrixListBox.Items.Add(GetSpaces(5) + "|");

            }

            // Вставляем первую и вторую строки в ListBox.
            showAdjacencyMatrixForm.AdjMatrixListBox.Items.Insert(0, caption);
            showAdjacencyMatrixForm.AdjMatrixListBox.Items.Insert(1, strFromHyphen);

            // Если вершин нет, закрываем матрицу.
            // Если есть, то выводим матрицу, при этом оставляем фокус на этом окне.
            if (vertex.Count == 0)
            {
                showAdjacencyMatrixForm.AdjMatrixListBox.Items.Clear();
                showAdjacencyMatrixForm.Hide();
            }
            else if (ShowOrHideAdjMatrix.Text == "Закрыть матрицу")
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
                    if (Calculate.GetDigits(adjMatrix[i, j]) > maxDigitsCount)
                        maxDigitsCount = Calculate.GetDigits(adjMatrix[i, j]);

            return maxDigitsCount;
        }

        /// <summary>
        /// Проверка, является ли число целым.
        /// </summary>
        /// <param name="val"> Число </param>
        /// <returns> true, если число целое,
        ///           false, если нет </returns>
        private bool IsInt(double val) =>
            int.TryParse(val.ToString(), out var temp);


        // Обработать случай, когда точки на одной прямой.
        // Когда возвращается в вершину, vertex выходит из диапазона.
        private double changeStep;
        private double step;

        private int totalCount = 0;
        private bool firstVertex = true;

        private bool er = true;

        private float coefficient = 1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            chartForm.chartForm.Series["Amount of points"]
                .Points.AddXY((int)(sp.ElapsedMilliseconds * coefficient) / 1000, totalCount);
        }


        private void MainTick(List<Edge>[] listArr)
        {
            coefficient = (float)(trackBar1.Value / 10.0);
            // Если готова выпустить точку.
            var count = points.Count;

            for (var i = 0; i < vertex.Count; i++)
            {
                if (vertex[i].hasPoint)
                {
                    vertex[i].hasPoint = false;

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
                    vertex[points[i].Ver2].hasPoint = true;
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

        private readonly List<Stopwatch> timers = new List<Stopwatch>();
        private readonly List<Edge> points = new List<Edge>();

        private bool clickContinue;
        private Timer mainTimer = new Timer();
        Stopwatch sp = new Stopwatch();

        private void button1_Click(object sender, EventArgs e)
        {
            chartForm.chartForm.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chartForm.chartForm.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chartForm.chartForm.ChartAreas[0].AxisX.Minimum = 0;

            clickContinue = true;
            CheckGraphForStrongConnectionButton.PerformClick();

            if (responseSC)
            {
                vertex[0].hasPoint = true;
                var listArr = new List<Edge>[vertex.Count];

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
                timer2.Start();
                sp.Start();
                chartForm.Show();
                Activate();
            }
        }

        /// <summary>
        /// Кнопка STOP/Continue.
        /// </summary>
        /// <param name="sender"> Sender </param>
        /// <param name="e"> E </param>
        private void StopProcessButton_Click(object sender, EventArgs e)
        {
            if (StopProcessButton.Text == "STOP")
            {
                mainTimer.Stop();
                timer1.Stop();
                timer2.Stop();
                sp.Stop();
                timers.ForEach(timer => timer.Stop());
                StopProcessButton.Text = "CONTINUE";
            }
            else
            {
                sp.Start();
                mainTimer.Start();
                timer1.Start();
                timer2.Start();
                timers.ForEach(timer => timer.Start());
                StopProcessButton.Text = "STOP";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
            }

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
                saveGraphDialog.Filter = "Files(*.CSV)|*.CSV";


                saveGraphDialog.ShowHelp = true;

                // Процесс сохранения файла.
                if (saveGraphDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string sv = saveGraphDialog.FileName;
                        pathToCsvFile = sv;
                        File.WriteAllText(sv, chartData.ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Нарисуйте что-нибудь");

            // Если есть веришны, то появляется возможность открыть матрицу смежности.
            if (vertex.Count != 0)
                ShowOrHideAdjMatrix.Enabled = true;
            else
            {
                ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
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

        private static string pathToCsvFile;

        private void button3_Click(object sender, EventArgs e)
        {
            chartDisplay.chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chartDisplay.chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chartDisplay.chart1.ChartAreas[0].AxisX.Minimum = 0;

            // Если была выбрана вершина для перемещения, то перекрашиваем её.
            if (indexVertexForMove != -1)
            {
                toolsForDrawing.DrawVertex(vertex[indexVertexForMove].X,
                    vertex[indexVertexForMove].Y, (1 + indexVertexForMove).ToString());
                firstPress = true;
                field.Image = toolsForDrawing.GetBitmap();
            }

            // Если была выбрана вершина для создания ребра, то перевкрашиваем её.
            if (ver1ForConnection != -1)
            {
                toolsForDrawing.DrawVertex(vertex[ver1ForConnection].X,
                    vertex[ver1ForConnection].Y, (1 + ver1ForConnection).ToString());

                ver1ForConnection = -1;

                field.Image = toolsForDrawing.GetBitmap();
            }

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

                        chartDisplay.chart1.Series["Series1"]
                            .Points.AddXY(time, amount);
                    }

                    chartDisplay.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("Невозможно открыть изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
