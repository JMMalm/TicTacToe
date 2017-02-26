using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace TicTacToe.Logic
{
	/// <summary>
	/// Handles the game logic for Tic Tac Toe.
	/// </summary>
	public class GameLogic
	{
		/// <summary>
		/// Tracks the number of moves made by the players.
		/// </summary>
		/// <remarks>
		/// The modulus of this value is often used for conditional operations.
		/// </remarks>
		private int _moveCounter = 0;
		public int MoveCounter
		{
			get
			{
				return _moveCounter;
			}
			set
			{
				_moveCounter = value;
			}
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
		/// This is more efficient than taking a list of rectangles from the UI and checking
		/// properties of those objects. Maps to the rectangles' UID in the XAML.
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
		/// Checks the game rectangles for a winning combination of 3
		/// rectangles of the same color.
		/// </summary>
		/// <returns>True if the game has been won.</returns>
		public bool CheckForGameWinner()
		{
			return false;
		}
	}
}
