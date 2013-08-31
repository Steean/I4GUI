using System;

namespace Part_3
{
    class Program
    {
        static void Main()
        {
            int a = 0, b = 0, result = 0;
            Console.Write("Enter 1 number: ");
            a = int.Parse(Console.ReadLine());

            Console.Write("Enter 2 number: ");
            b = int.Parse(Console.ReadLine());

            result = a + b;
            Console.WriteLine("The sum of " + a + " and " + b + " is " + result);

        }
    }
}
