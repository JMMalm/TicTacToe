using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

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
		private int _moveCounter = 0;
		public int MoveCounter
		{
			get { return _moveCounter; }
			set { _moveCounter = value; }
		}

		/// <summary>
		/// Keeps track of the current player.
		/// </summary>
		private int _currentPlayer = 1;
		public int CurrentPlayer
		{
			get { return _currentPlayer; }
			set { _currentPlayer = value; }
		}

		/// <summary>
		/// Keeps track of whether the game is over (via won or draw).
		/// </summary>
		private bool _gameOver;
		public bool GameOver
		{
			get { return _gameOver; }
			set { _gameOver = value; }
		}

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
		private int[] _pickedRectangles = new int[9];
		public int[] PickedRectangles
		{
			get { return _pickedRectangles; }
			set { _pickedRectangles = value; }
		}

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
