using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2(string folderPath)
        {
            InitializeComponent();
            DisplayFolderInfo(folderPath);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            return;        
        }

        private void DisplayFolderInfo(string folderPath)
        {
            Text = "Информация о папке";
            string folderName = Path.GetFileName(folderPath);
            string lastModified = Directory.GetLastWriteTime(folderPath).ToString();

            nameLabel.Text = "Название: " + folderName;
            lastModifiedLabel.Text = "Дата последней модификации: " + lastModified;
        }
    }
}
