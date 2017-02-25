using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TicTacToe
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
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

		public MainWindow()
		{
			InitializeComponent();
			label_GameMessage.Content = "Go Player 1!";
			label_GameMessage.Foreground = Brushes.Red;
		}

		private void OnRectangleClick(object sender, MouseButtonEventArgs e)
		{
			Rectangle clickedPiece = e.Source as Rectangle;

			if (MoveCounter % 2 == 0)
			{
				clickedPiece.Fill = new SolidColorBrush(Colors.Red);
			}
			else
			{
				clickedPiece.Fill = new SolidColorBrush(Colors.Blue);
			}

			MoveCounter++;
			// Check for winner.
			UpdateGameMessageLabel();
		}

		private void btn_Reset_Click(object sender, RoutedEventArgs e)
		{
			foreach(Rectangle gamePiece in grid_TicTacToe.Children.OfType<Rectangle>())
			{
				gamePiece.Fill = new SolidColorBrush(Colors.White);
			}

			MoveCounter = 0;
			label_GameMessage.Content = "Go Player 1!";
			label_GameMessage.Foreground = Brushes.Red;
		}

		private void UpdateGameMessageLabel()
		{
			if (MoveCounter % 2 != 0)
			{
				label_GameMessage.Content = "Go Player 2!";
				label_GameMessage.Foreground = Brushes.Blue;
			}
			else
			{
				label_GameMessage.Content = "Go Player 1!";
				label_GameMessage.Foreground = Brushes.Red;
			}
		}
	}
}
