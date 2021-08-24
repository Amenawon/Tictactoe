using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Core.Interface;
using TicTacToe.Core.Model;

namespace TicTacToe.Core.Service
{
    public class GameLogic : IGameLogic
    {
        char player = 'o', opponent = 'x';

        public string PlayGame(string boardInput)
        {
            char[,] board = BuildInputBoard(boardInput);
            Move bestMove = FindBestMove(board);

            board[bestMove.row, bestMove.col] = player;

            var output = BuildOutputBoard(board);

            return output;
		}
		// Convert string board to 2d char array board
		private char[,] BuildInputBoard(string input)
		{
			char[,] board = {{ input[0], input[1], input[2] },
					{ input[3], input[4], input[5] },
					{ input[6], input[7], input[8] }};

			return board;
		}

		private Move FindBestMove(char[,] board)
		{
			int bestVal = -1000;
			Move bestMove = new Move();
			bestMove.row = -1;
			bestMove.col = -1;

			// Traverse all cells, evaluate minimax function
			// for all empty cells. And return the cell
			// with optimal value.
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					// Check if cell is empty
					if (board[i, j] == ' ')
					{
						// Make the move
						board[i, j] = player;

						
						int moveVal = Minimax(board, 0, false);

						board[i, j] = ' ';

						if (moveVal > bestVal)
						{
							bestMove.row = i;
							bestMove.col = j;
							bestVal = moveVal;
						}
					}
				}
			}

			Console.Write("The value of the best Move " +
								"is : {0}\n\n", bestVal);

			return bestMove;
		}

		private int Minimax(char[,] board,
					int depth, Boolean isMax)
		{
			int score = EvaluateWinner(board);

			// If Maximizer has won the game
			// return his/her evaluated score
			if (score == 10)
				return score;

			// If Minimizer has won the game
			// return his/her evaluated score
			if (score == -10)
				return score;

			// If there are no more moves and
			// no winner then it is a tie
			if (IsMovesLeft(board) == false)
				return 0;

			// If this maximizer's move
			if (isMax)
			{
				int best = -1000;

				// Traverse all cells
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						// Check if cell is empty
						if (board[i, j] == ' ')
						{
							// Make the move
							board[i, j] = player;

							// Call minimax recursively and choose
							// the maximum value
							best = Math.Max(best, Minimax(board,
											depth + 1, !isMax));

							// Undo the move
							board[i, j] = ' ';
						}
					}
				}
				return best;
			}

			// If this minimizer's move
			else
			{
				int best = 1000;

				// Traverse all cells
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						// Check if cell is empty
						if (board[i, j] == ' ')
						{
							// Make the move
							board[i, j] = opponent;

							// Call minimax recursively and choose
							// the minimum value
							best = Math.Min(best, Minimax(board,
											depth + 1, !isMax));

							// Undo the move
							board[i, j] = ' ';
						}
					}
				}
				return best;
			}
		}
		private int EvaluateWinner(char[,] b)
		{
			// Checking for Rows for X or O victory.
			for (int row = 0; row < 3; row++)
			{
				if (b[row, 0] == b[row, 1] &&
					b[row, 1] == b[row, 2])
				{
					if (b[row, 0] == player)
						return +10;
					else if (b[row, 0] == opponent)
						return -10;
				}
			}

		

			// Checking for Columns for X or O victory.
			for (int col = 0; col < 3; col++)
			{
				if (b[0, col] == b[1, col] &&
					b[1, col] == b[2, col])
				{
					if (b[0, col] == player)
						return +10;

					else if (b[0, col] == opponent)
						return -10;
				}
			}

			// Checking for Diagonals for X or O victory.
			if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
			{
				if (b[0, 0] == player)
					return +10;
				else if (b[0, 0] == opponent)
					return -10;
			}

			if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
			{
				if (b[0, 2] == player)
					return +10;
				else if (b[0, 2] == opponent)
					return -10;
			}

			// Else if none of them have won then return 0
			return 0;
		}
		private bool IsMovesLeft(char[,] board)
		{
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					if (board[i, j] == ' ')
						return true;
			return false;
		}
		private string BuildOutputBoard(char[,] input)
		{
			var output = string.Empty;

			for (int j = 0; j < input.GetLength(0); j++)
			{
				for (int i = 0; i < input.GetLength(1); i++)
					output = string.Concat(output, input[j, i].ToString());
			}

			return output;
		}


	}
}
