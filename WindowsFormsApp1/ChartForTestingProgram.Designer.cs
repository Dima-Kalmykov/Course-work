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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timeLabel = new System.Windows.Forms.Label();
            this.field = new System.Windows.Forms.PictureBox();
            this.timerForPlotting = new System.Windows.Forms.Timer(this.components);
            this.stopTestingButton = new System.Windows.Forms.Button();
            this.coefficientTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.field)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coefficientTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea2.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart.Legends.Add(legend2);
            this.chart.Location = new System.Drawing.Point(613, 255);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Amount of points";
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(701, 452);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
            this.label1.Location = new System.Drawing.Point(521, 763);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "0.5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(934, 763);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "2.0";
            // 
            // ChartForTestingProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 1045);
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
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.field)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coefficientTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.PictureBox field;
        private System.Windows.Forms.Timer timerForPlotting;
        private System.Windows.Forms.Button stopTestingButton;
        private System.Windows.Forms.TrackBar coefficientTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}