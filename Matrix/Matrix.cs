using System;
public class Matrix
{
  private readonly double[,] values;
  private int rows;
  private int columns;

  public Matrix(int r, int c)
  {
      if (r <= 0 || c <= 0)
          throw new ArgumentException("Rows and columns must be positive.");
      rows = r;
      columns = c;
      values = new double[r, c];
  }

  public Matrix(double[,] initialValues)
  {
    if (initialValues == null)
        throw new ArgumentNullException(nameof(initialValues));
    rows = initialValues.GetLength(0);
    columns = initialValues.GetLength(1);
    values = initialValues;
  }

  public static Matrix Zero(int r, int c)
  {
      return new Matrix(r, c);
  }

  public static Matrix Zero(int n)
  {
      return new Matrix(n, n);
  }

  public static Matrix Identity(int n)
  {
      var matrix = new Matrix(n, n);
      for (int i = 0; i < n; i++)
          matrix[i, i] = 1;
      return matrix;
  }

  public double this[int i, int j]
  {
      get => values[i, j];
      set => throw new InvalidOperationException("Cannot change value through indexer.");
  }

  public int Rows => rows;
  public int Columns => columns;

public static Matrix operator ~(Matrix m)
{
        var transposed = new Matrix(m.Columns, m.Rows);
        for (int i = 0; i < m.Rows; i++)
            for (int j = 0; j < m.Columns; j++)
                 transposed[j, i] = m[i, j];
        return transposed;
}

public static Matrix operator *(Matrix m, double scalar)
    {
        var result = new Matrix(m.Rows, m.Columns);
        for (int i = 0; i < m.Rows; i++)
            for (int j = 0; j < m.Columns; j++)
                result[i, j] = m[i, j] * scalar;
        return result;
    }
public static Matrix operator +(Matrix m)
    {
        return m;
    }

public static Matrix operator -(Matrix m)
    {
        return m;
    }


public static Matrix operator +(Matrix m1, Matrix m2)
    {
        if (m1.Rows!= m2.Rows || m1.Columns!= m2.Columns)
            throw new ArgumentException("Matrices dimensions must match.");

        var result = new Matrix(m1.Rows, m1.Columns);
        for (int i = 0; i < m1.Rows; i++)
            for (int j = 0; j < m1.Columns; j++)
                result[i, j] = m1[i, j] + m2[i, j];
        return result;
    }

public static Matrix operator -(Matrix m1, Matrix m2)
    {
        if (m1.Rows!= m2.Rows || m1.Columns!= m2.Columns)
            throw new ArgumentException("Matrices dimensions must match.");

        var result = new Matrix(m1.Rows, m1.Columns);
        for (int i = 0; i < m1.Rows; i++)
            for (int j = 0; j < m1.Columns; j++)
                result[i, j] = m1[i, j] - m2[i, j];
        return result;
    }

public static Matrix operator *(Matrix m1, Matrix m2)
    {
        if (m1.Columns!= m2.Rows)
            throw new ArgumentException("Number of columns in the first matrix must equal the number of rows in the second matrix.");

        var result = new Matrix(m1.Rows, m2.Columns);
        for (int i = 0; i < m1.Rows; i++)
            for (int j = 0; j < m2.Columns; j++)
                for (int k = 0; k < m1.Columns; k++)
                    result[i, j] += m1[i, k] * m2[k, j];

        return result;
    }


public static Matrix operator /(Matrix m1, Matrix m2)
    {
        if (m1.Columns!= m2.Columns || m1.Rows!= m2.Rows)
            throw new ArgumentException("Both matrices must have the same dimensions.");

        var result = new Matrix(m1.Rows, m1.Columns);
        for (int i = 0; i < m1.Rows; i++)
            for (int j = 0; j < m1.Columns; j++)
                result[i, j] = m1[i, j] / m2[i, j];

        return result;
    }


    

  
}