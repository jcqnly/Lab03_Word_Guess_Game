using System;
using System.IO;

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
            //Console.WriteLine("1. Play\n" +
            //    "2. Admin\n" +
            //    "3. Exit\n");
            ////Checks that the user enters in a number and that the number is between 1-3
            //if (Int32.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= 3)
            //{
            //    Console.WriteLine($"Hello {selection}.");
            //}
            //else
            //{
            //    Console.Clear();
            //    Console.WriteLine("Please enter one of the choices.");
            //    StartMenu();
            //}
            CreateFile();
            ReadFile();
            UpdateFile();
            DeleteFile();
        }

        /// <summary>
        /// Create a populated file with Systems.IO
        /// </summary>
        public static void CreateFile()
        {
            string path = "../../../GameWords.txt";
            //if a file doesn't exist, create it
            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write("cat\ndog\nman\nbear\npig");
                }
            }
        }

        /// <summary>
        /// Read a file with Systems.IO
        /// </summary>
        public static void ReadFile()
        {
            string path = "../../../GameWords.txt";
            using (StreamReader sr = File.OpenText(path))
            {
                //string s = "";
                //while ((s = sr.ReadLine()) != null)
                //{
                //    Console.WriteLine(s);
                //}

                try
                {
                    string[] gameText = File.ReadAllLines(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong.");
                }
            }
        }

        /// <summary>
        /// Update a file with Systems.IO
        /// TO DO: split this up to 2 methods where the user can enter a new word (for unit testing)
        /// </summary>
        public static void UpdateFile()
        {
            string path = "../../../GameWords.txt";
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("\nahoy");
            }
        }

        /// <summary>
        /// Delete a file with Systems.IO
        /// </summary>
        public static void DeleteFile()
        {
            string path = "../../../GameWords.txt";
            File.Delete(path);
            Console.WriteLine("File Deleted!!");
            Console.Read();
        }
    }
}
