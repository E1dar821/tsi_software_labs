using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static Random random = new Random();

    static void Main()
    {
        CreateRandomMatrix(3, 3);
        MultiplyMatrixArrays(new double[][] { /* первый массив */ }, new double[][] { /* второй массив */ });
        ScalarProductOfMatrixArrays(new double[][] { /* первый массив */ }, new double[][] { /* второй массив */ });
        WriteMatrixArrayToDirectory(/* массив матриц */, /* директория */, /* префикс имени файла */, /* расширение файла */, /* способ записи */);
        ReadMatrixArrayFromDirectory(/* префикс имени файла */, /* расширение файла */, /* способ чтения */);
        CompareMatrixArraysOnEquality(/* первый массив */, /* второй массив */);
    }

    static Matrix CreateRandomMatrix(int rows, int columns)
    {
        var matrix = new Matrix(rows, columns);
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                matrix[i, j] = random.NextDouble() * 10;
        return matrix;
    }

    static void MultiplyMatrixArrays(double[][] array1, double[][] array2)
    {
        if (array1.Select(x => x.Length).Distinct().Count()!= 1 || array2.Select(x => x.Length).Distinct().Count()!= 1)
            throw new ArgumentException("All arrays must have the same length.");
        var result = new double[array1[0].Length, array2[0].Length];
        for (int i = 0; i < array1[0].Length; i++)
            for (int j = 0; j < array2[0].Length; j++)
                for (int k = 0; k < array1.Length; k++)
                    result[i, j] += array1[k][i] * array2[k][j];
        Console.WriteLine($"Resultant matrix:\n{result}");
    }

    static double ScalarProductOfMatrixArrays(double[][] array1, double[][] array2)
    {
        if (array1.Select(x => x.Length).Distinct().Count()!= 1 || array2.Select(x => x.Length).Distinct().Count()!= 1)
            throw new ArgumentException("All arrays must have the same length.");
        double product = 0;
        for (int i = 0; i < array1[0].Length; i++)
            for (int j = 0; j < array2[0].Length; j++)
                for (int k = 0; k < array1.Length; k++)
                    product += array1[k][i] * array2[k][j];
        return product;
    }

    static void WriteMatrixArrayToDirectory(double[][] matrixArray, string directory, string prefix, string extension, Action<Matrix, Stream> writeAction)
    {
        int index = 0;
        foreach (var matrix in matrixArray)
        {
            string fileName = $"{prefix}{index++}.{extension}";
            string fullPath = Path.Combine(directory, fileName);
            using var fileStream = File.Open(fullPath, FileMode.Create);
            writeAction(new Matrix(matrix), fileStream);
            if (index % 10 == 0)
                Console.WriteLine($"Written matrix {index} to {fileName}");
        }
    }

    static void ReadMatrixArrayFromDirectory(string prefix, string extension, Func<Stream, Matrix> readAction)
    {
        string[] files = Directory.GetFiles(".", "*." + extension);
        var matrixArray = new Matrix[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            string fileName = files[i];
            if (!fileName.StartsWith(prefix))
                continue;
            using var fileStream = File.Open(fileName, FileMode.Open);
            matrixArray[i] = readAction(fileStream);
        }
    }

    static bool CompareMatrixArraysOnEquality(double[][] array1, double[][] array2)
    {
        if (array1.Select(x => x.Length).Distinct().Count()!= 1 || array2.Select(x => x.Length).Distinct().Count()!= 1)
            throw new ArgumentException("All arrays must have the same length.");
        return array1.SequenceEqual(array2, Matrix.Equals);
    }
}
