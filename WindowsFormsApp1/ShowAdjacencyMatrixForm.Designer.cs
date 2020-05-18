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
            this.AdjacencyMatrixListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // AdjacencyMatrixListBox
            // 
            this.AdjacencyMatrixListBox.BackColor = System.Drawing.Color.Black;
            this.AdjacencyMatrixListBox.FormattingEnabled = true;
            this.AdjacencyMatrixListBox.HorizontalScrollbar = true;
            this.AdjacencyMatrixListBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.AdjacencyMatrixListBox.ItemHeight = 16;
            this.AdjacencyMatrixListBox.Location = new System.Drawing.Point(10, 7);
            this.AdjacencyMatrixListBox.Name = "AdjacencyMatrixListBox";
            this.AdjacencyMatrixListBox.Size = new System.Drawing.Size(1329, 692);
            this.AdjacencyMatrixListBox.TabIndex = 0;
            this.AdjacencyMatrixListBox.Click += new System.EventHandler(this.AdjacencyMatrixListBoxClick);
            // 
            // ShowAdjacencyMatrixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 708);
            this.Controls.Add(this.AdjacencyMatrixListBox);
            this.Name = "ShowAdjacencyMatrixForm";
            this.Text = "AdjacencyMatrix";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListBox AdjacencyMatrixListBox;
    }
}