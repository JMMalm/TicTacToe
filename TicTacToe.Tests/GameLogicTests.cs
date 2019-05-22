using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Logic;

namespace TicTacToe.Tests
{
	[TestClass]
	public class GameLogicTests
	{
		private static GameLogic _gameLogic = null;

		[ClassInitialize]
		public static void Initalize(TestContext context)
		{
			_gameLogic = new GameLogic()
			{
				CurrentPlayer = 1,
				PickedRectangles = new int[9]
			};
		}

		[TestMethod]
		public void CheckForGameWinner_NoWinner_ReturnsFalse()
		{
			// Arrange
			_gameLogic.PickedRectangles = new int[] { 1, 2, 1, 1, 2, 1, 2, 1, 2 };

			// Act
			_gameLogic.CheckForGameWinner();

			// Assert
			Assert.IsFalse(_gameLogic.GameOver);
		}

		[TestMethod]
		public void CheckForGameWinner_ValidWinner_ReturnsTrue()
		{
			// Arrange
			_gameLogic.PickedRectangles = new int[] { 0, 0, 1, 0, 1, 0, 1, 0, 0 };

			// Act
			_gameLogic.CheckForGameWinner();

			// Assert
			Assert.IsTrue(_gameLogic.GameOver);
		}

		[TestMethod]
		public void IsValidMove_SquareHasNotBeenPicked_ReturnsTrue()
		{
			_gameLogic.PickedRectangles = new int[] { 0, 0, 1, 0, 1, 0, 1, 0, 0 };

			bool isValidMove = _gameLogic.IsValidMove(1);

			Assert.IsTrue(isValidMove);
		}

		[TestMethod]
		public void IsValidMove_SquareHasBeenPicked_ReturnsFalse()
		{
			_gameLogic.PickedRectangles = new int[] { 0, 0, 1, 0, 1, 0, 1, 0, 0 };

			bool isValidMove = _gameLogic.IsValidMove(2);

			Assert.IsFalse(isValidMove);
		}
	}
}
