﻿struct Complex
{
    public double re;
    public double im;
    public Complex(double Re, double Im)
    {
        re = Re;
        im = Im;
    }
    public static Complex operator +(Complex a, Complex b)
    {
        return new(a.re + b.re, a.im + b.im);
    }
    public static Complex operator -(Complex a, Complex b)
    {
        return new(a.re - b.re, a.im - b.im);
    }
    public static Complex operator *(Complex a, Complex b)
    {
        return new(a.re * b.re - a.im * b.im, a.im * b.re + a.re * b.im);
    }
    public static Complex operator /(Complex a, Complex b)
    {
        double div = b.re * b.re + b.im * b.im;
        return new((a.re * b.re + a.im * b.im) / div, (b.re * a.im - a.re * b.im) / div);
    }
    public static double ComplexModule(Complex x)
    {
        return Math.Sqrt(x.re * x.re + x.im * x.im);
    }
    public static double ComplexArgument(Complex x)
    {
        return Math.Atan(x.im / x.re);
    }
    public static void Print_re(Complex a)
    {
        System.Console.WriteLine(a.re);
    }
    public static void Print_im(Complex a)
    {
        System.Console.WriteLine(a.im+"i");
    }
    public static void Print_all(Complex a)
    {
        if (a.im >= 0) System.Console.WriteLine($"{a.re} + {a.im}i");
        else System.Console.WriteLine($"{a.re} - {Math.Abs(a.im)}i");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Complexed madness :)");
        Complex num_1 = new Complex();
        Complex num_2 = new Complex();
        bool f = true;
        while (f)
        {
            Console.WriteLine();
            Console.WriteLine("Choose your option: ");
            Console.WriteLine("0.Create a complex number");
            Console.WriteLine("1.Sum the complex numbers");
            Console.WriteLine("2.Subtract the complex numbers");
            Console.WriteLine("3.Multiply the complex numbers");
            Console.WriteLine("4.Divide the complex numbers");
            Console.WriteLine("5.Find the module of the complex number");
            Console.WriteLine("6.Find the argument of the complex number");
            Console.WriteLine("7.Print the real part of the complex number");
            Console.WriteLine("8.Print the imaginary part of the complex number");
            Console.WriteLine("9.Print the complex number");
            Console.WriteLine("\"Q\" or \"q\" for exit");
            char s;
            s = Convert.ToChar(Console.ReadLine());
            switch (s)
            {
                case '0':
                    Console.WriteLine("Enter the real part of the complex number:");
                    double re = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the imaginary part of the complex number:");
                    double im = Convert.ToDouble(Console.ReadLine());
                    num_1 = new Complex(re, im);
                    continue;
                case '1':
                    Console.WriteLine("Enter the real part of the complex number:");
                    re = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the imaginary part of the complex number:");
                    im = Convert.ToDouble(Console.ReadLine());
                    num_2 = new Complex(re, im);
                    Console.Write("Sum: ");
                    Complex.Print_all(num_1 + num_2);
                    continue;
                case '2':
                    Console.WriteLine("Enter the real part of the complex number:");
                    re = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the imaginary part of the complex number:");
                    im = Convert.ToDouble(Console.ReadLine());
                    num_2 = new Complex(re, im);
                    Console.Write("Difference: ");
                    Complex.Print_all(num_1 - num_2);
                    continue;
                case '3':
                    Console.WriteLine("Enter the real part of the complex number:");
                    re = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the imaginary part of the complex number:");
                    im = Convert.ToDouble(Console.ReadLine());
                    num_2 = new Complex(re, im);
                    Console.Write("Multiplication: ");
                    Complex.Print_all(num_1 * num_2);
                    continue;
                case '4':
                    Console.WriteLine("Enter the real part of the complex number:");
                    re = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the imaginary part of the complex number:");
                    im = Convert.ToDouble(Console.ReadLine());
                    num_2 = new Complex(re, im);
                    Console.Write("Division: ");
                    Complex.Print_all(num_1 / num_2);
                    continue;
                case '5':
                    Console.WriteLine($"Module: {Complex.ComplexModule(num_1)}");
                    continue;
                case '6':
                    Console.WriteLine($"Argument: {Complex.ComplexArgument(num_1)}");
                    continue;
                case '7':
                    Console.Write($"Real part of the complex number: ");
                    Complex.Print_re(num_1);
                    Console.WriteLine();
                    continue;
                case '8':
                    Console.Write($"Imaginary part of the complex number: ");
                    Complex.Print_im(num_1);
                    Console.WriteLine();
                    continue;
                case '9':
                    Console.Write($"The complex number: ");
                    Complex.Print_all(num_1);
                    Console.WriteLine();
                    continue;
                case 'Q':
                    f = false;
                    break;
                case 'q':
                    f = false;
                    break;
                default:
                    Console.WriteLine("Error: Unknown operation. Try again.");
                    continue;
            }
        }
    }
}
