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
            this.saveChartButton = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.openChartButton = new System.Windows.Forms.Button();
            this.testingProgrammButton = new System.Windows.Forms.Button();
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.changeParametersSubPanel = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.drawingSubPanel = new System.Windows.Forms.Panel();
            this.drawEdgeLabel = new System.Windows.Forms.Label();
            this.drawingButton = new System.Windows.Forms.Button();
            this.field = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.graphInfoButton = new System.Windows.Forms.Button();
            this.toolsSubPanel = new System.Windows.Forms.Panel();
            this.exitButton = new System.Windows.Forms.Button();
            this.toolsButton = new System.Windows.Forms.Button();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.drawVertexLabel = new System.Windows.Forms.Label();
            this.deleteElementLabel = new System.Windows.Forms.Label();
            this.changeLengthLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelChart = new System.Windows.Forms.Panel();
            this.exitButton2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.x05 = new System.Windows.Forms.Label();
            this.x2 = new System.Windows.Forms.Label();
            this.mathKernel1 = new Wolfram.NETLink.MathKernel();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.drawingPanel.SuspendLayout();
            this.changeParametersSubPanel.SuspendLayout();
            this.drawingSubPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.field)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolsSubPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // DrawVertexButton
            // 
            this.DrawVertexButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DrawVertexButton.FlatAppearance.BorderSize = 0;
            this.DrawVertexButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DrawVertexButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DrawVertexButton.ForeColor = System.Drawing.Color.Cyan;
            this.DrawVertexButton.Location = new System.Drawing.Point(0, 180);
            this.DrawVertexButton.Name = "DrawVertexButton";
            this.DrawVertexButton.Size = new System.Drawing.Size(210, 60);
            this.DrawVertexButton.TabIndex = 2;
            this.DrawVertexButton.Text = "Draw vertex";
            this.DrawVertexButton.UseVisualStyleBackColor = true;
            this.DrawVertexButton.Click += new System.EventHandler(this.DrawVertexButton_Click);
            // 
            // DeleteAllGraphButton
            // 
            this.DeleteAllGraphButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DeleteAllGraphButton.FlatAppearance.BorderSize = 0;
            this.DeleteAllGraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteAllGraphButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteAllGraphButton.ForeColor = System.Drawing.Color.Magenta;
            this.DeleteAllGraphButton.Location = new System.Drawing.Point(0, 60);
            this.DeleteAllGraphButton.Name = "DeleteAllGraphButton";
            this.DeleteAllGraphButton.Size = new System.Drawing.Size(210, 60);
            this.DeleteAllGraphButton.TabIndex = 4;
            this.DeleteAllGraphButton.Text = "Delete full graph";
            this.DeleteAllGraphButton.UseVisualStyleBackColor = true;
            this.DeleteAllGraphButton.Click += new System.EventHandler(this.DeleteAllGraphButton_Click);
            // 
            // DrawEdgeButton
            // 
            this.DrawEdgeButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DrawEdgeButton.FlatAppearance.BorderSize = 0;
            this.DrawEdgeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DrawEdgeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DrawEdgeButton.ForeColor = System.Drawing.Color.Cyan;
            this.DrawEdgeButton.Location = new System.Drawing.Point(0, 120);
            this.DrawEdgeButton.Name = "DrawEdgeButton";
            this.DrawEdgeButton.Size = new System.Drawing.Size(210, 60);
            this.DrawEdgeButton.TabIndex = 5;
            this.DrawEdgeButton.Text = "Draw edge";
            this.DrawEdgeButton.UseVisualStyleBackColor = true;
            this.DrawEdgeButton.Click += new System.EventHandler(this.DrawEdgeButton_Click);
            // 
            // CheckGraphForStrongConnectionButton
            // 
            this.CheckGraphForStrongConnectionButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.CheckGraphForStrongConnectionButton.FlatAppearance.BorderSize = 0;
            this.CheckGraphForStrongConnectionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckGraphForStrongConnectionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckGraphForStrongConnectionButton.ForeColor = System.Drawing.Color.Chartreuse;
            this.CheckGraphForStrongConnectionButton.Location = new System.Drawing.Point(0, 0);
            this.CheckGraphForStrongConnectionButton.Name = "CheckGraphForStrongConnectionButton";
            this.CheckGraphForStrongConnectionButton.Size = new System.Drawing.Size(220, 60);
            this.CheckGraphForStrongConnectionButton.TabIndex = 11;
            this.CheckGraphForStrongConnectionButton.Text = "Check graph for strongly direction";
            this.CheckGraphForStrongConnectionButton.UseVisualStyleBackColor = true;
            this.CheckGraphForStrongConnectionButton.Click += new System.EventHandler(this.CheckGraphForStrongConnectionButton_Click);
            // 
            // GetRandomGraphButton
            // 
            this.GetRandomGraphButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.GetRandomGraphButton.FlatAppearance.BorderSize = 0;
            this.GetRandomGraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetRandomGraphButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GetRandomGraphButton.ForeColor = System.Drawing.Color.Cyan;
            this.GetRandomGraphButton.Location = new System.Drawing.Point(0, 60);
            this.GetRandomGraphButton.Name = "GetRandomGraphButton";
            this.GetRandomGraphButton.Size = new System.Drawing.Size(210, 60);
            this.GetRandomGraphButton.TabIndex = 13;
            this.GetRandomGraphButton.Text = "Generated graph";
            this.GetRandomGraphButton.UseVisualStyleBackColor = true;
            this.GetRandomGraphButton.Click += new System.EventHandler(this.GetRandomGraphButton_Click);
            // 
            // DeleteElementButton
            // 
            this.DeleteElementButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DeleteElementButton.FlatAppearance.BorderSize = 0;
            this.DeleteElementButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteElementButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteElementButton.ForeColor = System.Drawing.Color.Magenta;
            this.DeleteElementButton.Location = new System.Drawing.Point(0, 0);
            this.DeleteElementButton.Name = "DeleteElementButton";
            this.DeleteElementButton.Size = new System.Drawing.Size(210, 60);
            this.DeleteElementButton.TabIndex = 14;
            this.DeleteElementButton.Text = "Delete element";
            this.DeleteElementButton.UseVisualStyleBackColor = true;
            this.DeleteElementButton.Click += new System.EventHandler(this.DeleteElementButton_Click);
            // 
            // SaveGraphButton
            // 
            this.SaveGraphButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.SaveGraphButton.FlatAppearance.BorderSize = 0;
            this.SaveGraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveGraphButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveGraphButton.ForeColor = System.Drawing.Color.Chartreuse;
            this.SaveGraphButton.Location = new System.Drawing.Point(0, 180);
            this.SaveGraphButton.Name = "SaveGraphButton";
            this.SaveGraphButton.Size = new System.Drawing.Size(220, 60);
            this.SaveGraphButton.TabIndex = 15;
            this.SaveGraphButton.Text = "Save file";
            this.SaveGraphButton.UseVisualStyleBackColor = true;
            this.SaveGraphButton.Click += new System.EventHandler(this.SaveGraphButton_Click);
            // 
            // OpenGraphButton
            // 
            this.OpenGraphButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.OpenGraphButton.FlatAppearance.BorderSize = 0;
            this.OpenGraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenGraphButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OpenGraphButton.ForeColor = System.Drawing.Color.Chartreuse;
            this.OpenGraphButton.Location = new System.Drawing.Point(0, 120);
            this.OpenGraphButton.Name = "OpenGraphButton";
            this.OpenGraphButton.Size = new System.Drawing.Size(220, 60);
            this.OpenGraphButton.TabIndex = 16;
            this.OpenGraphButton.Text = "Open file";
            this.OpenGraphButton.UseVisualStyleBackColor = true;
            this.OpenGraphButton.Click += new System.EventHandler(this.OpenGraphButton_Click);
            // 
            // ChangeEdgeLengthButton
            // 
            this.ChangeEdgeLengthButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChangeEdgeLengthButton.FlatAppearance.BorderSize = 0;
            this.ChangeEdgeLengthButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChangeEdgeLengthButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChangeEdgeLengthButton.ForeColor = System.Drawing.Color.Magenta;
            this.ChangeEdgeLengthButton.Location = new System.Drawing.Point(0, 120);
            this.ChangeEdgeLengthButton.Name = "ChangeEdgeLengthButton";
            this.ChangeEdgeLengthButton.Size = new System.Drawing.Size(210, 60);
            this.ChangeEdgeLengthButton.TabIndex = 17;
            this.ChangeEdgeLengthButton.Text = "Change edge weight";
            this.ChangeEdgeLengthButton.UseVisualStyleBackColor = true;
            this.ChangeEdgeLengthButton.Click += new System.EventHandler(this.ChangeLengthButton_Click);
            // 
            // StopDrawingButton
            // 
            this.StopDrawingButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.StopDrawingButton.FlatAppearance.BorderSize = 0;
            this.StopDrawingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopDrawingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StopDrawingButton.ForeColor = System.Drawing.Color.Cyan;
            this.StopDrawingButton.Location = new System.Drawing.Point(0, 0);
            this.StopDrawingButton.Name = "StopDrawingButton";
            this.StopDrawingButton.Size = new System.Drawing.Size(210, 60);
            this.StopDrawingButton.TabIndex = 18;
            this.StopDrawingButton.Text = "Stop drawing";
            this.StopDrawingButton.UseVisualStyleBackColor = true;
            this.StopDrawingButton.Click += new System.EventHandler(this.StopDrawingButton_Click);
            // 
            // ShowOrHideAdjMatrix
            // 
            this.ShowOrHideAdjMatrix.Dock = System.Windows.Forms.DockStyle.Top;
            this.ShowOrHideAdjMatrix.Enabled = false;
            this.ShowOrHideAdjMatrix.FlatAppearance.BorderSize = 0;
            this.ShowOrHideAdjMatrix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowOrHideAdjMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShowOrHideAdjMatrix.ForeColor = System.Drawing.Color.Chartreuse;
            this.ShowOrHideAdjMatrix.Location = new System.Drawing.Point(0, 60);
            this.ShowOrHideAdjMatrix.Name = "ShowOrHideAdjMatrix";
            this.ShowOrHideAdjMatrix.Size = new System.Drawing.Size(220, 60);
            this.ShowOrHideAdjMatrix.TabIndex = 19;
            this.ShowOrHideAdjMatrix.Text = "Close matrix";
            this.ShowOrHideAdjMatrix.UseVisualStyleBackColor = true;
            this.ShowOrHideAdjMatrix.Click += new System.EventHandler(this.ShowOrHideAdjMatrix_Click);
            // 
            // startReseachButton
            // 
            this.startReseachButton.FlatAppearance.BorderSize = 0;
            this.startReseachButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startReseachButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startReseachButton.ForeColor = System.Drawing.Color.LimeGreen;
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
            this.StopProcessButton.BackColor = System.Drawing.Color.Black;
            this.StopProcessButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.StopProcessButton.FlatAppearance.BorderSize = 0;
            this.StopProcessButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopProcessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StopProcessButton.ForeColor = System.Drawing.Color.LightGray;
            this.StopProcessButton.Location = new System.Drawing.Point(0, 0);
            this.StopProcessButton.Name = "StopProcessButton";
            this.StopProcessButton.Size = new System.Drawing.Size(211, 60);
            this.StopProcessButton.TabIndex = 21;
            this.StopProcessButton.Text = "Get main part";
            this.StopProcessButton.UseVisualStyleBackColor = false;
            this.StopProcessButton.Click += new System.EventHandler(this.StopProcessButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(358, 760);
            this.trackBar1.Maximum = 19;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(722, 56);
            this.trackBar1.TabIndex = 23;
            this.trackBar1.Value = 10;
            // 
            // saveChartButton
            // 
            this.saveChartButton.BackColor = System.Drawing.Color.Black;
            this.saveChartButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.saveChartButton.FlatAppearance.BorderSize = 0;
            this.saveChartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveChartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveChartButton.ForeColor = System.Drawing.Color.LightGray;
            this.saveChartButton.Location = new System.Drawing.Point(0, 180);
            this.saveChartButton.Name = "saveChartButton";
            this.saveChartButton.Size = new System.Drawing.Size(211, 60);
            this.saveChartButton.TabIndex = 24;
            this.saveChartButton.Text = "Save chart";
            this.saveChartButton.UseVisualStyleBackColor = false;
            this.saveChartButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // openChartButton
            // 
            this.openChartButton.BackColor = System.Drawing.Color.Black;
            this.openChartButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.openChartButton.FlatAppearance.BorderSize = 0;
            this.openChartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openChartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openChartButton.ForeColor = System.Drawing.Color.LightGray;
            this.openChartButton.Location = new System.Drawing.Point(0, 120);
            this.openChartButton.Name = "openChartButton";
            this.openChartButton.Size = new System.Drawing.Size(211, 60);
            this.openChartButton.TabIndex = 25;
            this.openChartButton.Text = "Open chart";
            this.openChartButton.UseVisualStyleBackColor = false;
            this.openChartButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // testingProgrammButton
            // 
            this.testingProgrammButton.BackColor = System.Drawing.Color.Black;
            this.testingProgrammButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.testingProgrammButton.FlatAppearance.BorderSize = 0;
            this.testingProgrammButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.testingProgrammButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.testingProgrammButton.ForeColor = System.Drawing.Color.LightGray;
            this.testingProgrammButton.Location = new System.Drawing.Point(0, 60);
            this.testingProgrammButton.Name = "testingProgrammButton";
            this.testingProgrammButton.Size = new System.Drawing.Size(211, 60);
            this.testingProgrammButton.TabIndex = 26;
            this.testingProgrammButton.Text = "Test program";
            this.testingProgrammButton.UseVisualStyleBackColor = false;
            this.testingProgrammButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // drawingPanel
            // 
            this.drawingPanel.AutoScroll = true;
            this.drawingPanel.Controls.Add(this.changeParametersSubPanel);
            this.drawingPanel.Controls.Add(this.button5);
            this.drawingPanel.Controls.Add(this.drawingSubPanel);
            this.drawingPanel.Controls.Add(this.drawingButton);
            this.drawingPanel.Location = new System.Drawing.Point(0, 0);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(210, 880);
            this.drawingPanel.TabIndex = 27;
            // 
            // changeParametersSubPanel
            // 
            this.changeParametersSubPanel.Controls.Add(this.ChangeEdgeLengthButton);
            this.changeParametersSubPanel.Controls.Add(this.DeleteAllGraphButton);
            this.changeParametersSubPanel.Controls.Add(this.DeleteElementButton);
            this.changeParametersSubPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.changeParametersSubPanel.Location = new System.Drawing.Point(0, 399);
            this.changeParametersSubPanel.Name = "changeParametersSubPanel";
            this.changeParametersSubPanel.Size = new System.Drawing.Size(210, 180);
            this.changeParametersSubPanel.TabIndex = 3;
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Top;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.ForeColor = System.Drawing.Color.Gainsboro;
            this.button5.Location = new System.Drawing.Point(0, 309);
            this.button5.Name = "button5";
            this.button5.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button5.Size = new System.Drawing.Size(210, 90);
            this.button5.TabIndex = 0;
            this.button5.Text = "Change parameters";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // drawingSubPanel
            // 
            this.drawingSubPanel.Controls.Add(this.drawEdgeLabel);
            this.drawingSubPanel.Controls.Add(this.DrawVertexButton);
            this.drawingSubPanel.Controls.Add(this.DrawEdgeButton);
            this.drawingSubPanel.Controls.Add(this.GetRandomGraphButton);
            this.drawingSubPanel.Controls.Add(this.StopDrawingButton);
            this.drawingSubPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.drawingSubPanel.Location = new System.Drawing.Point(0, 69);
            this.drawingSubPanel.Name = "drawingSubPanel";
            this.drawingSubPanel.Size = new System.Drawing.Size(210, 240);
            this.drawingSubPanel.TabIndex = 2;
            // 
            // drawEdgeLabel
            // 
            this.drawEdgeLabel.AutoSize = true;
            this.drawEdgeLabel.BackColor = System.Drawing.Color.SaddleBrown;
            this.drawEdgeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.drawEdgeLabel.ForeColor = System.Drawing.Color.Yellow;
            this.drawEdgeLabel.Location = new System.Drawing.Point(23, 117);
            this.drawEdgeLabel.Name = "drawEdgeLabel";
            this.drawEdgeLabel.Size = new System.Drawing.Size(106, 25);
            this.drawEdgeLabel.TabIndex = 29;
            this.drawEdgeLabel.Text = "Draw edge";
            this.drawEdgeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // drawingButton
            // 
            this.drawingButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.drawingButton.FlatAppearance.BorderSize = 0;
            this.drawingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drawingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.drawingButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.drawingButton.Location = new System.Drawing.Point(0, 0);
            this.drawingButton.Name = "drawingButton";
            this.drawingButton.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.drawingButton.Size = new System.Drawing.Size(210, 69);
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
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.graphInfoButton);
            this.panel1.Controls.Add(this.toolsSubPanel);
            this.panel1.Controls.Add(this.toolsButton);
            this.panel1.Location = new System.Drawing.Point(1430, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 880);
            this.panel1.TabIndex = 28;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button8);
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.button7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 430);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 180);
            this.panel2.TabIndex = 3;
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Top;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button8.ForeColor = System.Drawing.Color.Red;
            this.button8.Location = new System.Drawing.Point(0, 120);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(220, 60);
            this.button8.TabIndex = 6;
            this.button8.Text = "Cyclomatic number";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Dock = System.Windows.Forms.DockStyle.Top;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button6.ForeColor = System.Drawing.Color.Red;
            this.button6.Location = new System.Drawing.Point(0, 60);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(220, 60);
            this.button6.TabIndex = 4;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Dock = System.Windows.Forms.DockStyle.Top;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button7.ForeColor = System.Drawing.Color.Red;
            this.button7.Location = new System.Drawing.Point(0, 0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(220, 60);
            this.button7.TabIndex = 5;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // graphInfoButton
            // 
            this.graphInfoButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.graphInfoButton.FlatAppearance.BorderSize = 0;
            this.graphInfoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.graphInfoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.graphInfoButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.graphInfoButton.Location = new System.Drawing.Point(0, 358);
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
            this.toolsSubPanel.Controls.Add(this.exitButton);
            this.toolsSubPanel.Controls.Add(this.SaveGraphButton);
            this.toolsSubPanel.Controls.Add(this.OpenGraphButton);
            this.toolsSubPanel.Controls.Add(this.ShowOrHideAdjMatrix);
            this.toolsSubPanel.Controls.Add(this.CheckGraphForStrongConnectionButton);
            this.toolsSubPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolsSubPanel.Location = new System.Drawing.Point(0, 58);
            this.toolsSubPanel.Name = "toolsSubPanel";
            this.toolsSubPanel.Size = new System.Drawing.Size(220, 300);
            this.toolsSubPanel.TabIndex = 1;
            // 
            // exitButton
            // 
            this.exitButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exitButton.ForeColor = System.Drawing.Color.Chartreuse;
            this.exitButton.Location = new System.Drawing.Point(0, 240);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(220, 60);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // toolsButton
            // 
            this.toolsButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolsButton.FlatAppearance.BorderSize = 0;
            this.toolsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
            // drawVertexLabel
            // 
            this.drawVertexLabel.AutoSize = true;
            this.drawVertexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.drawVertexLabel.ForeColor = System.Drawing.Color.Yellow;
            this.drawVertexLabel.Location = new System.Drawing.Point(311, 453);
            this.drawVertexLabel.Name = "drawVertexLabel";
            this.drawVertexLabel.Size = new System.Drawing.Size(115, 25);
            this.drawVertexLabel.TabIndex = 29;
            this.drawVertexLabel.Text = "Draw vertex";
            // 
            // deleteElementLabel
            // 
            this.deleteElementLabel.AutoSize = true;
            this.deleteElementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteElementLabel.ForeColor = System.Drawing.Color.Yellow;
            this.deleteElementLabel.Location = new System.Drawing.Point(1009, 266);
            this.deleteElementLabel.Name = "deleteElementLabel";
            this.deleteElementLabel.Size = new System.Drawing.Size(142, 25);
            this.deleteElementLabel.TabIndex = 30;
            this.deleteElementLabel.Text = "Delete element";
            // 
            // changeLengthLabel
            // 
            this.changeLengthLabel.AutoSize = true;
            this.changeLengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.changeLengthLabel.ForeColor = System.Drawing.Color.Yellow;
            this.changeLengthLabel.Location = new System.Drawing.Point(921, 387);
            this.changeLengthLabel.Name = "changeLengthLabel";
            this.changeLengthLabel.Size = new System.Drawing.Size(192, 25);
            this.changeLengthLabel.TabIndex = 31;
            this.changeLengthLabel.Text = "Change edge weight";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panelChart);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Location = new System.Drawing.Point(1213, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(211, 865);
            this.panel3.TabIndex = 33;
            // 
            // panelChart
            // 
            this.panelChart.Controls.Add(this.exitButton2);
            this.panelChart.Controls.Add(this.saveChartButton);
            this.panelChart.Controls.Add(this.openChartButton);
            this.panelChart.Controls.Add(this.testingProgrammButton);
            this.panelChart.Controls.Add(this.StopProcessButton);
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelChart.Location = new System.Drawing.Point(0, 66);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(211, 300);
            this.panelChart.TabIndex = 1;
            // 
            // exitButton2
            // 
            this.exitButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.exitButton2.FlatAppearance.BorderSize = 0;
            this.exitButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exitButton2.ForeColor = System.Drawing.Color.LightGray;
            this.exitButton2.Location = new System.Drawing.Point(0, 240);
            this.exitButton2.Name = "exitButton2";
            this.exitButton2.Size = new System.Drawing.Size(211, 60);
            this.exitButton2.TabIndex = 27;
            this.exitButton2.Text = "Exit";
            this.exitButton2.UseVisualStyleBackColor = true;
            this.exitButton2.Click += new System.EventHandler(this.exitButton2_Click_1);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.button1.Size = new System.Drawing.Size(211, 66);
            this.button1.TabIndex = 0;
            this.button1.Text = "Tools";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(239, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 34;
            this.label1.Text = "weight";
            // 
            // x05
            // 
            this.x05.AutoSize = true;
            this.x05.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.x05.ForeColor = System.Drawing.Color.White;
            this.x05.Location = new System.Drawing.Point(261, 744);
            this.x05.Name = "x05";
            this.x05.Size = new System.Drawing.Size(31, 20);
            this.x05.TabIndex = 35;
            this.x05.Text = "0.5";
            // 
            // x2
            // 
            this.x2.AutoSize = true;
            this.x2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.x2.ForeColor = System.Drawing.Color.White;
            this.x2.Location = new System.Drawing.Point(229, 760);
            this.x2.Name = "x2";
            this.x2.Size = new System.Drawing.Size(18, 20);
            this.x2.TabIndex = 36;
            this.x2.Text = "2";
            // 
            // mathKernel1
            // 
            this.mathKernel1.AutoCloseLink = true;
            this.mathKernel1.CaptureGraphics = true;
            this.mathKernel1.CaptureMessages = true;
            this.mathKernel1.CapturePrint = true;
            this.mathKernel1.GraphicsFormat = "Automatic";
            this.mathKernel1.GraphicsHeight = 0;
            this.mathKernel1.GraphicsResolution = 0;
            this.mathKernel1.GraphicsWidth = 0;
            this.mathKernel1.HandleEvents = true;
            this.mathKernel1.Input = null;
            this.mathKernel1.Link = null;
            this.mathKernel1.LinkArguments = null;
            this.mathKernel1.PageWidth = 0;
            this.mathKernel1.ResultFormat = Wolfram.NETLink.MathKernel.ResultFormatType.OutputForm;
            this.mathKernel1.UseFrontEnd = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1650, 880);
            this.Controls.Add(this.x2);
            this.Controls.Add(this.x05);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.changeLengthLabel);
            this.Controls.Add(this.deleteElementLabel);
            this.Controls.Add(this.drawVertexLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.drawingPanel);
            this.Controls.Add(this.trackBar1);
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
            this.drawingSubPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.field)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.toolsSubPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panelChart.ResumeLayout(false);
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
        public System.Windows.Forms.Button StopProcessButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button saveChartButton;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button openChartButton;
        private System.Windows.Forms.Button testingProgrammButton;
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
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label drawEdgeLabel;
        private System.Windows.Forms.Label drawVertexLabel;
        private System.Windows.Forms.Label deleteElementLabel;
        private System.Windows.Forms.Label changeLengthLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.Button exitButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label x05;
        private System.Windows.Forms.Label x2;
        private Wolfram.NETLink.MathKernel mathKernel1;
    }
}

