public static class Strategies
{
    public static readonly IPolynomialSolver Linear = new LinearEquationSolver();
    public static readonly IPolynomialSolver Quadratic = new QuadraticEquationSolver();
    public static readonly IPolynomialSolver Cubic = new CubicEquationSolver();

    public static IPolynomialSolver GetStrategy(int degree)
    {
        switch (degree)
        {
            case 1: return Linear;
            case 2: return Quadratic;
            case 3: return Cubic;
            default: throw new ArgumentException("Неизвестная степень уравнения.");
        }
    }
}