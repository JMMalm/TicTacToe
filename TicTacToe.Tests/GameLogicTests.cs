using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Logic;

namespace TicTacToe.Tests
{
	[TestClass]
	public class GameLogicTests
	{
		[TestMethod]
		public void CheckForGameWinner_NoWinner_ReturnsFalse()
		{
			// Arrange
			GameLogic gameLogic = new GameLogic();
			gameLogic.CurrentPlayer = 1;
			gameLogic.PickedRectangles = new int[] { 1, 2, 1, 1, 2, 1, 2, 1, 2 };
			bool gameWon = false;

			// Act
			gameLogic.CheckForGameWinner();

			// Assert
			Assert.AreEqual(gameWon, gameLogic.GameOver);
		}

		[TestMethod]
		public void CheckForGameWinner_ValidWinner_ReturnsTrue()
		{
			// Arrange
			GameLogic gameLogic = new GameLogic();
			gameLogic.CurrentPlayer = 1;
			gameLogic.PickedRectangles = new int[] { 0, 0, 1, 0, 1, 0, 1, 0, 0 };
			bool gameWon = true;

			// Act
			gameLogic.CheckForGameWinner();

			// Assert
			Assert.AreEqual(gameWon, gameLogic.GameOver);
		}
	}
}
