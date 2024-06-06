public class LinearEquationSolver : IPolynomialSolver
{
    public Complex[] Solve(double[] coefficients)
    {
        if (coefficients.Length != 2)
            throw new ArgumentException("Коэффициенты линейного уравнения должны содержать 2 элемента.");

        double a = coefficients[0];
        double b = coefficients[1];

        if (a == 0)
            throw new DivideByZeroException("Коэффициент при x равен нулю.");

        return new Complex[] { new Complex(-b / a) };
    }
}