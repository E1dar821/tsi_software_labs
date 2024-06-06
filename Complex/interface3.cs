public class QuadraticEquationSolver : IPolynomialSolver
{
    public Complex[] Solve(double[] coefficients)
    {
        if (coefficients.Length != 3)
            throw new ArgumentException("Коэффициенты квадратного уравнения должны содержать 3 элемента.");

        double a = coefficients[0];
        double b = coefficients[1];
        double c = coefficients[2];

        double discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
            return new Complex[] { new Complex(-b / (2 * a), Math.Sqrt(-discriminant) / (2 * a)), new Complex(-b / (2 * a), -Math.Sqrt(-discriminant) / (2 * a)) };
        else
            return new Complex[] { new Complex((-b + Math.Sqrt(discriminant)) / (2 * a)), new Complex((-b - Math.Sqrt(discriminant)) / (2 * a)) };
    }
}binus