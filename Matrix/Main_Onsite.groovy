using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Шаг 1: Создание матриц
        int[,] matrix1 = new int[3, 3];
        int[,] matrix2 = new int[3, 3];

        // Заполняем матрицы случайными числами
        Random rand = new Random();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                matrix1[i, j] = rand.Next(1, 10);
                matrix2[i, j] = rand.Next(1, 10);
            }
        }

        // Шаг 2: Сохранение матриц в файлы
        string directoryPath = @"results";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        await SaveMatrixToFileAsync(matrix1, Path.Combine(directoryPath, "matrix1.txt"), "; ");
        await SaveMatrixToFileAsync(matrix2, Path.Combine(directoryPath, "matrix2.txt"), "; ");

        // Шаг 3: Чтение из файлов и сравнение
        int[,] readMatrix1 = new int[3, 3];
        int[,] readMatrix2 = new int[3, 3];

        await ReadAndCompareMatricesAsync(directoryPath, "matrix1.txt", ref readMatrix1);
        await ReadAndCompareMatricesAsync(directoryPath, "matrix2.txt", ref readMatrix2);

        bool isEqual = matrix1.SequenceEqual(readMatrix1) && matrix2.SequenceEqual(readMatrix2);
        Console.WriteLine($"Матрицы равны: {isEqual}");

        // Шаг 4: Завершение задачи
        Console.WriteLine("Задача выполнена.");
    }

    static async Task SaveMatrixToFileAsync(int[,] matrix, string filePath, string separator)
    {
        await Task.Run(() =>
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        sw.Write(matrix[i, j]);
                        if (j!= matrix.GetLength(1) - 1) sw.Write(separator);
                    }
                    sw.WriteLine();
                }
            }
        });
    }

    static async Task ReadAndCompareMatricesAsync(string directoryPath, string fileName, ref int[,] matrix)
    {
        await Task.Run(() =>
        {
            using (StreamReader sr = new StreamReader(Path.Combine(directoryPath, fileName)))
            {
                string line;
                int rowIndex = 0;
                while ((line = sr.ReadLine())!= null)
                {
                    string[] values = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                    {
                        matrix[rowIndex, colIndex] = int.Parse(values[colIndex]);
                    }
                    rowIndex++;
                }
            }
        });
    }
}
