using System;
using System.Linq;

public static class Equations
{
    public static double[] RemoveLeadingZeros(double[] coefficients)
    {
        int firstNonZeroIndex = Array.FindIndex(coefficients, c => c != 0);
        if (firstNonZeroIndex == -1)
            return new double[0]; // Все коэффициенты равны нулю
        return coefficients.Skip(firstNonZeroIndex).ToArray();
    }

    public static IPolynomialEquation CreateEquation(double[] coefficients)
    {
        coefficients = RemoveLeadingZeros(coefficients);
        if (coefficients.Length < 2)
            throw new InvalidOperationException("Неизвестный тип уравнения");
        if (coefficients.Length > 4)
            throw new InvalidOperationException("Корней бесконечно много");

        return new PolynomialEquation(coefficients, new DummyRootFindingStrategy());
    }
}