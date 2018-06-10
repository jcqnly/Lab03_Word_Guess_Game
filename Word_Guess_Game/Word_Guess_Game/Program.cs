using System;
using System.IO;
using System.Linq;

namespace Word_Guess_Game
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainMenu();
        }

        /// <summary>
        /// Start Menu with 3 options for the user
        /// </summary>
        public static void MainMenu()
        {
            Console.WriteLine("1. Play the Game\n" +
                "2. Admin Menu\n" +
                "3. Exit the Program\n");
            //Checks that the user enters in a number and that the number is between 1-3
            if (Int32.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= 3)
            {
                switch (selection)
                {
                    case 1:
                        Console.Clear();
                        PlayGame();
                        break;

                    case 2:
                        Console.Clear();
                        AdminMenu();
                        break;

                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
            else //user will be prompted again if they didn't select one of the options
            {
                Console.Clear();
                Console.WriteLine("Please enter one of the choices.");
                MainMenu();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void PlayGame()
        {
            //create a file with words by calling this:
            CreateFile();
            //method to generate random word from list created at the start
            SelectWord();
        }

        /// <summary>
        /// Select a random word from pre-populated list
        /// </summary>
        public static void SelectWord()
        {
            string path = "../../../GameWords.txt";
            try
            {
                //read words from the file and stores it
                string[] gameText = File.ReadAllLines(path);
                //generate a random number based on the length of the word list
                Random word = new Random();
                int value = word.Next(gameText.Length);
                string randomWord = gameText[value];
                //send off the random word to be replaced by underscore
                //to start the guessing process
                DisplayWord(randomWord);
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.");
            }
        }

        /// <summary>
        /// Replace the random word with underscores
        /// that corresponds to the length of the word
        /// </summary>
        /// <param name="randomWord"></param>
        public static void DisplayWord(string randomWord)
        {
            int randomWordLength = randomWord.Length;            
            for (int i = 0; i <= randomWordLength - 1; i++) Console.Write(" _ ");
           
            //this method will take in user guesses
            GuessingTime(randomWord, randomWordLength);
        }

        /// <summary>
        /// If user guesses a letter in the word, the result is stored
        /// Otherwise, they'll have to keep trying
        /// </summary>
        /// <param name="randomWord"></param>
        public static void GuessingTime(string randomWord, int randomWordLength)
        {
            Console.WriteLine(randomWord);
            Console.WriteLine("\nGuess a letter\n");
            string guess = Console.ReadLine();
            //check if user types in more than 1 char and if the letter is contained in the random word
            if (guess.Length > 1 || !guess.All(c => Char.IsLetter(c)))
            {
                Console.Clear();
                Console.WriteLine("Guess again");
                DisplayWord(randomWord);
            }
            else if (randomWord.Contains(guess))
            {
                Console.WriteLine("Excellent guess!");
                //pass correct letter guess to another method
                //RevealLetter(guess, randomWord, randomWordLength);
            }
            else
            {
                Console.WriteLine("Try again");
                DisplayWord(randomWord);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Admin Menu with 4 options for the user
        /// </summary>
        public static void AdminMenu()
        {
            Console.WriteLine("1. View the list of words\n" +
                "2. Update the list of words\n" +
                "3. Delete the list of words\n" +
                "4. Return to the Main Menu\n" +
                "5. Exit the program");
            //Checks that the user enters in a number and that the number is between 1-5
            if (Int32.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= 5)
            {
                switch (selection)
                {
                    case 1:
                        ReadFile();
                        break;

                    case 2:
                        GetUserUpdate();
                        break;

                    case 3:
                        DeleteFile();
                        break;

                    case 4:
                        Console.Clear();
                        MainMenu();
                        break;

                    case 5:
                        Environment.Exit(0);
                        break;
                }
            }
            else //user will be prompted again if they didn't select one of the options
            {
                Console.Clear();
                Console.WriteLine("Please enter one of the choices.");
                AdminMenu();
            }         
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
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s + "\n");
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
        /// If user input is only letters, then file will be updated
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