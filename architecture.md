# Hangman Game
The first iteration of this hangman game will be interacted with and displayed in the terminal.
The plan is to decouple the game logic from the visuals/display of the hangman so that the game logic/state can be reused with a web app visual in the future.

## Classes
### Program
Will contain the logic for interacting with the terminal and feed the necessary starting information and guesses to the Game State.

### Game State
Will contain all the state information and logic necessary to represent and play a game of Hangman logically

### Visualizer
Will contain the logic needed to display the game to the terminal. Will likely pring ASCII characters that represent a hangman and will show the word length and guesses used.
This class will hold an instance/object of the **GameState** so it knows what to display.