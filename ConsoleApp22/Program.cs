using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp22
{
    internal class Program
    {
        static double Dihotomia(Func<double, double> func, double precision, double left, double right)
        {
            var middle = (left + right) / 2;
            var temp = func(middle);
            int i = 0;
            while (Math.Abs(temp) > precision)
            {
                i++;
                middle = (left + right) / 2;
                temp = func(middle);
                if (temp > 0)
                {
                    right = middle;
                }
                else
                {
                    left = middle;
                }
            }

            Console.WriteLine(i);
            return (left + right) / 2;
        }

        static double Newton(Func<double, double> func, double precision, double startPoint, Func<double, double> derivative)
        {
            var x0 = startPoint;
            int i = 0;
            while (Math.Abs(func(x0)) > precision)
            {
                i++;
                x0 = x0 - func(x0) / derivative(x0);
            }

            Console.WriteLine(i);
            return x0;
        }

        static double NewtonMod(Func<double, double> func, double precision, double startPoint, Func<double, double> derivative)
        {
            var x0 = startPoint;
            int i = 0;
            while (Math.Abs(func(x0)) > precision)
            {
                i++;
                x0 = x0 - func(x0) / derivative(startPoint);
            }

            Console.WriteLine(i);
            return x0;
        }

        static double StaticHords(Func<double, double> func, double precision, double startPoint, double nextPoint)
        {
            var xn = 1.5;
            var i = 0;
            while (Math.Abs(func(xn)) > precision)
            {
                i++;
                xn = xn - (func(xn) * (xn - startPoint)) / (func(xn) - func(startPoint));
            }

            Console.WriteLine(i);
            return xn;
        }

        static double MovingHords(Func<double, double> func, double precision, double startPoint, double nextPoint)
        {
            var xn = startPoint;
            var xnprev = nextPoint;
            int i = 0;
            while (Math.Abs(func(xn)) > precision)
            {
                i++;
                xn = xn - (func(xn) * (xn - xnprev)) / (func(xn) - func(xnprev));
            }

            Console.WriteLine(i);
            return xn;
        }

        static double SimpleIteration(Func<double, double> func, Func<double, double> iterFunc, double precision, double startPoint)
        {
            var i = 0;
            while (Math.Abs(func(startPoint)) > precision)
            {
                i++;
                startPoint = iterFunc(startPoint);
            }

            Console.WriteLine(i);
            return startPoint;
        }

        static void Main(string[] args)
        {
            var func = new Func<double, double>(x => x * x - 3 * Math.Log(x + 2));
            var derivative = new Func<double, double>(x => 2 * x - 3 / (x + 2));
            var precision = 0.5 / 100000;
            var dihotomiaRes = Dihotomia(func, precision, 1.5, 2.5);
            Console.WriteLine(dihotomiaRes);
            Console.WriteLine(func(dihotomiaRes));
            var newtonRes = Newton(func, precision, 2.5, derivative);
            Console.WriteLine(newtonRes);
            Console.WriteLine(func(newtonRes));
            var newtonModRes = NewtonMod(func, precision, 2.5, derivative);
            Console.WriteLine(newtonModRes);
            Console.WriteLine(func(newtonModRes));
            var staticHordsRes = StaticHords(func, precision, 2.5, 2);
            Console.WriteLine(staticHordsRes);
            Console.WriteLine(func(staticHordsRes));
            var movingHordsRes = MovingHords(func, precision, 2.5, 2);
            Console.WriteLine(movingHordsRes);
            Console.WriteLine(func(movingHordsRes));
            var iterFunc = new Func<double, double>(x => Math.Sqrt(3 * Math.Log(x + 2)));
            var iterFuncRes = SimpleIteration(func, iterFunc, precision, 2.5);
            Console.WriteLine(iterFuncRes);
            Console.WriteLine(func(iterFuncRes));
        }
    }
}