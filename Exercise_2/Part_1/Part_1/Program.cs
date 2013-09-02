using System;
using System.IO;
using System.Text;

namespace Part_1
{
    class Program
    {
        static void Main()
        {
            FileStream fs = new FileStream(@"C:\Users\Stian\Documents\IHA\4.semester\GUI\Exercise_2\02 deltagerliste.csv", FileMode.Open, FileAccess.Read);

            string[] tokens;
            string line = "";
            char[] separators = {';'};
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            Console.SetWindowSize(130,40);

            while((line = sr.ReadLine()) != null)
            {             
                tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (string i in tokens)
                {
                    Console.Write(i.PadRight(30));
                }
                Console.WriteLine();
            }

        }
    }
}
