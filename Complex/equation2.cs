using System;

public class PolynomialEquation : IPolynomialEquation
{
    private double[] coefficients;
    private IRootFindingStrategy strategy;

    public PolynomialEquation(double[] coefficients, IRootFindingStrategy strategy)
    {
        this.coefficients = coefficients;
        this.strategy = strategy;
    }

    public int Dimension => coefficients.Length;

    public double[] Coefficients => (double[])coefficients.Clone();

    public Complex[] FindRoots()
    {
        if (coefficients.Length < 2)
            throw new InvalidOperationException("Неизвестный тип уравнения");
        if (coefficients.Length > 4)
            throw new InvalidOperationException("Корней бесконечно много");
        return strategy.FindRoots(coefficients);
    }
}
