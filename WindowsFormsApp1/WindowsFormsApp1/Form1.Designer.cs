namespace WindowsFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1232, 509);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnShowInfo;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.ListBox lstFolders;
        private System.Windows.Forms.DataGridView dataGridFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModification;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCalcResult;
        private System.Windows.Forms.Button btnProcessFiles;
        private System.Windows.Forms.Button btnDuplicate; // Объявление кнопки для дублирования файла
    }
}
