using System;
using System.Linq;
using System.Windows;
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

		private const string Player1Message = "Go Player 1!";
		private const string Player2Message = "Go Player 2!";


		/// <summary>
		/// The primary UI that the user interacts with. Contains our game objects.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			// Player 1 (red) starts first.
			UpdateLabelsForNextPlayer(Player1Message, Brushes.Red);
		}

		/// <summary>
		/// Updates the game piece that the user has clicked on.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnRectangleClick(object sender, MouseButtonEventArgs e)
		{
			Rectangle clickedPiece = e.Source as Rectangle;
			UpdateGameUi(clickedPiece);
			UpdateGameLogicMap(Convert.ToInt32(clickedPiece.Uid));
			TicTacToeLogic.MoveCounter++;

			if (TicTacToeLogic.MoveCounter >= 5)
			{
				TicTacToeLogic.CheckForGameWinner();
			}
		}

		/// <summary>
		/// Reinitializes the game UI to the starting state (e.g. a new game).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Reset_Click(object sender, RoutedEventArgs e)
		{
			foreach(Rectangle gamePieces in grid_TicTacToe.Children.OfType<Rectangle>())
			{
				gamePieces.Fill = new SolidColorBrush(Colors.White);
			}

			TicTacToeLogic.MoveCounter = 0;
			TicTacToeLogic.PickedRectangles = new int[9];
			UpdateLabelsForNextPlayer(Player1Message, Brushes.Red);
		}

		/// <summary>
		/// Updates the game UI based on the last player's actions.
		/// </summary>
		/// <param name="clickedRectangle"></param>
		private void UpdateGameUi(Rectangle clickedRectangle)
		{
			if (TicTacToeLogic.IsPlayerOneTurn)
			{
				UpdateRectangleFillColor(clickedRectangle, Colors.Red);
				UpdateLabelsForNextPlayer(Player2Message, Brushes.Blue);
			}
			else
			{
				UpdateRectangleFillColor(clickedRectangle, Colors.Blue);
				UpdateLabelsForNextPlayer(Player1Message, Brushes.Red);
			}
		}

		/// <summary>
		/// Fills the rectangle that the user has clicked in the game.
		/// </summary>
		/// <param name="clickedRectangle">The rectangle clicked by the user.</param>
		/// <param name="color">The color the rectangle will be filled with.</param>
		/// <remarks>
		/// This method is called by the UpdateGameUi method, which is called after a
		/// player clicks a game rectangle or resets the game. Passing in null allows
		/// this same code to work when reseting the game.
		/// </remarks>
		private void UpdateRectangleFillColor(Rectangle clickedRectangle, Color color)
		{
			if (clickedRectangle != null)
			{
				clickedRectangle.Fill = new SolidColorBrush(color);
			}
		}

		/// <summary>
		/// Updates the game labels and color, indicating the turn of the next player.
		/// </summary>
		/// <param name="labelMessage">The message being passed to the label.</param>
		/// <param name="color">The color the label should be updated to.</param>
		private void UpdateLabelsForNextPlayer(string labelMessage, Brush color)
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
			int mapValue = (TicTacToeLogic.IsPlayerOneTurn) ? 1 : 2;
			TicTacToeLogic.PickedRectangles[arrayIndex] = mapValue;
		}
	}
}
