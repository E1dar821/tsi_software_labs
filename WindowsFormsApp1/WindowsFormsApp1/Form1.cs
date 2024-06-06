using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ташболотов Эльдар SEST 1-22");
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    txtFolderPath.Text = folderBrowserDialog.SelectedPath;
                    string selectedPath = folderBrowserDialog.SelectedPath;
                    string[] directories = Directory.GetDirectories(selectedPath);

                    // Clear ListBox before adding new items
                    lstFolders.Items.Clear();

                    // Add directory names to ListBox
                    foreach (string directory in directories)
                    {
                        lstFolders.Items.Add(Path.GetFileName(directory));
                    }

                    string[] files = Directory.GetFiles(selectedPath);
                    foreach (string filePath in files)
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        dataGridFiles.Rows.Add(fileInfo.Name, fileInfo.Length, fileInfo.LastWriteTime, "");
                    }
                }
            }
        }

        private void lstFolders_DoubleClick(object sender, EventArgs e)
        {
            if (lstFolders.SelectedItem != null)
            {
                string selectedFolder = Path.Combine(txtFolderPath.Text, lstFolders.SelectedItem.ToString());

                Form2 form = new Form2(selectedFolder);
                form.ShowDialog();
            }
        }

        private void DuplicateButtonClick(object sender, EventArgs e)
        {
            if (dataGridFiles.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridFiles.SelectedRows[0];
                string originalFilePath = selectedRow.Cells[0].Value.ToString();
                string directoryPath = Path.GetDirectoryName(originalFilePath);
                string baseFileName = Path.GetFileNameWithoutExtension(originalFilePath);
                string extension = Path.GetExtension(originalFilePath);

                string duplicateFilePath = Path.Combine(directoryPath, baseFileName + "_copy" + extension);

                try
                {
                    File.Copy(originalFilePath, duplicateFilePath, true);
                    MessageBox.Show($"Файл '{originalFilePath}' успешно дублирован как '{duplicateFilePath}'");
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show($"Ошибка при копировании файла: {ioEx.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите файл для дублирования.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Button duplicateButton = new Button
            {
                Text = "Дублировать файл",
                Location = new Point(10, 300),
                Size = new Size(200, 30)
            };
            duplicateButton.Click += DuplicateButtonClick;
            this.Controls.Add(duplicateButton);
        }

        private void dataGridFiles_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridFiles.SelectedRows.Count > 0)
            {
                // Изменяем MessageBox на вариант с двумя кнопками: Да и Нет
                DialogResult result = MessageBox.Show("Продублировать файл?", "Дублирование файла", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = dataGridFiles.SelectedRows[0];

                    // Проверяем, не равно ли значение ячейки null, прежде чем преобразовывать его в строку
                    if (selectedRow.Cells[0]?.Value != null)
                    {
                        string originalFilePath = selectedRow.Cells[0].Value.ToString();
                        string directoryPath = Path.GetDirectoryName(originalFilePath);
                        string baseFileName = Path.GetFileNameWithoutExtension(originalFilePath);
                        string extension = Path.GetExtension(originalFilePath);

                        string duplicateFilePath = Path.Combine(directoryPath, baseFileName + "_copy" + extension);

                        try
                        {
                            File.Copy(originalFilePath, duplicateFilePath, true);
                            MessageBox.Show($"Файл '{originalFilePath}' успешно дублирован как '{duplicateFilePath}'");

                            // Добавляем новую строку в DataGridView для отображения дублированного файла
                            DataGridViewRow newRow = new DataGridViewRow();
                            newRow.CreateCells(dataGridFiles); // Создаем ячейки для новой строки
                            newRow.Cells[0].Value = Path.GetFileName(duplicateFilePath); // Устанавливаем имя файла
                            newRow.Cells[1].Value = new FileInfo(duplicateFilePath).Length; // Размер файла
                            newRow.Cells[2].Value = File.GetLastWriteTime(duplicateFilePath); // Дата последнего изменения файла
                            newRow.Cells[3].Value = ""; // Значение для четвертого столбца, если оно необходимо

                            dataGridFiles.Rows.Add(newRow); // Добавляем новую строку в DataGridView
                        }
                        catch (IOException ioEx)
                        {
                            MessageBox.Show($"Ошибка при копировании файла: {ioEx.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не выбран файл для дублирования.");
                    }
                }
            }
        }

        private void dataGridFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Проверяем, была ли нажата ячейка внутри таблицы
            {
                DataGridViewRow row = this.dataGridFiles.Rows[e.RowIndex];
                string fileName = row.Cells["colFileName"].Value.ToString();
                MessageBox.Show($"Вы кликнули по файлу: {fileName}");
            }
        }

        private async void btnProcessFiles_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridFiles.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    Random randomGenerator = new Random(); // Инициализация нового экземпляра Random здесь
                    int delaySeconds = randomGenerator.Next(1, dataGridFiles.Rows.Count + 1);

                    await ProcessFileAsync(randomGenerator, delaySeconds, row); // Передаем экземпляр Random в метод
                }
            }
        }
        
        private async Task ProcessFileAsync(Random randomGenerator, int delaySeconds, DataGridViewRow row)
        {
            await Task.Delay(delaySeconds * 1000);
            row.Cells[3].Value = randomGenerator.Next(1, 100);
        }
    }
}
    