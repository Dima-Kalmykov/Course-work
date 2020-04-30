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
            this.field = new System.Windows.Forms.PictureBox();
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
            this.button1 = new System.Windows.Forms.Button();
            this.StopProcessButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.field)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // field
            // 
            this.field.Location = new System.Drawing.Point(154, 5);
            this.field.Name = "field";
            this.field.Size = new System.Drawing.Size(1021, 718);
            this.field.TabIndex = 1;
            this.field.TabStop = false;
            this.field.MouseClick += new System.Windows.Forms.MouseEventHandler(this.field_MouseClick);
            // 
            // DrawVertexButton
            // 
            this.DrawVertexButton.Location = new System.Drawing.Point(12, 43);
            this.DrawVertexButton.Name = "DrawVertexButton";
            this.DrawVertexButton.Size = new System.Drawing.Size(135, 55);
            this.DrawVertexButton.TabIndex = 2;
            this.DrawVertexButton.Text = "Нарисовать вершину";
            this.DrawVertexButton.UseVisualStyleBackColor = true;
            this.DrawVertexButton.Click += new System.EventHandler(this.DrawVertexButton_Click);
            // 
            // DeleteAllGraphButton
            // 
            this.DeleteAllGraphButton.Location = new System.Drawing.Point(1181, 189);
            this.DeleteAllGraphButton.Name = "DeleteAllGraphButton";
            this.DeleteAllGraphButton.Size = new System.Drawing.Size(135, 55);
            this.DeleteAllGraphButton.TabIndex = 4;
            this.DeleteAllGraphButton.Text = "Удалить весь граф";
            this.DeleteAllGraphButton.UseVisualStyleBackColor = true;
            this.DeleteAllGraphButton.Click += new System.EventHandler(this.DeleteAllGraphButton_Click);
            // 
            // DrawEdgeButton
            // 
            this.DrawEdgeButton.Location = new System.Drawing.Point(12, 114);
            this.DrawEdgeButton.Name = "DrawEdgeButton";
            this.DrawEdgeButton.Size = new System.Drawing.Size(135, 55);
            this.DrawEdgeButton.TabIndex = 5;
            this.DrawEdgeButton.Text = "Нарисовать ребро";
            this.DrawEdgeButton.UseVisualStyleBackColor = true;
            this.DrawEdgeButton.Click += new System.EventHandler(this.DrawEdgeButton_Click);
            // 
            // CheckGraphForStrongConnectionButton
            // 
            this.CheckGraphForStrongConnectionButton.Location = new System.Drawing.Point(-3, 502);
            this.CheckGraphForStrongConnectionButton.Name = "CheckGraphForStrongConnectionButton";
            this.CheckGraphForStrongConnectionButton.Size = new System.Drawing.Size(150, 60);
            this.CheckGraphForStrongConnectionButton.TabIndex = 11;
            this.CheckGraphForStrongConnectionButton.Text = "Проверить граф на сильносвязность";
            this.CheckGraphForStrongConnectionButton.UseVisualStyleBackColor = true;
            this.CheckGraphForStrongConnectionButton.Click += new System.EventHandler(this.CheckGraphForStrongConnectionButton_Click);
            // 
            // GetRandomGraphButton
            // 
            this.GetRandomGraphButton.Location = new System.Drawing.Point(12, 189);
            this.GetRandomGraphButton.Name = "GetRandomGraphButton";
            this.GetRandomGraphButton.Size = new System.Drawing.Size(135, 55);
            this.GetRandomGraphButton.TabIndex = 13;
            this.GetRandomGraphButton.Text = "Сгенерировать граф";
            this.GetRandomGraphButton.UseVisualStyleBackColor = true;
            this.GetRandomGraphButton.Click += new System.EventHandler(this.GetRandomGraphButton_Click);
            // 
            // DeleteElementButton
            // 
            this.DeleteElementButton.Location = new System.Drawing.Point(1181, 114);
            this.DeleteElementButton.Name = "DeleteElementButton";
            this.DeleteElementButton.Size = new System.Drawing.Size(135, 55);
            this.DeleteElementButton.TabIndex = 14;
            this.DeleteElementButton.Text = "Удалить элемент";
            this.DeleteElementButton.UseVisualStyleBackColor = true;
            this.DeleteElementButton.Click += new System.EventHandler(this.DeleteElementButton_Click);
            // 
            // SaveGraphButton
            // 
            this.SaveGraphButton.Location = new System.Drawing.Point(1181, 431);
            this.SaveGraphButton.Name = "SaveGraphButton";
            this.SaveGraphButton.Size = new System.Drawing.Size(135, 55);
            this.SaveGraphButton.TabIndex = 15;
            this.SaveGraphButton.Text = "Сохранить изображение";
            this.SaveGraphButton.UseVisualStyleBackColor = true;
            this.SaveGraphButton.Click += new System.EventHandler(this.SaveGraphButton_Click);
            // 
            // OpenGraphButton
            // 
            this.OpenGraphButton.Location = new System.Drawing.Point(1181, 502);
            this.OpenGraphButton.Name = "OpenGraphButton";
            this.OpenGraphButton.Size = new System.Drawing.Size(135, 55);
            this.OpenGraphButton.TabIndex = 16;
            this.OpenGraphButton.Text = "Открыть файл";
            this.OpenGraphButton.UseVisualStyleBackColor = true;
            this.OpenGraphButton.Click += new System.EventHandler(this.OpenGraphButton_Click);
            // 
            // ChangeEdgeLengthButton
            // 
            this.ChangeEdgeLengthButton.Location = new System.Drawing.Point(1181, 43);
            this.ChangeEdgeLengthButton.Name = "ChangeEdgeLengthButton";
            this.ChangeEdgeLengthButton.Size = new System.Drawing.Size(135, 55);
            this.ChangeEdgeLengthButton.TabIndex = 17;
            this.ChangeEdgeLengthButton.Text = "Изменить длину ребра";
            this.ChangeEdgeLengthButton.UseVisualStyleBackColor = true;
            this.ChangeEdgeLengthButton.Click += new System.EventHandler(this.ChangeLengthButton_Click);
            // 
            // StopDrawingButton
            // 
            this.StopDrawingButton.Location = new System.Drawing.Point(12, 260);
            this.StopDrawingButton.Name = "StopDrawingButton";
            this.StopDrawingButton.Size = new System.Drawing.Size(135, 55);
            this.StopDrawingButton.TabIndex = 18;
            this.StopDrawingButton.Text = "Прекратить рисование";
            this.StopDrawingButton.UseVisualStyleBackColor = true;
            this.StopDrawingButton.Click += new System.EventHandler(this.StopDrawingButton_Click);
            // 
            // ShowOrHideAdjMatrix
            // 
            this.ShowOrHideAdjMatrix.Enabled = false;
            this.ShowOrHideAdjMatrix.Location = new System.Drawing.Point(-2, 431);
            this.ShowOrHideAdjMatrix.Name = "ShowOrHideAdjMatrix";
            this.ShowOrHideAdjMatrix.Size = new System.Drawing.Size(150, 60);
            this.ShowOrHideAdjMatrix.TabIndex = 19;
            this.ShowOrHideAdjMatrix.Text = "Закрыть матрицу";
            this.ShowOrHideAdjMatrix.UseVisualStyleBackColor = true;
            this.ShowOrHideAdjMatrix.Click += new System.EventHandler(this.ShowOrHideAdjMatrix_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1194, 714);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 92);
            this.button1.TabIndex = 20;
            this.button1.Text = "Продолжить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // StopProcessButton
            // 
            this.StopProcessButton.Location = new System.Drawing.Point(1095, 729);
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
            this.trackBar1.Location = new System.Drawing.Point(377, 777);
            this.trackBar1.Maximum = 19;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(330, 56);
            this.trackBar1.TabIndex = 23;
            this.trackBar1.Value = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(29, 607);
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
            this.button3.Location = new System.Drawing.Point(19, 361);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(107, 46);
            this.button3.TabIndex = 25;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1341, 874);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.StopProcessButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ShowOrHideAdjMatrix);
            this.Controls.Add(this.StopDrawingButton);
            this.Controls.Add(this.ChangeEdgeLengthButton);
            this.Controls.Add(this.OpenGraphButton);
            this.Controls.Add(this.SaveGraphButton);
            this.Controls.Add(this.DeleteElementButton);
            this.Controls.Add(this.GetRandomGraphButton);
            this.Controls.Add(this.CheckGraphForStrongConnectionButton);
            this.Controls.Add(this.DrawEdgeButton);
            this.Controls.Add(this.DeleteAllGraphButton);
            this.Controls.Add(this.DrawVertexButton);
            this.Controls.Add(this.field);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DrawingGraph";
            this.Click += new System.EventHandler(this.MainForm_Click);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.field)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox field;
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button StopProcessButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button button3;
    }
}

