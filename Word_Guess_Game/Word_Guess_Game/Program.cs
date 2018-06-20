using System;
using System.IO;
using System.Linq;

namespace Word_Guess_Game
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = "../../../GameWords.txt";
            //create a list of word at game start
            //accounts for when user selects admin choices before even playing the game
            if (!File.Exists(path))
            {
                CreateFile();
            }
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
        /// Start the game process
        /// </summary>
        public static void PlayGame()
        {
            //create a file with words by calling this:
            CreateFile();
            //method to generate random word from list created at the start
            SelectWord();
        }

        /// <summary>
        /// Select a random word from pre-populated list and stores it to an array
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
                DisplayWord(randomWord);
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.");
            }
        }

        /// <summary>
        /// Displays the placeholder value for as many letters as there are 
        /// in the randomly selected word that will be passed to this method
        /// </summary>
        /// <param name="randomWord"></param>
        public static void DisplayWord(string randomWord)
        {
            //create a char array to store the random word
            //b/c of strict type, convert to char
            char[] storeRandomWord = randomWord.ToCharArray();
            //create a placeholder of string type because char type didn't work
            string placeholder = "X";
            //create a char array and store the placeholder
            char[] storeUnderscore = placeholder.ToArray();
            //create a new char array to hold the array of hidden words
            char[] wordWithPlaceholder = new char[randomWord.Length];
            //iterate through and add the placeholder to every spot
            for (int i = 0; i <= randomWord.Length-1; i++)
            {
                wordWithPlaceholder[i] = storeUnderscore[0];
                Console.Write($"{wordWithPlaceholder[i]} ");
            }
            //start the guessing process
            GuessingTime(randomWord, storeRandomWord, wordWithPlaceholder);
        }

        /// <summary>
        /// Checks user guess against what is currently stored as the random word
        /// </summary>
        /// <param name="randomWord"></param>
        /// <param name="storeRandomWord"></param>
        /// <param name="wordWithPlaceholder"></param>
        public static void GuessingTime(string randomWord, char[] storeRandomWord, char[] wordWithPlaceholder)
        {
            Console.WriteLine("\nGuess a letter\n");
            string guess = Console.ReadLine();
            //convert the guess from string to char
            char[] guessAsChar = guess.ToArray();
            //checks if user types more than a letter and/or number at a time
            //also checks whether input is a character
            if (guess.Length > 1 || !guess.All(c => Char.IsLetter(c)))
            {
                Console.WriteLine("Guess again:\n");
                //call the DisplayWord method and start the process again
                DisplayWord(randomWord);
            } //if that guess is 1 character and the letter is in the word...
            else if (randomWord.Contains(guess))
            {
                Console.Clear();
                //pass the char-ified guess to the reveal method
                RevealLetter(randomWord, guessAsChar, storeRandomWord, wordWithPlaceholder);
            }
            else
            {   //continue guessing if they guessed incorrectly
                Console.Clear();
                Console.WriteLine("Try again");
                RevealLetter(randomWord, guessAsChar, storeRandomWord, wordWithPlaceholder);
            }
        }

        /// <summary>
        /// This will only run if the user guess is in the word in 
        /// order to reveal where it is within the random word
        /// </summary>
        /// <param name="randomWord"></param>
        /// <param name="guessAsChar"></param>
        /// <param name="storeRandomWord"></param>
        /// <param name="wordWithPlaceholder"></param>
        public static void RevealLetter(string randomWord, char[] guessAsChar, char[] storeRandomWord, char[] wordWithPlaceholder)
        {
            for (int i = 0; i <= storeRandomWord.Length - 1; i++)
            {   //checks if the character at that iteration matches the guess
                if (storeRandomWord[i] == guessAsChar[0])
                {   //if that guess matches, then replace that index with that guess
                    wordWithPlaceholder[i] = guessAsChar[0];
                    Console.Write($"You correctly guessed: {guessAsChar[0]}\n");
                }
            }
            //create a placeholder of string type because char type didn't work
            for (int i = 0; i <= storeRandomWord.Length - 1; i++)
            {
                Console.Write($"{wordWithPlaceholder[i]} ");                
            }

            string placeholder = "X";
            //create a char array and store the placeholder
            char[] storeUnderscore = placeholder.ToArray();
            //displays the hidden word with the correctly revealed letter
            for (int i = 0; i <= wordWithPlaceholder.Length - 1; i++)
            {
                if (wordWithPlaceholder[i] == storeUnderscore[0])
                {   //as long as an X remains, the user will be prompted to continue guessing
                    while (wordWithPlaceholder[i] != storeUnderscore[0]);
                    {
                        GuessingTime(randomWord, storeRandomWord, wordWithPlaceholder); 
                    }
                } 
            }
            //otherwise, they will have guessed the correct word and be routed back to the maim menu
            Console.Clear();
            Console.WriteLine($"Congratulations! The word was {randomWord}.");
            Console.Read();
            Console.Clear();
            MainMenu();
        }

        /// <summary>
        /// Admin Menu with 4 options
        /// </summary>
        public static void AdminMenu()
        {
            string path = "../../../GameWords.txt";
            Console.WriteLine("1. View the list of words\n" +
                "2. Update the list of words\n" +
                "3. Delete\n" +
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
                        Console.Clear();
                        if (!File.Exists(path))
                        {
                            Console.WriteLine("No file with which to update.");
                            AdminMenu();
                        }
                        GetUserUpdate();
                        break;

                    case 3:
                        GetUserDeleteChoice();
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
        /// This will show the user the list of words
        /// </summary>
        public static bool ReadFile()
        {
            Console.Clear();
            string path = "../../../GameWords.txt";
            if (!File.Exists(path))
            {
                Console.WriteLine("There was no file, so one was made for you.");
                CreateFile();
            }
            //writes the list of words to the console.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s + "\n");
                }
            }
            Console.Read();
            Console.Clear();
            AdminMenu();
            return true;
        }

        /// <summary>
        /// Gets user input and checks if input is only letters.
        /// </summary>
        public static void GetUserUpdate()
        {
            Console.Clear();
            Console.WriteLine("Enter a word");
            string userUpdateForFile = Console.ReadLine();
            //The Linq library is needed because the Char.IsLetter() method
            //is used to verify that input are only letters
            bool allLetters = userUpdateForFile.All(c => Char.IsLetter(c));
            if (allLetters)
            {
                UpdateFile(userUpdateForFile);
            }
            else
            {
                Console.WriteLine("NOT all letters.  Try again");
                GetUserUpdate();
            }
        }

        /// <summary>
        /// If user input is only letters, then file will be updated
        /// </summary>
        /// <returns></returns>
        public static void UpdateFile(string userUpdateForFile)
        {
            string path = "../../../GameWords.txt";
            Console.Clear();
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.Write("\n" + userUpdateForFile);
            }
            Console.WriteLine("File update!");
            AdminMenu();
        }

        /// <summary>
        /// Give the user the option to delete the entire file or a specific word
        /// </summary>
        public static void GetUserDeleteChoice()
        {
            Console.Clear();
            string path = "../../../GameWords.txt";
            Console.WriteLine("1. Delete the entire file\n2. Delete a specific word\n3. Admin Menu");
            if (Int32.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= 3)
            {
                switch (selection)
                {
                    case 1:
                        DeleteFile();
                        break;

                    case 2:
                        Console.Clear();
                        if (!File.Exists(path))
                        {
                            Console.WriteLine("No file to delete.");
                            AdminMenu();
                        }
                        //show the user the words to delete
                        using (StreamReader sr = File.OpenText(path))
                        {
                            string s = "";
                            while ((s = sr.ReadLine()) != null)
                            {
                                Console.WriteLine(s);
                            }
                        }
                        Console.WriteLine("Which of those word would you like to delete?");
                        string specificWordToDelete = Console.ReadLine();
                        string trimWordToDelete = specificWordToDelete.Trim().ToLower();
                        Console.WriteLine($"the word to delete is {trimWordToDelete}");
                        Console.Read();
                        DeleteSpecificWord(trimWordToDelete);
                        break;

                    case 3:
                        Console.Clear();
                        AdminMenu();
                        break;

                    default:
                        GetUserDeleteChoice();
                        break;
                }
            }
        }

        /// <summary>
        /// Delete a specific word that the user requests
        /// </summary>
        public static void DeleteSpecificWord(string trimWordToDelete)
        {
            string path = "../../../GameWords.txt";
            Console.Clear();

            string[] wordList = File.ReadAllText(path).Split("\n");
            string[] trimmedList = new string[wordList.Length - 1];
            //loop through original word list
            for (int i = 0; i <= wordList.Length - 1; i++)
            {
                if (wordList[i] != trimWordToDelete)
                {
                    trimmedList[i] = wordList[i];
                }
                if (wordList[i] == trimWordToDelete)
                {
                    for (int j = 0, k = 0; j < trimmedList.Length; j++, k++)
                    {   //when the index of the new array reaches i, which is the index of the soon-to-be-deleted word
                        if (j == i)
                        { //increase the index of the new array as a way to 'shorten' its length
                            k++;
                            //continue populating the new array with the contents of the old array
                            trimmedList[j] = wordList[k];
                        }
                        else
                        {
                            trimmedList[j] = wordList[k];
                        }
                    }
                    break;
                }
            }
            Console.WriteLine($"{trimWordToDelete} has been deleted!");
            //delete the current file, so that...
            File.Delete(path);
            //the updated list can be written to the file
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    for (int m = 0; m <= trimmedList.Length-1; m++)
                    {
                        Console.WriteLine($"{trimmedList[m]}");
                        sw.WriteLine(trimmedList[m]);
                    }
                }

            }           
            Console.Read();
            MainMenu();
        }

        /// <summary>
        /// Delete the entire file
        /// </summary>
        public static bool DeleteFile()
        {
            string path = "../../../GameWords.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.Clear();
                Console.WriteLine("File Deleted!");
                AdminMenu();
                return true;
            }
            else if (!File.Exists(path))
            {
                Console.Clear();
                Console.WriteLine("No file to delete.");
                Console.Read();
                AdminMenu();
            }
            return true;
        }
    }
}