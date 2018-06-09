using System;

namespace Word_Guess_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            StartMenu();
        }

        /// <summary>
        /// Menu with 3 options for the user
        /// </summary>
        public static void StartMenu()
        {
            Console.WriteLine("1. Play\n" +
                "2. Admin\n" +
                "3. Exit\n");
            //Checks that the user enters in a number and that the number is between 1-3
            if (Int32.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= 3)
            {
                Console.WriteLine($"Hello {selection}.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Please enter one of the choices.");
                StartMenu();
            }
        }
    }
}
