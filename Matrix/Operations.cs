using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class MatrixOperations
{
  public static double[,] Transpose(double[,] matrix)
  {
      var transposed = new double[matrix.GetLength(1), matrix.GetLength(0)];
      for (int i = 0; i < matrix.GetLength(0); i++)
          for (int j = 0; j < matrix.GetLength(1); j++)
              transposed[j, i] = matrix[i, j];
      return transposed;
  }

  public static double[,] MultiplyByScalar(double[,] matrix, double scalar)
  {
      for (int i = 0; i < matrix.GetLength(0); i++)
          for (int j = 0; j < matrix.GetLength(1); j++)
              matrix[i, j] *= scalar;
      return matrix;
  }
  public static double[,] Add(double[,] matrix1, double[,] matrix2)
  {
      if (matrix1.GetLength(0)!= matrix2.GetLength(0) || matrix1.GetLength(1)!= matrix2.GetLength(1))
          throw new ArgumentException("Matrices dimensions must match.");

      var result = new double[matrix1.GetLength(0), matrix1.GetLength(1)];
      for (int i = 0; i < matrix1.GetLength(0); i++)
          for (int j = 0; j < matrix1.GetLength(1); j++)
              result[i, j] = matrix1[i, j] + matrix2[i, j];
      return result;
  }
  public static double[,] Subtract(double[,] matrix1, double[,] matrix2)
  {
      if (matrix1.GetLength(0)!= matrix2.GetLength(0) || matrix1.GetLength(1)!= matrix2.GetLength(1))
          throw new ArgumentException("Matrices dimensions must match.");

      var result = new double[matrix1.GetLength(0), matrix1.GetLength(1)];
      for (int i = 0; i < matrix1.GetLength(0); i++)
          for (int j = 0; j < matrix1.GetLength(1); j++)
              result[i, j] = matrix1[i, j] - matrix2[i, j];
      return result;
  }

  public static double[,] Multiply(double[,] matrix1, double[,] matrix2)
  {
      if (matrix1.GetLength(1)!= matrix2.GetLength(0))
          throw new ArgumentException("Number of columns in the first matrix must equal the number of rows in the second matrix.");

      var result = new double[matrix1.GetLength(0), matrix2.GetLength(1)];
      for (int i = 0; i < matrix1.GetLength(0); i++)
          for (int j = 0; j < matrix2.GetLength(1); j++)
              for (int k = 0; k < matrix1.GetLength(1); k++)
                  result[i, j] += matrix1[i, k] * matrix2[k, j];
      return result;
  }






  
}