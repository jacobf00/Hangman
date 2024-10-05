using System.IO.Compression;
using System.Security.Cryptography;
using Hangman.State;

namespace Hangman.Display;

class Visualizer
{
    private GameState GameState { get; set; }

    public Visualizer(GameState gameState)
    {
        GameState = gameState;
    }

    public void Update()
    {
        if (GameState.State is HangmanState.InProgress)
        {
            Console.Clear();
            DrawHangman(GameState.GetNumGuesses());
            DrawWordStateAndGuesses();
            int guessesLeft = GameState.Guesses.Count(c => c == '\0');
            Console.WriteLine($"Guesses left: {guessesLeft}");
            switch (GameState.LastGuessState)
            {
                case GuessState.GuessedCorrect:
                    Console.WriteLine("Nice! The secret word does contain that character!");
                    break;
                case GuessState.GuessedWrong:
                    Console.WriteLine("The secret word does not contain that character!");
                    break;
                case GuessState.GuessedRepeated:
                    Console.WriteLine("You already guessed this character. Try again!");
                    break;
            }
        }
    }
    private void DrawHangman(int numFailedGuesses)
    {
        Console.WriteLine("  +---+");
        Console.WriteLine("  |   |");
        switch (numFailedGuesses)
        {
            case 0:
                Console.WriteLine("      |\n" +
                                  "      |\n" +
                                  "      |"); break;
            case 1:
                Console.WriteLine("  O   |\n" +
                                  "      |\n" +
                                  "      |"); break;
            case 2:
                Console.WriteLine("  O   |\n" +
                                  "  |   |\n" +
                                  "      |"); break;
            case 3:
                Console.WriteLine("  O   |\n" +
                                  " /|   |\n" +
                                  "      |"); break;
            case 4:
                Console.WriteLine("  O   |\n" +
                                  " /|\\  |\n" +
                                  "      |"); break;
            case 5:
                Console.WriteLine("  O   |\n" +
                                  " /|\\  |\n" +
                                  " /    |"); break;
            case 6:
                Console.WriteLine("  O   |\n" +
                                  " /|\\  |\n" +
                                  " / \\  |"); break;
            default:
                break;
        }
        Console.WriteLine("      |");
        Console.WriteLine("=========");
    }

    public void DrawFinalState()
    {
        switch (GameState.State)
        {
            case HangmanState.Won:
                Console.WriteLine("Congratulations! You saved the Hangman!");
                break;
            case HangmanState.OutOfGuesses:
                Console.WriteLine("RIP Hangman, you failed!");
                break;
            default:
                Console.WriteLine("Something went terribly wrong! Program failed");
                break;
        }
        Console.WriteLine($"The secret word was: {GameState.SecretWord}");
    }

    private void DrawWordStateAndGuesses()
    {
        DrawWordState();
        Console.Write("         ");
        DrawGuesses();
    }

    private void DrawWordState()
    {
        Console.Write(string.Join("", GameState.WordState));
    }

    private void DrawGuesses()
    {
        string guessesUsed = string.Join("", GameState.Guesses);
        Console.WriteLine($"Guesses used: {string.Join("", GameState.Guesses)}");
    }
}