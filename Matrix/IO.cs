using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public static class MatrixIO
{
  public static async Task WriteMatrixAsync(Stream stream, double[,] matrix, string separator = ", ")
  {
      using var writer = new StreamWriter(stream);
      await writer.WriteLineAsync($"{matrix.GetLength(0)}x{matrix.GetLength(1)}");
      foreach (var row in matrix)
      {
          await writer.WriteAsync(string.Join(separator, row));
          await writer.WriteLineAsync();
      }
  }

  public static async Task<double[,]> ReadMatrixAsync(Stream stream, string separator = ", ")
  {
      using var reader = new StreamReader(stream);
      var size = int.Parse(await reader.ReadLine());
      var matrix = new double[size, size];
      for (int i = 0; i < size; i++)
      {
          var row = await reader.ReadLine();
          var values = row.Split(separator);
          for (int j = 0; j < size; j++)
          {
              matrix[i, j] = double.Parse(values[j]);
          }
      }
      return matrix;
    }

    public static void WriteBinary(Matrix matrix, Stream stream)
    {
        using var writer = new BinaryWriter(stream);
        writer.Write(matrix.Rows);
        writer.Write(matrix.Columns);
        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Columns; j++)
            {
                writer.Write(matrix[i, j]);
            }
        }
    }

    public static double[,] ReadBinary(Stream stream)
    {
        using var reader = new BinaryReader(stream);
        var rows = reader.ReadInt32();
        var columns = reader.ReadInt32();
        var matrix = new double[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrix[i, j] = reader.ReadDouble();
            }
        }
        return matrix;
    }

    public static async Task WriteJsonAsync(Matrix matrix, Stream stream)
    {
        var json = JsonSerializer.Serialize(matrix.Values);
        await stream.WriteAsync(Encoding.UTF8.GetBytes(json));
    }

    public static async Task<double[,]> ReadJsonAsync(Stream stream)
    {
        var json = await new StreamReader(stream).ReadToEndAsync();
        var matrixData = JsonSerializer.Deserialize<double[][]>(json);
        var matrix = new double[matrixData.Length, matrixData[0].Length];
        for (int i = 0; i < matrixData.Length; i++)
        {
            for (int j = 0; j < matrixData[i].Length; j++)
            {
                matrix[i, j] = matrixData[i][j];
            }
        }
        return matrix;
    }

    public static void WriteToFile(string directory, string fileName, Matrix matrix, Action<Matrix, Stream> writeAction)
    {
        using var fileStream = File.Open(Path.Combine(directory, fileName), FileMode.Create);
        writeAction(matrix, fileStream);
    }

    public static async Task WriteToFileAsync(string directory, string fileName, Matrix matrix, Func<Matrix, Stream, Task> writeActionAsync)
    {
        using var fileStream = File.Open(Path.Combine(directory, fileName), FileMode.Create);
        await writeActionAsync(matrix, fileStream);
    }

    public static void ReadFromFile(string filePath, Func<Stream, Matrix> readAction)
    {
        using var fileStream = File.Open(filePath, FileMode.Open);
        var matrix = readAction(fileStream);
    }

    public static async Task ReadFromFileAsync(string filePath, Func<Stream, Task<Matrix>> readActionAsync)
    {
        using var fileStream = File.Open(filePath, FileMode.Open);
        var matrix = await readActionAsync(fileStream);
    }

    






  
}