using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TicTacToe.Logic;

namespace TicTacToe
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private const string Player1Message = "Go Player 1!";
		private const string Player2Message = "Go Player 2!";
		private const string TiedGameMessage = "It's a tie!";
		private const string SymbolX = "X";
		private const string SymbolO = "O";

		/// <summary>
		/// Manages the logic details of the game, separating those concerns
		/// from the game's UI.
		/// </summary>
		private GameLogic _ticTacToeLogic = null;
		private GameLogic TicTacToeLogic
		{
			get
			{
				if (_ticTacToeLogic == null)
				{
					_ticTacToeLogic = new GameLogic();
				}

				return _ticTacToeLogic;
			}
		}

		/// <summary>
		/// The primary UI that the user interacts with. Contains our game objects.
		/// </summary>
		/// <remarks>
		/// Player 1 always starts first and is "X" with red background.
		/// </remarks>
		public MainWindow()
		{
			InitializeComponent();
			UpdateGameLabelForNextPlayer(Player1Message, Brushes.Red);
		}

		/// <summary>
		/// Updates the game piece that the user has clicked on.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnRectangleClick(object sender, MouseButtonEventArgs e)
		{
			if (TicTacToeLogic.GameOver)
			{
				return;
			}

			Rectangle clickedPiece = e.Source as Rectangle;
			int rectangleId = Convert.ToInt32(clickedPiece.Uid);

			if (TicTacToeLogic.IsValidMove(rectangleId))
			{
				UpdateGameUi(clickedPiece);
				UpdateGameLogicMap(rectangleId);
				TicTacToeLogic.MoveCounter++;

				if (TicTacToeLogic.MoveCounter >= 5)
				{
					CheckForGameWinner();
					CheckForGameTie();
				}

				TicTacToeLogic.CurrentPlayer = TicTacToeLogic.IsPlayerOneTurn ? 1 : 2;
			}
		}

		/// <summary>
		/// Reinitializes the game UI to the starting state (e.g. a new game).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Reset_Click(object sender, RoutedEventArgs e)
		{
			foreach (Rectangle gamePieces in grid_TicTacToe.Children.OfType<Rectangle>())
			{
				gamePieces.Fill = new SolidColorBrush(Colors.White);
			}

			TicTacToeLogic.ResetGame();
			UpdateGameLabelForNextPlayer(Player1Message, Brushes.Red);
		}

		/// <summary>
		/// Examines the game board after a player has moved to determine
		/// if there is a winner.
		/// </summary>
		/// <remarks>
		/// This code will halt the game if a winner exists; in that case,
		/// the game must be reset via the 'Reset' button.
		/// </remarks>
		private void CheckForGameWinner()
		{
			TicTacToeLogic.CheckForGameWinner();

			if (TicTacToeLogic.GameOver)
			{
				UpdateGameLabelForNextPlayer(string.Format("Player {0} has won!", TicTacToeLogic.CurrentPlayer), Brushes.DarkGoldenrod);
			}
		}

		/// <summary>
		/// Ends the game if there is no winner after all 9 rectangles have been picked.
		/// </summary>
		private void CheckForGameTie()
		{
			if (TicTacToeLogic.MoveCounter >= 9 && !TicTacToeLogic.GameOver)
			{
				UpdateGameLabelForNextPlayer(TiedGameMessage, Brushes.Black);
				TicTacToeLogic.GameOver = true;
			}
		}

		/// <summary>
		/// Updates the game UI based on the last player's actions.
		/// </summary>
		/// <param name="clickedRectangle">The rectangle clicked by the user.</param>
		private void UpdateGameUi(Rectangle clickedRectangle)
		{
			if (TicTacToeLogic.IsPlayerOneTurn)
			{
				UpdateRectangleFill(clickedRectangle, SymbolX, new SolidColorBrush(Colors.Red));
				UpdateGameLabelForNextPlayer(Player2Message, Brushes.Blue);
			}
			else
			{
				UpdateRectangleFill(clickedRectangle, SymbolO, new SolidColorBrush(Colors.Blue));
				UpdateGameLabelForNextPlayer(Player1Message, Brushes.Red);
			}
		}

		/// <summary>
		/// Fills the rectangle that the user has clicked in the game.
		/// </summary>
		/// <param name="clickedRectangle">The rectangle clicked by the user.</param>
		/// <param name="color">The color the rectangle will be filled with.</param>
		private void UpdateRectangleFill(Rectangle clickedRectangle, string playerSymbol, Brush color)
		{
			if (clickedRectangle != null)
			{
				TextBlock tb = new TextBlock();
				tb.FontSize = 72;
				tb.Background = color;
				tb.Text = playerSymbol;
				BitmapCacheBrush bcb = new BitmapCacheBrush(tb);
				clickedRectangle.Fill = bcb;
			}
		}

		/// <summary>
		/// Updates the game labels and color, indicating the turn of the next player.
		/// </summary>
		/// <param name="labelMessage">The message being passed to the label.</param>
		/// <param name="color">The color the label should be updated to.</param>
		private void UpdateGameLabelForNextPlayer(string labelMessage, Brush color)
		{
			label_GameMessage.Content = labelMessage;
			label_GameMessage.Foreground = color;
		}

		/// <summary>
		/// Updates the GameLogic's map with the index of
		/// the last rectangle picked.
		/// </summary>
		/// <param name="arrayIndex">The array index to update.</param>
		/// <remarks>
		///	The array index maps to the UID of the rectangle that was clicked.
		/// </remarks>
		private void UpdateGameLogicMap(int arrayIndex)
		{
			int mapValue = TicTacToeLogic.CurrentPlayer;
			TicTacToeLogic.PickedRectangles[arrayIndex] = mapValue;
		}
	}
}
