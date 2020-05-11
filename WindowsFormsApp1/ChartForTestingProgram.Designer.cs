namespace WindowsFormsApp1
{
    partial class ChartForTestingProgram
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timeLabel = new System.Windows.Forms.Label();
            this.field = new System.Windows.Forms.PictureBox();
            this.timerForPlotting = new System.Windows.Forms.Timer(this.components);
            this.stopTestingButton = new System.Windows.Forms.Button();
            this.coefficientTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.leftMiniButton = new System.Windows.Forms.Button();
            this.rightMiniButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.field)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coefficientTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea3.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea3);
            legend3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            legend3.IsTextAutoFit = false;
            legend3.Name = "Legend1";
            this.chart.Legends.Add(legend3);
            this.chart.Location = new System.Drawing.Point(613, 255);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Amount of points";
            this.chart.Series.Add(series3);
            this.chart.Size = new System.Drawing.Size(701, 452);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.timeLabel.Location = new System.Drawing.Point(700, 740);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(50, 20);
            this.timeLabel.TabIndex = 1;
            this.timeLabel.Text = "Time";
            // 
            // field
            // 
            this.field.Location = new System.Drawing.Point(12, 60);
            this.field.Name = "field";
            this.field.Size = new System.Drawing.Size(605, 647);
            this.field.TabIndex = 2;
            this.field.TabStop = false;
            // 
            // timerForPlotting
            // 
            this.timerForPlotting.Tick += new System.EventHandler(this.TimerForPlotting_Tick);
            // 
            // stopTestingButton
            // 
            this.stopTestingButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.stopTestingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopTestingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stopTestingButton.Location = new System.Drawing.Point(613, 810);
            this.stopTestingButton.Name = "stopTestingButton";
            this.stopTestingButton.Size = new System.Drawing.Size(260, 105);
            this.stopTestingButton.TabIndex = 3;
            this.stopTestingButton.Text = "Остановить испытания";
            this.stopTestingButton.UseVisualStyleBackColor = false;
            this.stopTestingButton.Click += new System.EventHandler(this.StopTestingButton_Click);
            // 
            // coefficientTrackBar
            // 
            this.coefficientTrackBar.Location = new System.Drawing.Point(555, 763);
            this.coefficientTrackBar.Maximum = 19;
            this.coefficientTrackBar.Minimum = 1;
            this.coefficientTrackBar.Name = "coefficientTrackBar";
            this.coefficientTrackBar.Size = new System.Drawing.Size(373, 56);
            this.coefficientTrackBar.TabIndex = 4;
            this.coefficientTrackBar.Value = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(521, 763);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "0.5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(934, 763);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "2.0";
            // 
            // leftMiniButton
            // 
            this.leftMiniButton.Location = new System.Drawing.Point(138, 897);
            this.leftMiniButton.Name = "leftMiniButton";
            this.leftMiniButton.Size = new System.Drawing.Size(75, 23);
            this.leftMiniButton.TabIndex = 8;
            this.leftMiniButton.Text = "<";
            this.leftMiniButton.UseVisualStyleBackColor = true;
            this.leftMiniButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // rightMiniButton
            // 
            this.rightMiniButton.Location = new System.Drawing.Point(280, 876);
            this.rightMiniButton.Name = "rightMiniButton";
            this.rightMiniButton.Size = new System.Drawing.Size(137, 56);
            this.rightMiniButton.TabIndex = 9;
            this.rightMiniButton.Text = ">";
            this.rightMiniButton.UseVisualStyleBackColor = true;
            this.rightMiniButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // rightButton
            // 
            this.rightButton.Location = new System.Drawing.Point(444, 876);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(104, 63);
            this.rightButton.TabIndex = 10;
            this.rightButton.Text = ">>";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            // 
            // leftButton
            // 
            this.leftButton.Location = new System.Drawing.Point(38, 874);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(58, 64);
            this.leftButton.TabIndex = 11;
            this.leftButton.Text = "<<";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.textBox1.Location = new System.Drawing.Point(193, 926);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(81, 22);
            this.textBox1.TabIndex = 12;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(140, 742);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(190, 802);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(296, 790);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "label5";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(194, 810);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 52);
            this.button1.TabIndex = 16;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChartForTestingProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 1045);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.rightMiniButton);
            this.Controls.Add(this.leftMiniButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.coefficientTrackBar);
            this.Controls.Add(this.stopTestingButton);
            this.Controls.Add(this.field);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.chart);
            this.Name = "ChartForTestingProgram";
            this.Text = "Testing Program";
            this.Load += new System.EventHandler(this.ChartForTestingProgram_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ChartForTestingProgram_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.field)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coefficientTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart chart;
        public System.Windows.Forms.Label timeLabel;
        public System.Windows.Forms.PictureBox field;
        public System.Windows.Forms.Timer timerForPlotting;
        public System.Windows.Forms.Button stopTestingButton;
        public System.Windows.Forms.TrackBar coefficientTrackBar;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button leftMiniButton;
        private System.Windows.Forms.Button rightMiniButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
    }
}