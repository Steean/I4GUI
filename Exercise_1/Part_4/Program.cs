using System;


namespace Part_4
{
    class Program
    {
        static void Main()
        {
            var random = new Random();
            var randomNr = random.Next(1, 100);
            var correct = false;

            Console.WriteLine("The computer choose a number between 1 and 100, you guess it");
            while (!correct)
            {
                var guess = 0;
                Console.WriteLine("Enter your guess: ");
                guess = int.Parse(Console.ReadLine());

                if (guess < randomNr)
                    Console.WriteLine("Your guess is too low!");
                if (guess > randomNr)
                    Console.WriteLine("Your guess is too high!");
                if (guess == randomNr)
                    correct = true;
            }
            Console.WriteLine("YOU GOT IT!!!");
        }
    }
}
