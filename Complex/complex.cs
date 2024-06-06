using System;

public class Complex
{
    public static readonly Complex Zero = new Complex(0, 0);
    public static readonly Complex One = new Complex(1, 0);
    public static readonly Complex ImaginaryOne = new Complex(0, 1);

    // Свойства
    public double Real { get; }
    public double Imaginary { get; }

    // Конструкторы
    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public Complex(double real) : this(real, 0) { }
    public Complex() : this(0, 0) { }

    // Методы
    public static Complex Re(double real) => new Complex(real, 0);
    public static Complex Im(double imaginary) => new Complex(0, imaginary);

    public static Complex Sqrt(double real) => new Complex(Math.Sqrt(real), 0);

    // Свойство "Длина"
    public double Length => Math.Sqrt(Real * Real + Imaginary * Imaginary);

    // Операторы
    public static Complex operator +(Complex a, Complex b) => new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
    public static Complex operator -(Complex a, Complex b) => new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
    public static Complex operator *(Complex a, Complex b) => new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
    public static Complex operator /(Complex a, Complex b)
    {
        if (b.Real == 0 && b.Imaginary == 0)
            throw new DivideByZeroException("Деление на ноль невозможно.");
        double denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
        return new Complex((a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator, (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator);
    }

    public static Complex operator -(Complex a) => new Complex(-a.Real, -a.Imaginary);
    public static Complex operator +(Complex a) => a;


    public override string ToString() => $"{Real} + {Imaginary}i";


    public override bool Equals(object obj) => obj is Complex other && Real == other.Real && Imaginary == other.Imaginary;
    public override int GetHashCode() => HashCode.Combine(Real, Imaginary);
}