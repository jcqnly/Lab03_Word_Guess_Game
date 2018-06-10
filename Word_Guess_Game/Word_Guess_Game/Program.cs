using System;
using System.IO;
using System.Linq;

namespace Word_Guess_Game
{
    public class Program
    {
        public static void Main(string[] args)
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
            GetUserUpdate();
            DeleteFile();
        }

        /// <summary>
        /// Create a populated file with Systems.IO
        /// </summary>
        public static bool CreateFile()
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
            return true;
        }

        /// <summary>
        /// Read a file with Systems.IO
        /// </summary>
        public static bool ReadFile()
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
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong.");
                }
            }
            return true;
        }

        /// <summary>
        /// Gets user input and checks if input is only letters.
        /// </summary>
        public static void GetUserUpdate()
        {
            Console.WriteLine("Enter a word");
            string userUpdateForFile = Console.ReadLine();
            //The Linq library is needed because the Char.IsLetter() method
            //is used to verify that input are only letters
            bool allLetters = userUpdateForFile.All(c => Char.IsLetter(c));
            if (allLetters)
            {
                UpdateFile(userUpdateForFile);
            }
            else Console.WriteLine("NOT all letters");
        }

        /// <summary>
        /// If user input is only letters, then text will be updated
        /// </summary>
        /// <returns></returns>
        public static string UpdateFile(string userUpdateForFile)
        {
            string path = "../../../GameWords.txt";
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("\n" + userUpdateForFile);
            }

            return "File updated!";
        }

        /// <summary>
        /// Delete a file with Systems.IO
        /// </summary>
        public static bool DeleteFile()
        {
            string path = "../../../GameWords.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine("File Deleted!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}