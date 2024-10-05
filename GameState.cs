using System.Dynamic;

namespace Hangman.State;

public enum HangmanState
{
    InProgress,
    OutOfGuesses,
    Won
}

public enum GuessState
{
    GuessedCorrect,
    GuessedWrong,
    GuessedRepeated
}
class GameState
{
    public string SecretWord { get; set; }
    public char[] WordState { get; set; }
    public char[] Guesses { get; set; }
    public HangmanState State { get; set; }
    public GuessState LastGuessState { get; set; }


    public GameState(string secret_word)
    {
        SecretWord = secret_word;
        WordState = Enumerable.Repeat('*', secret_word.Length).ToArray();
        Guesses = new char[7];
        State = HangmanState.InProgress;
    }

    public int GetNumGuesses()
    {
        return Guesses.Count(c => c != '\0');
    }

    public bool Guess(char guess)
    {
        bool shouldContinue = true;


        var correctlyGuessedIndices = SecretWord
            .Select((c, index) => new { c, index })
            .Where(x => x.c == guess)
            .Select(x => x.index)
            .ToList();

        if (!correctlyGuessedIndices.Any())
        {
            if (Guesses.Contains(guess))
            {
                LastGuessState = GuessState.GuessedRepeated;
                return shouldContinue;
            }
            LastGuessState = GuessState.GuessedWrong;
            // if guess char isn't found in SecretWord, add guess to Guesses array
            for (int i = 0; i < Guesses.Length; i++)
            {
                if (Guesses[i] == '\0')
                {
                    Guesses[i] = guess;
                    break;
                }
            }
            // if adding the guess fills the Guesses array, set State to OutOfGuesses and stop the game loop
            if (!Guesses.Contains('\0'))
            {
                State = HangmanState.OutOfGuesses;
                shouldContinue = false;
            }
            return shouldContinue;
        }
        else
        {
            LastGuessState = GuessState.GuessedCorrect;
            foreach (int index in correctlyGuessedIndices)
            {
                WordState[index] = guess;
            }
            // if adding the guess fills the WordState array, set State to Won and stop the game loop
            if (!WordState.Contains('*'))
            {
                State = HangmanState.Won;
                shouldContinue = false;
            }
            return shouldContinue;
        }
    }
}
