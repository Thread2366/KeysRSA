using System;
using System.IO;
using System.Collections;

namespace Lab7
{
    class Program
    {
        public static int EulerFunction(int p, int q)
        {
            return (p - 1) * (q - 1);
        }

        public static int GreaterCommonDivisor(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            else
            {
                return GreaterCommonDivisor(b, a % b);
            } 
        }

        public static int GetPrimeNumber()
        {
            bool[] numbers = new bool[9998];
            for (int i = 2; (int)Math.Pow(i, 2) <= 9999; i++)
            {
                if (numbers[i - 2] == false)
                {
                    for (int j = (int)Math.Pow(i, 2); j <= 9999; j += i)
                    {
                        numbers[j - 2] = true;
                    }
                }
            }
            ArrayList PrimeNumbers = new ArrayList();
            for (int i = 1002; i < 9999; i++)
            {
                if (numbers[i - 2] == false)
                {
                    PrimeNumbers.Add(i);
                }
            }
            Random rnd = new Random((int)DateTime.Now.Ticks);
            var PrimeNumber = PrimeNumbers[rnd.Next(PrimeNumbers.Count)];
            return (int)PrimeNumber;
        }

        public static int GetPublicExponent(int p, int q)
        {
            int f = EulerFunction(p, q);
            Random rnd = new Random((int)DateTime.Now.Ticks);
            int e = rnd.Next(2, f);
            while (GreaterCommonDivisor(e, f) != 1)
            {
                e = rnd.Next(10, 99);
            }
            //Console.WriteLine("{0}, {1}, {2}", p, q, f);
            return e;
        }

        public static int GetPrivateExponent(int f, int e)
        {
            int a1 = e, b1 = f, x1 = 0, x2 = 1, y1 = 1, y2 = 0;
            while (b1 > 0)
            {
                int q = a1 / b1;
                int r = a1 - q * b1;
                int x = x2 - q * x1;
                int y = y2 - q * y1;
                a1 = b1;
                b1 = r;
                x2 = x1;
                y2 = y1;
                x1 = x;
                y1 = y;
            }
            if (x2 < 0)
            {
                x2 = f + x2;
            }
            return x2;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Сгенерировать простые числа? (y/n)");
            if (Console.ReadLine() == "y")
            {
                StreamWriter pnw = new StreamWriter("../PrimeNumbers.txt", false);
                pnw.WriteLine(GetPrimeNumber());
                pnw.WriteLine(GetPrimeNumber());
                pnw.Close();
            }

            StreamReader pnr = new StreamReader("../PrimeNumbers.txt");
            var p = int.Parse(pnr.ReadLine());
            var q = int.Parse(pnr.ReadLine());
            pnr.Close();

            int e = GetPublicExponent(p, q);
            StreamWriter pbw = new StreamWriter("../PublicKey.txt", false);
            pbw.WriteLine(e);
            pbw.WriteLine(p * q);
            pbw.Close();
            //Console.WriteLine(e);

            int d = GetPrivateExponent(EulerFunction(p, q), e);
            StreamWriter pvw = new StreamWriter("../PrivateKey.txt", false);
            pvw.WriteLine(d);
            pvw.WriteLine(p * q);
            pvw.Close();
            //Console.WriteLine(d);
        }
    }
}
