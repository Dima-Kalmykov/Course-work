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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timeLabel = new System.Windows.Forms.Label();
            this.field = new System.Windows.Forms.PictureBox();
            this.timerForPlotting = new System.Windows.Forms.Timer(this.components);
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
            this.exitButton = new System.Windows.Forms.Button();
            this.stopTestingButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.field)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coefficientTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(613, 255);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Amount of points";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(701, 452);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeLabel.ForeColor = System.Drawing.Color.Yellow;
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
            // coefficientTrackBar
            // 
            this.coefficientTrackBar.BackColor = System.Drawing.Color.Black;
            this.coefficientTrackBar.Location = new System.Drawing.Point(611, 763);
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(521, 763);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "0.5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(934, 763);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "2.0";
            // 
            // leftMiniButton
            // 
            this.leftMiniButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.leftMiniButton.FlatAppearance.BorderSize = 0;
            this.leftMiniButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leftMiniButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leftMiniButton.ForeColor = System.Drawing.Color.Aqua;
            this.leftMiniButton.Location = new System.Drawing.Point(902, 789);
            this.leftMiniButton.Name = "leftMiniButton";
            this.leftMiniButton.Size = new System.Drawing.Size(65, 45);
            this.leftMiniButton.TabIndex = 8;
            this.leftMiniButton.Text = "<";
            this.leftMiniButton.UseVisualStyleBackColor = false;
            this.leftMiniButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // rightMiniButton
            // 
            this.rightMiniButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rightMiniButton.FlatAppearance.BorderSize = 0;
            this.rightMiniButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rightMiniButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rightMiniButton.ForeColor = System.Drawing.Color.Aqua;
            this.rightMiniButton.Location = new System.Drawing.Point(1069, 789);
            this.rightMiniButton.Name = "rightMiniButton";
            this.rightMiniButton.Size = new System.Drawing.Size(65, 45);
            this.rightMiniButton.TabIndex = 9;
            this.rightMiniButton.Text = ">";
            this.rightMiniButton.UseVisualStyleBackColor = false;
            this.rightMiniButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // rightButton
            // 
            this.rightButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rightButton.FlatAppearance.BorderSize = 0;
            this.rightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rightButton.ForeColor = System.Drawing.Color.Aqua;
            this.rightButton.Location = new System.Drawing.Point(1140, 789);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(65, 45);
            this.rightButton.TabIndex = 10;
            this.rightButton.Text = ">>";
            this.rightButton.UseVisualStyleBackColor = false;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            // 
            // leftButton
            // 
            this.leftButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.leftButton.FlatAppearance.BorderSize = 0;
            this.leftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leftButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leftButton.ForeColor = System.Drawing.Color.Aqua;
            this.leftButton.Location = new System.Drawing.Point(831, 789);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(65, 45);
            this.leftButton.TabIndex = 11;
            this.leftButton.Text = "<<";
            this.leftButton.UseVisualStyleBackColor = false;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.textBox1.Location = new System.Drawing.Point(977, 797);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(81, 32);
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
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exitButton.ForeColor = System.Drawing.Color.Aqua;
            this.exitButton.Location = new System.Drawing.Point(941, 857);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(147, 52);
            this.exitButton.TabIndex = 16;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopTestingButton
            // 
            this.stopTestingButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.stopTestingButton.FlatAppearance.BorderSize = 0;
            this.stopTestingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopTestingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stopTestingButton.ForeColor = System.Drawing.Color.Aqua;
            this.stopTestingButton.Location = new System.Drawing.Point(593, 823);
            this.stopTestingButton.Name = "stopTestingButton";
            this.stopTestingButton.Size = new System.Drawing.Size(260, 105);
            this.stopTestingButton.TabIndex = 3;
            this.stopTestingButton.Text = "Stop the test";
            this.stopTestingButton.UseVisualStyleBackColor = false;
            this.stopTestingButton.Click += new System.EventHandler(this.StopTestingButton_Click);
            // 
            // ChartForTestingProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 1045);
            this.Controls.Add(this.exitButton);
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
            this.ForeColor = System.Drawing.Color.LightGray;
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
        private System.Windows.Forms.Button exitButton;
    }
}