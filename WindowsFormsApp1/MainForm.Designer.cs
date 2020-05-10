namespace WindowsFormsApp1
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DrawVertexButton = new System.Windows.Forms.Button();
            this.DeleteAllGraphButton = new System.Windows.Forms.Button();
            this.DrawEdgeButton = new System.Windows.Forms.Button();
            this.CheckGraphForStrongConnectionButton = new System.Windows.Forms.Button();
            this.GetRandomGraphButton = new System.Windows.Forms.Button();
            this.DeleteElementButton = new System.Windows.Forms.Button();
            this.SaveGraphButton = new System.Windows.Forms.Button();
            this.OpenGraphButton = new System.Windows.Forms.Button();
            this.ChangeEdgeLengthButton = new System.Windows.Forms.Button();
            this.StopDrawingButton = new System.Windows.Forms.Button();
            this.ShowOrHideAdjMatrix = new System.Windows.Forms.Button();
            this.startReseachButton = new System.Windows.Forms.Button();
            this.StopProcessButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.changeParametersSubPanel = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.drawingSubPanel = new System.Windows.Forms.Panel();
            this.drawingButton = new System.Windows.Forms.Button();
            this.field = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.graphInfoButton = new System.Windows.Forms.Button();
            this.toolsSubPanel = new System.Windows.Forms.Panel();
            this.toolsButton = new System.Windows.Forms.Button();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.drawingPanel.SuspendLayout();
            this.changeParametersSubPanel.SuspendLayout();
            this.drawingSubPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.field)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolsSubPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // DrawVertexButton
            // 
            this.DrawVertexButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DrawVertexButton.FlatAppearance.BorderSize = 0;
            this.DrawVertexButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DrawVertexButton.ForeColor = System.Drawing.Color.LightGray;
            this.DrawVertexButton.Location = new System.Drawing.Point(0, 150);
            this.DrawVertexButton.Name = "DrawVertexButton";
            this.DrawVertexButton.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.DrawVertexButton.Size = new System.Drawing.Size(200, 50);
            this.DrawVertexButton.TabIndex = 2;
            this.DrawVertexButton.Text = "Нарисовать вершину";
            this.DrawVertexButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DrawVertexButton.UseVisualStyleBackColor = true;
            this.DrawVertexButton.Click += new System.EventHandler(this.DrawVertexButton_Click);
            // 
            // DeleteAllGraphButton
            // 
            this.DeleteAllGraphButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DeleteAllGraphButton.FlatAppearance.BorderSize = 0;
            this.DeleteAllGraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteAllGraphButton.ForeColor = System.Drawing.Color.LightGray;
            this.DeleteAllGraphButton.Location = new System.Drawing.Point(0, 50);
            this.DeleteAllGraphButton.Name = "DeleteAllGraphButton";
            this.DeleteAllGraphButton.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.DeleteAllGraphButton.Size = new System.Drawing.Size(200, 50);
            this.DeleteAllGraphButton.TabIndex = 4;
            this.DeleteAllGraphButton.Text = "Удалить весь граф";
            this.DeleteAllGraphButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeleteAllGraphButton.UseVisualStyleBackColor = true;
            this.DeleteAllGraphButton.Click += new System.EventHandler(this.DeleteAllGraphButton_Click);
            // 
            // DrawEdgeButton
            // 
            this.DrawEdgeButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DrawEdgeButton.FlatAppearance.BorderSize = 0;
            this.DrawEdgeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DrawEdgeButton.ForeColor = System.Drawing.Color.LightGray;
            this.DrawEdgeButton.Location = new System.Drawing.Point(0, 100);
            this.DrawEdgeButton.Name = "DrawEdgeButton";
            this.DrawEdgeButton.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.DrawEdgeButton.Size = new System.Drawing.Size(200, 50);
            this.DrawEdgeButton.TabIndex = 5;
            this.DrawEdgeButton.Text = "Нарисовать ребро";
            this.DrawEdgeButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DrawEdgeButton.UseVisualStyleBackColor = true;
            this.DrawEdgeButton.Click += new System.EventHandler(this.DrawEdgeButton_Click);
            // 
            // CheckGraphForStrongConnectionButton
            // 
            this.CheckGraphForStrongConnectionButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.CheckGraphForStrongConnectionButton.FlatAppearance.BorderSize = 0;
            this.CheckGraphForStrongConnectionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckGraphForStrongConnectionButton.ForeColor = System.Drawing.Color.LightGray;
            this.CheckGraphForStrongConnectionButton.Location = new System.Drawing.Point(0, 0);
            this.CheckGraphForStrongConnectionButton.Name = "CheckGraphForStrongConnectionButton";
            this.CheckGraphForStrongConnectionButton.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.CheckGraphForStrongConnectionButton.Size = new System.Drawing.Size(220, 60);
            this.CheckGraphForStrongConnectionButton.TabIndex = 11;
            this.CheckGraphForStrongConnectionButton.Text = "Проверить граф на сильносвязность";
            this.CheckGraphForStrongConnectionButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CheckGraphForStrongConnectionButton.UseVisualStyleBackColor = true;
            this.CheckGraphForStrongConnectionButton.Click += new System.EventHandler(this.CheckGraphForStrongConnectionButton_Click);
            // 
            // GetRandomGraphButton
            // 
            this.GetRandomGraphButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.GetRandomGraphButton.FlatAppearance.BorderSize = 0;
            this.GetRandomGraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetRandomGraphButton.ForeColor = System.Drawing.Color.LightGray;
            this.GetRandomGraphButton.Location = new System.Drawing.Point(0, 50);
            this.GetRandomGraphButton.Name = "GetRandomGraphButton";
            this.GetRandomGraphButton.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.GetRandomGraphButton.Size = new System.Drawing.Size(200, 50);
            this.GetRandomGraphButton.TabIndex = 13;
            this.GetRandomGraphButton.Text = "Сгенерировать граф";
            this.GetRandomGraphButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GetRandomGraphButton.UseVisualStyleBackColor = true;
            this.GetRandomGraphButton.Click += new System.EventHandler(this.GetRandomGraphButton_Click);
            // 
            // DeleteElementButton
            // 
            this.DeleteElementButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DeleteElementButton.FlatAppearance.BorderSize = 0;
            this.DeleteElementButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteElementButton.ForeColor = System.Drawing.Color.LightGray;
            this.DeleteElementButton.Location = new System.Drawing.Point(0, 0);
            this.DeleteElementButton.Name = "DeleteElementButton";
            this.DeleteElementButton.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.DeleteElementButton.Size = new System.Drawing.Size(200, 50);
            this.DeleteElementButton.TabIndex = 14;
            this.DeleteElementButton.Text = "Удалить элемент";
            this.DeleteElementButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeleteElementButton.UseVisualStyleBackColor = true;
            this.DeleteElementButton.Click += new System.EventHandler(this.DeleteElementButton_Click);
            // 
            // SaveGraphButton
            // 
            this.SaveGraphButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.SaveGraphButton.FlatAppearance.BorderSize = 0;
            this.SaveGraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveGraphButton.ForeColor = System.Drawing.Color.LightGray;
            this.SaveGraphButton.Location = new System.Drawing.Point(0, 175);
            this.SaveGraphButton.Name = "SaveGraphButton";
            this.SaveGraphButton.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.SaveGraphButton.Size = new System.Drawing.Size(220, 55);
            this.SaveGraphButton.TabIndex = 15;
            this.SaveGraphButton.Text = "Сохранить изображение";
            this.SaveGraphButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveGraphButton.UseVisualStyleBackColor = true;
            this.SaveGraphButton.Click += new System.EventHandler(this.SaveGraphButton_Click);
            // 
            // OpenGraphButton
            // 
            this.OpenGraphButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.OpenGraphButton.FlatAppearance.BorderSize = 0;
            this.OpenGraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenGraphButton.ForeColor = System.Drawing.Color.LightGray;
            this.OpenGraphButton.Location = new System.Drawing.Point(0, 120);
            this.OpenGraphButton.Name = "OpenGraphButton";
            this.OpenGraphButton.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.OpenGraphButton.Size = new System.Drawing.Size(220, 55);
            this.OpenGraphButton.TabIndex = 16;
            this.OpenGraphButton.Text = "Открыть файл";
            this.OpenGraphButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OpenGraphButton.UseVisualStyleBackColor = true;
            this.OpenGraphButton.Click += new System.EventHandler(this.OpenGraphButton_Click);
            // 
            // ChangeEdgeLengthButton
            // 
            this.ChangeEdgeLengthButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChangeEdgeLengthButton.FlatAppearance.BorderSize = 0;
            this.ChangeEdgeLengthButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChangeEdgeLengthButton.ForeColor = System.Drawing.Color.LightGray;
            this.ChangeEdgeLengthButton.Location = new System.Drawing.Point(0, 100);
            this.ChangeEdgeLengthButton.Name = "ChangeEdgeLengthButton";
            this.ChangeEdgeLengthButton.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.ChangeEdgeLengthButton.Size = new System.Drawing.Size(200, 50);
            this.ChangeEdgeLengthButton.TabIndex = 17;
            this.ChangeEdgeLengthButton.Text = "Изменить длину ребра";
            this.ChangeEdgeLengthButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ChangeEdgeLengthButton.UseVisualStyleBackColor = true;
            this.ChangeEdgeLengthButton.Click += new System.EventHandler(this.ChangeLengthButton_Click);
            // 
            // StopDrawingButton
            // 
            this.StopDrawingButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.StopDrawingButton.FlatAppearance.BorderSize = 0;
            this.StopDrawingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopDrawingButton.ForeColor = System.Drawing.Color.LightGray;
            this.StopDrawingButton.Location = new System.Drawing.Point(0, 0);
            this.StopDrawingButton.Name = "StopDrawingButton";
            this.StopDrawingButton.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.StopDrawingButton.Size = new System.Drawing.Size(200, 50);
            this.StopDrawingButton.TabIndex = 18;
            this.StopDrawingButton.Text = "Прекратить рисование";
            this.StopDrawingButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StopDrawingButton.UseVisualStyleBackColor = true;
            this.StopDrawingButton.Click += new System.EventHandler(this.StopDrawingButton_Click);
            // 
            // ShowOrHideAdjMatrix
            // 
            this.ShowOrHideAdjMatrix.Dock = System.Windows.Forms.DockStyle.Top;
            this.ShowOrHideAdjMatrix.Enabled = false;
            this.ShowOrHideAdjMatrix.FlatAppearance.BorderSize = 0;
            this.ShowOrHideAdjMatrix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowOrHideAdjMatrix.ForeColor = System.Drawing.Color.LightGray;
            this.ShowOrHideAdjMatrix.Location = new System.Drawing.Point(0, 60);
            this.ShowOrHideAdjMatrix.Name = "ShowOrHideAdjMatrix";
            this.ShowOrHideAdjMatrix.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.ShowOrHideAdjMatrix.Size = new System.Drawing.Size(220, 60);
            this.ShowOrHideAdjMatrix.TabIndex = 19;
            this.ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
            this.ShowOrHideAdjMatrix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ShowOrHideAdjMatrix.UseVisualStyleBackColor = true;
            this.ShowOrHideAdjMatrix.Click += new System.EventHandler(this.ShowOrHideAdjMatrix_Click);
            // 
            // startReseachButton
            // 
            this.startReseachButton.Location = new System.Drawing.Point(206, 776);
            this.startReseachButton.Name = "startReseachButton";
            this.startReseachButton.Size = new System.Drawing.Size(1144, 92);
            this.startReseachButton.TabIndex = 20;
            this.startReseachButton.Text = "Start reseach";
            this.startReseachButton.UseVisualStyleBackColor = true;
            this.startReseachButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // StopProcessButton
            // 
            this.StopProcessButton.Location = new System.Drawing.Point(1012, 661);
            this.StopProcessButton.Name = "StopProcessButton";
            this.StopProcessButton.Size = new System.Drawing.Size(91, 62);
            this.StopProcessButton.TabIndex = 21;
            this.StopProcessButton.Text = "STOP";
            this.StopProcessButton.UseVisualStyleBackColor = true;
            this.StopProcessButton.Click += new System.EventHandler(this.StopProcessButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(372, 646);
            this.trackBar1.Maximum = 19;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(330, 56);
            this.trackBar1.TabIndex = 23;
            this.trackBar1.Value = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(226, 682);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 48);
            this.button2.TabIndex = 24;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(729, 684);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(107, 46);
            this.button3.TabIndex = 25;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(842, 622);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 80);
            this.button4.TabIndex = 26;
            this.button4.Text = "Испытания программы";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // drawingPanel
            // 
            this.drawingPanel.AutoScroll = true;
            this.drawingPanel.Controls.Add(this.changeParametersSubPanel);
            this.drawingPanel.Controls.Add(this.button5);
            this.drawingPanel.Controls.Add(this.drawingSubPanel);
            this.drawingPanel.Controls.Add(this.drawingButton);
            this.drawingPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.drawingPanel.Location = new System.Drawing.Point(0, 0);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(200, 880);
            this.drawingPanel.TabIndex = 27;
            // 
            // changeParametersSubPanel
            // 
            this.changeParametersSubPanel.Controls.Add(this.ChangeEdgeLengthButton);
            this.changeParametersSubPanel.Controls.Add(this.DeleteAllGraphButton);
            this.changeParametersSubPanel.Controls.Add(this.DeleteElementButton);
            this.changeParametersSubPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.changeParametersSubPanel.Location = new System.Drawing.Point(0, 354);
            this.changeParametersSubPanel.Name = "changeParametersSubPanel";
            this.changeParametersSubPanel.Size = new System.Drawing.Size(200, 154);
            this.changeParametersSubPanel.TabIndex = 3;
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Top;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.ForeColor = System.Drawing.Color.Gainsboro;
            this.button5.Location = new System.Drawing.Point(0, 283);
            this.button5.Name = "button5";
            this.button5.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button5.Size = new System.Drawing.Size(200, 71);
            this.button5.TabIndex = 0;
            this.button5.Text = "Change parameters";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // drawingSubPanel
            // 
            this.drawingSubPanel.Controls.Add(this.DrawVertexButton);
            this.drawingSubPanel.Controls.Add(this.DrawEdgeButton);
            this.drawingSubPanel.Controls.Add(this.GetRandomGraphButton);
            this.drawingSubPanel.Controls.Add(this.StopDrawingButton);
            this.drawingSubPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.drawingSubPanel.Location = new System.Drawing.Point(0, 69);
            this.drawingSubPanel.Name = "drawingSubPanel";
            this.drawingSubPanel.Size = new System.Drawing.Size(200, 214);
            this.drawingSubPanel.TabIndex = 2;
            // 
            // drawingButton
            // 
            this.drawingButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.drawingButton.FlatAppearance.BorderSize = 0;
            this.drawingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drawingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.drawingButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.drawingButton.Location = new System.Drawing.Point(0, 0);
            this.drawingButton.Name = "drawingButton";
            this.drawingButton.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.drawingButton.Size = new System.Drawing.Size(200, 69);
            this.drawingButton.TabIndex = 1;
            this.drawingButton.Text = "Drawing";
            this.drawingButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.drawingButton.UseVisualStyleBackColor = true;
            this.drawingButton.Click += new System.EventHandler(this.drawingButton_Click);
            // 
            // field
            // 
            this.field.Location = new System.Drawing.Point(196, 3);
            this.field.Name = "field";
            this.field.Size = new System.Drawing.Size(1020, 720);
            this.field.TabIndex = 1;
            this.field.TabStop = false;
            this.field.MouseClick += new System.Windows.Forms.MouseEventHandler(this.field_MouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.graphInfoButton);
            this.panel1.Controls.Add(this.toolsSubPanel);
            this.panel1.Controls.Add(this.toolsButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1430, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 880);
            this.panel1.TabIndex = 28;
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Top;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button8.ForeColor = System.Drawing.Color.Red;
            this.button8.Location = new System.Drawing.Point(0, 120);
            this.button8.Name = "button8";
            this.button8.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.button8.Size = new System.Drawing.Size(220, 60);
            this.button8.TabIndex = 6;
            this.button8.Text = "Cyclomatic number";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Dock = System.Windows.Forms.DockStyle.Top;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button7.ForeColor = System.Drawing.Color.Red;
            this.button7.Location = new System.Drawing.Point(0, 0);
            this.button7.Name = "button7";
            this.button7.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.button7.Size = new System.Drawing.Size(220, 60);
            this.button7.TabIndex = 5;
            this.button7.Text = "button7";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Dock = System.Windows.Forms.DockStyle.Top;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button6.ForeColor = System.Drawing.Color.Red;
            this.button6.Location = new System.Drawing.Point(0, 60);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.button6.Size = new System.Drawing.Size(220, 60);
            this.button6.TabIndex = 4;
            this.button6.Text = "button6";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button8);
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.button7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 404);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 196);
            this.panel2.TabIndex = 3;
            // 
            // graphInfoButton
            // 
            this.graphInfoButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.graphInfoButton.FlatAppearance.BorderSize = 0;
            this.graphInfoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.graphInfoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.graphInfoButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.graphInfoButton.Location = new System.Drawing.Point(0, 332);
            this.graphInfoButton.Name = "graphInfoButton";
            this.graphInfoButton.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.graphInfoButton.Size = new System.Drawing.Size(220, 72);
            this.graphInfoButton.TabIndex = 2;
            this.graphInfoButton.Text = "Graph info";
            this.graphInfoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.graphInfoButton.UseVisualStyleBackColor = true;
            this.graphInfoButton.Click += new System.EventHandler(this.graphInfoButton_Click);
            // 
            // toolsSubPanel
            // 
            this.toolsSubPanel.Controls.Add(this.SaveGraphButton);
            this.toolsSubPanel.Controls.Add(this.OpenGraphButton);
            this.toolsSubPanel.Controls.Add(this.ShowOrHideAdjMatrix);
            this.toolsSubPanel.Controls.Add(this.CheckGraphForStrongConnectionButton);
            this.toolsSubPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolsSubPanel.Location = new System.Drawing.Point(0, 58);
            this.toolsSubPanel.Name = "toolsSubPanel";
            this.toolsSubPanel.Size = new System.Drawing.Size(220, 274);
            this.toolsSubPanel.TabIndex = 1;
            // 
            // toolsButton
            // 
            this.toolsButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolsButton.FlatAppearance.BorderSize = 0;
            this.toolsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolsButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.toolsButton.Location = new System.Drawing.Point(0, 0);
            this.toolsButton.Name = "toolsButton";
            this.toolsButton.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.toolsButton.Size = new System.Drawing.Size(220, 58);
            this.toolsButton.TabIndex = 0;
            this.toolsButton.Text = "Tools";
            this.toolsButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolsButton.UseVisualStyleBackColor = true;
            this.toolsButton.Click += new System.EventHandler(this.toolsButton_Click);
            // 
            // timer3
            // 
            this.timer3.Interval = 1;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(1650, 880);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.drawingPanel);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.StopProcessButton);
            this.Controls.Add(this.startReseachButton);
            this.Controls.Add(this.field);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Click += new System.EventHandler(this.MainForm_Click);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.drawingPanel.ResumeLayout(false);
            this.changeParametersSubPanel.ResumeLayout(false);
            this.drawingSubPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.field)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.toolsSubPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button DrawVertexButton;
        private System.Windows.Forms.Button DeleteAllGraphButton;
        private System.Windows.Forms.Button DrawEdgeButton;
        private System.Windows.Forms.Button CheckGraphForStrongConnectionButton;
        private System.Windows.Forms.Button GetRandomGraphButton;
        private System.Windows.Forms.Button DeleteElementButton;
        private System.Windows.Forms.Button SaveGraphButton;
        private System.Windows.Forms.Button OpenGraphButton;
        private System.Windows.Forms.Button ChangeEdgeLengthButton;
        private System.Windows.Forms.Button StopDrawingButton;
        private System.Windows.Forms.Button ShowOrHideAdjMatrix;
        private System.Windows.Forms.Button startReseachButton;
        private System.Windows.Forms.Button StopProcessButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.Panel drawingSubPanel;
        private System.Windows.Forms.Button drawingButton;
        public System.Windows.Forms.PictureBox field;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel changeParametersSubPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel toolsSubPanel;
        private System.Windows.Forms.Button toolsButton;
        private System.Windows.Forms.Button graphInfoButton;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Panel panel2;
    }
}

