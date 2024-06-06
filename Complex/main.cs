public static double[] ReadCoefficients(int degree)
{
    double[] coefficients = new double[degree + 1];
    for (int i = 0; i <= degree; i++)
    {
        Console.WriteLine($"Введите коэффициент при x^{degree - i}:");
        while (!double.TryParse(Console.ReadLine(), out coefficients[i]))
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
        }
    }
    return coefficients;
}
public static void Main(string[] args)
{
    Console.WriteLine("Введите степень уравнения:");
    if (!int.TryParse(Console.ReadLine(), out int degree))
    {
        Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
        return;
    }

    double[] coefficients = ReadCoefficients(degree);

    try
    {
        IPolynomialSolver solver = Strategies.GetStrategy(degree);
        Complex[] roots = solver.Solve(coefficients);
        Console.WriteLine("Корни уравнения:");
        foreach (var root in roots)
        {
            Console.WriteLine(root);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
    }
}