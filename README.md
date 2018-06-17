# Lab03 Word Guess Game
Word Guess Game using Systems I/O concepts

## Dependencies
This game runs on .NET Core 2.1, which can be downloaded [here](https://www.microsoft.com/net/download/macos).

---
## Build
After installing the [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/macos), clone this repo onto your machine. From a terminal interface, go to where this was cloned and type the following commands:

```
cd Lab03_Word_Guess_Game
dotnet restore
dotnet run
```
---
## Concepts Implemented
This game implements CRUD operations in conjunction with Systems I/O.

A file will be create.  
It can be read.  
It can be updated.  
It can be deleted.

---
## How to Use the Program
The user will be presented with a menu to play 
or go to the admin portion.

If they choose to play, they will be presented with 
a hidden word and asked to guess a letter.
The letters will be revealed if they are correct.
They then will be directed back to the main menu.

If they choose the admin route, they can view the file, 
update the file, delete the file, return to the main menu, 
or exit the program.

---

## Screenshot Walk Through
1: Main Menu
![Main Menu](/mainMenu.png)

2: Game Play
![Start Of Guessing Game](/guessingTime.png)

3: If the user is correct, ask them to guess another letter
![Correct Guess](/guessingCorrect.png)

4: If the user is not correct, ask them to guess again
![Wrong Guess](/guessingWrong.png)

5: Let the user know when they have correctly guessed all letters.
![All Correctly Guessed Letters](/congrats.png)

6: Admin Options
![Admin Options](/admin.png)

7: View List of Words
![View List](/view.png)

8: Delete a Word
![Delete Word](/deleteAWord.png)

9: List After a Deletion
![List After a Deletion](/afterDelete.png)