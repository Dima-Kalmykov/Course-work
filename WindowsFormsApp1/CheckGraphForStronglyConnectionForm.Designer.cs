namespace WindowsFormsApp1
{
    partial class CheckGraphForStronglyConnectionForm
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
            this.Caption = new System.Windows.Forms.Label();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.GenerateGraphButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Caption
            // 
            this.Caption.AutoSize = true;
            this.Caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Caption.ForeColor = System.Drawing.Color.Gainsboro;
            this.Caption.Location = new System.Drawing.Point(60, 81);
            this.Caption.Name = "Caption";
            this.Caption.Size = new System.Drawing.Size(274, 25);
            this.Caption.TabIndex = 0;
            this.Caption.Text = "Select the following actions";
            // 
            // ContinueButton
            // 
            this.ContinueButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ContinueButton.FlatAppearance.BorderSize = 0;
            this.ContinueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContinueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ContinueButton.ForeColor = System.Drawing.Color.LightGray;
            this.ContinueButton.Location = new System.Drawing.Point(31, 142);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(190, 75);
            this.ContinueButton.TabIndex = 1;
            this.ContinueButton.Text = "Continue editing";
            this.ContinueButton.UseVisualStyleBackColor = false;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // GenerateGraphButton
            // 
            this.GenerateGraphButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GenerateGraphButton.FlatAppearance.BorderSize = 0;
            this.GenerateGraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateGraphButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GenerateGraphButton.ForeColor = System.Drawing.Color.LightGray;
            this.GenerateGraphButton.Location = new System.Drawing.Point(227, 142);
            this.GenerateGraphButton.Name = "GenerateGraphButton";
            this.GenerateGraphButton.Size = new System.Drawing.Size(190, 75);
            this.GenerateGraphButton.TabIndex = 2;
            this.GenerateGraphButton.Text = "Generate strongly directed graph";
            this.GenerateGraphButton.UseVisualStyleBackColor = false;
            this.GenerateGraphButton.Click += new System.EventHandler(this.GenerateGraphButton_Click);
            // 
            // CheckGraphForStronglyConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 261);
            this.Controls.Add(this.GenerateGraphButton);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.Caption);
            this.Name = "CheckGraphForStronglyConnectionForm";
            this.Text = "Check graph";
            this.Load += new System.EventHandler(this.CheckGraphForStronglyConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Caption;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Button GenerateGraphButton;
    }
}