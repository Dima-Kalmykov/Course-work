namespace WindowsFormsApp1
{
    partial class ChooseEdgeForm
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
            this.FirstOptionButton = new System.Windows.Forms.Button();
            this.SecondOptionButton = new System.Windows.Forms.Button();
            this.TextForUnderstandingLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FirstOptionButton
            // 
            this.FirstOptionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FirstOptionButton.FlatAppearance.BorderSize = 0;
            this.FirstOptionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FirstOptionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FirstOptionButton.ForeColor = System.Drawing.Color.Aqua;
            this.FirstOptionButton.Location = new System.Drawing.Point(70, 145);
            this.FirstOptionButton.Name = "FirstOptionButton";
            this.FirstOptionButton.Size = new System.Drawing.Size(250, 55);
            this.FirstOptionButton.TabIndex = 0;
            this.FirstOptionButton.UseVisualStyleBackColor = false;
            this.FirstOptionButton.Click += new System.EventHandler(this.FirstActionButton_Click);
            // 
            // SecondOptionButton
            // 
            this.SecondOptionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SecondOptionButton.FlatAppearance.BorderSize = 0;
            this.SecondOptionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SecondOptionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SecondOptionButton.ForeColor = System.Drawing.Color.Aqua;
            this.SecondOptionButton.Location = new System.Drawing.Point(70, 215);
            this.SecondOptionButton.Name = "SecondOptionButton";
            this.SecondOptionButton.Size = new System.Drawing.Size(250, 55);
            this.SecondOptionButton.TabIndex = 1;
            this.SecondOptionButton.UseVisualStyleBackColor = false;
            this.SecondOptionButton.Click += new System.EventHandler(this.SecondActionButton_Click);
            // 
            // TextForUnderstandingLabel
            // 
            this.TextForUnderstandingLabel.AutoSize = true;
            this.TextForUnderstandingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextForUnderstandingLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.TextForUnderstandingLabel.Location = new System.Drawing.Point(9, 90);
            this.TextForUnderstandingLabel.Name = "TextForUnderstandingLabel";
            this.TextForUnderstandingLabel.Size = new System.Drawing.Size(439, 25);
            this.TextForUnderstandingLabel.TabIndex = 2;
            this.TextForUnderstandingLabel.Text = "Выберите путь, который хотите удалить:";
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CancelButton.FlatAppearance.BorderSize = 0;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelButton.ForeColor = System.Drawing.Color.Aqua;
            this.CancelButton.Location = new System.Drawing.Point(70, 285);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(250, 55);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ChooseEdgeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 379);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.TextForUnderstandingLabel);
            this.Controls.Add(this.SecondOptionButton);
            this.Controls.Add(this.FirstOptionButton);
            this.Name = "ChooseEdgeForm";
            this.Text = "Choose edge";
            this.Load += new System.EventHandler(this.ChooseEdgeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button FirstOptionButton;
        internal System.Windows.Forms.Button SecondOptionButton;
        internal System.Windows.Forms.Label TextForUnderstandingLabel;
        private System.Windows.Forms.Button CancelButton;
    }
}