namespace WindowsFormsApp1
{
    partial class ShowAdjacencyMatrixForm
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
            this.AdjMatrixListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // AdjMatrixListBox
            // 
            this.AdjMatrixListBox.FormattingEnabled = true;
            this.AdjMatrixListBox.HorizontalScrollbar = true;
            this.AdjMatrixListBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.AdjMatrixListBox.ItemHeight = 16;
            this.AdjMatrixListBox.Location = new System.Drawing.Point(10, 7);
            this.AdjMatrixListBox.Name = "AdjMatrixListBox";
            this.AdjMatrixListBox.Size = new System.Drawing.Size(1329, 692);
            this.AdjMatrixListBox.TabIndex = 0;
            this.AdjMatrixListBox.Click += new System.EventHandler(this.AdjMatrixListBox_Click);
            // 
            // ShowAdjacencyMatrixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 708);
            this.Controls.Add(this.AdjMatrixListBox);
            this.Name = "ShowAdjacencyMatrixForm";
            this.Text = "AdjacencyMatrix";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListBox AdjMatrixListBox;
    }
}