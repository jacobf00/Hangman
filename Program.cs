// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using Hangman.State;
using Hangman.Display;
using System.Threading;

class Program
{
    public static void Main(string[] args)
    {
        string? secretWord = "";
        Console.WriteLine("Enter your secret word:");
        while (secretWord == "")
        {
            secretWord = Console.ReadLine();
            if (secretWord == "")
            {
                Console.WriteLine("Nothing entered, please try again.");
            }
        }
        Console.WriteLine($"Your secret hangman word is: {secretWord}");

        Thread.Sleep(2000);
        Console.Clear();

        var gameState = new GameState(secretWord);
        var vis = new Visualizer(gameState);


        bool shouldContinue = true;
        char guess;

        while (shouldContinue)
        {
            guess = GetGuess();
            shouldContinue = gameState.Guess(guess);
            vis.Update();
        }

        vis.DrawFinalState();
    }

    private static char GetGuess()
    {
        var guess = new char();
        string? guessString;
        Console.WriteLine("Enter your guess:");
        while (guess == '\0')
        {
            guessString = Console.ReadLine();
            try
            {
                guess = char.Parse(guessString);
                return guess;
            }
            catch (Exception e)
            {
                Console.WriteLine("No valid char given. Please try again");
                Console.WriteLine(e.Message);
                continue;
            }
        }
        return '\0';
    }
}
