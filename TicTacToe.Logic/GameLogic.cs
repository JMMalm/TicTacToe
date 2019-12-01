namespace TicTacToe.Logic
{
	/// <summary>
	/// Handles the game logic for Tic Tac Toe and keeps track of player data.
	/// </summary>
	public class GameLogic
	{
		/// <summary>
		/// Tracks the number of moves made by the players.
		/// </summary>
		public int MoveCounter { get; set; } = 0;

		/// <summary>
		/// Keeps track of the current player.
		/// </summary>
		public int CurrentPlayer { get; set; } = 1;

		/// <summary>
		/// Keeps track of whether the game is over (via won or draw).
		/// </summary>
		public bool GameOver { get; set; }

		/// <summary>
		/// Determine the current player. Used by the UI and game logic.
		/// </summary>
		/// <remarks>
		/// Player 1 is red color and moves on even numbers.
		/// Player 2 is blue color and moves on odd numbers.
		/// </remarks>
		public bool IsPlayerOneTurn
		{
			get { return MoveCounter % 2 == 0; }
		}

		/// <summary>
		/// Keeps track of the rectangles users have picked.
		/// </summary>
		/// <remarks>
		/// This array maps to the rectangles' UID in the XAML.
		/// 0 is unclaimed; 1 = player 1; 2 = player 2.
		/// </remarks>
		public int[] PickedRectangles { get; set; } = new int[9];

		/// <summary>
		/// Constructor.
		/// </summary>
		public GameLogic() { }

		/// <summary>
		/// Checks for a winning combination on the game board,
		/// assigning the result to the appropriate property.
		/// </summary>
		public void CheckForGameWinner()
		{
			GameOver = GameWinnerFound();
		}

		/// <summary>
		/// Checks if the rectangle a player clicked on is "valid." (not claimed by either player)
		/// </summary>
		/// <param name="clickedRectangleIndex">The index of the grid rectangle selected by the user.</param>
		/// <returns>True if the clicked rectangle has not been claimed by a player.</returns>
		public bool IsValidMove(int clickedRectangleIndex)
		{
			if (PickedRectangles[clickedRectangleIndex] == 0)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Checks the game rectangles for a winning combination of 3
		/// consecutive rectangles horizontally, vertically, or diagonally.
		/// </summary>
		/// <returns>True if the game has been won.</returns>
		/// <remarks>
		/// There is likely a better way to check all 8 winning combinations
		/// but this is at least easily readable.
		/// </remarks>
		private bool GameWinnerFound()
		{
			if (WinningCombinationFound(0, 1, 2)) return true;
			if (WinningCombinationFound(3, 4, 5)) return true;
			if (WinningCombinationFound(6, 7, 8)) return true;

			if (WinningCombinationFound(0, 3, 6)) return true;
			if (WinningCombinationFound(1, 4, 7)) return true;
			if (WinningCombinationFound(2, 5, 8)) return true;

			if (WinningCombinationFound(0, 4, 8)) return true;
			if (WinningCombinationFound(2, 4, 6)) return true;

			return false;
		}

		/// <summary>
		/// Scans 3 provided rectangles for a winning combination. (3 in a row)
		/// </summary>
		/// <param name="left">The left-most index.</param>
		/// <param name="mid">The center index.</param>
		/// <param name="right">The right-most index.</param>
		/// <returns>True if there is a match across all 3 indices, indicating a game winner.</returns>
		private bool WinningCombinationFound(int left, int mid, int right)
		{
			return (
				PickedRectangles[left] == CurrentPlayer &&
				PickedRectangles[mid] == CurrentPlayer &&
				PickedRectangles[right] == CurrentPlayer
			);
		}

		/// <summary>
		/// Resets game-tracking properties for a new game.
		/// </summary>
		public void ResetGame()
		{
			MoveCounter = 0;
			GameOver = false;
			CurrentPlayer = 1;
			PickedRectangles = new int[9];
		}
	}
}
