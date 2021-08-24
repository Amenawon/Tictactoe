using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Core.Interface;

namespace TicTacToe.API.Controllers
{
    [ApiController]

    public class TictactoeController : Controller
    {
		readonly IGameLogic _gameLogic;

		public TictactoeController(IGameLogic gameLogic)
        {
			_gameLogic = gameLogic;
        }

        [HttpGet("play")]
        public IActionResult PlayGame(string board)
        {
			if (board !=null && board.Any(c => "+%20".Contains(c)))
				board = board.Replace("+", " ").Replace("%20", " ");

			if (!IsBoardValid(board))
				return BadRequest();

			var result = _gameLogic.PlayGame(board);

            return Ok(result);
        }
		private  bool IsBoardValid(string board)
		{
			
			// Board string has valid length.
			if (board == null || board.Length != 9)
			{
				return false;
			}
			// Board string has valid characters.
			if (!board.All(c => "xo ".Contains(c)))
			{
				return false;
			}

			// Is o's turn.
			int xCount = board.Count(character => character == 'x');
			int oCount = board.Count(character => character == 'o');
			if (!(xCount - 1 == oCount || xCount == oCount))
			{
				return false;
			}
			// Board is not full.
			if (IsFull(board))
			{
				return false;
			}

			return true;
		}
		private bool IsFull(string board)
		{
			return !board.Contains(" ");
		}


	}
}
