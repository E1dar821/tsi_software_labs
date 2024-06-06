public interface IPolynomialEquation
{
    int Dimension { get; }
    double[] Coefficients { get; }
    Complex[] FindRoots();
}

