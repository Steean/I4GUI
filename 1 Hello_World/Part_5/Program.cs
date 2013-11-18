using System;

namespace Part_5
{
    class Program
    {
        static void Main()
        {
            double guess = 50;
            double guessChanger = 50;
            double lastGuess = 50;
            var result = "";
            var correct = false;

            Console.WriteLine("*** Welcome to the Hi-Lo game ***");
            Console.WriteLine("Think of a number between 1 and 100");

            while (!correct)
            {
                Console.WriteLine("My guess is " + guess);
                Console.Write("Tell me if im <H>High, <L>Low or <E>Equal? ");
                result = Console.ReadLine();

                switch (result)
                {
                    case "h":
                    case "H":
                        lastGuess = guess;
                        guessChanger = Math.Round(guessChanger/2);
                        guess = lastGuess-guessChanger;
                        break;
                    case "l":
                    case "L":
                        lastGuess = guess;
                        guessChanger = Math.Round(guessChanger / 2);
                        guess = lastGuess + guessChanger;
                        break;
                    case "e":
                    case "E":
                        correct = true;
                        break;
                }

                if (guessChanger < 1)
                {
                    Console.WriteLine("You cheated! I quit!");
                    Environment.Exit(0);
                }
            }
            Console.WriteLine("*** I GOT IT! ***");
        }
    }
}
