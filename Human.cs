using System;
using System.Text.RegularExpressions;

namespace IA1CS
{
	public class Human : IPlayer
	{
		public int[,] State { get; set; }
		public int Player { get; set; }
		public string Name { get; set; }

		public Human (int[,] state, int player)
		{
			this.State = state;
			this.Player = player;
			Name = "Human: " + player;
		}

		public void Play()
		{
			Console.Write ("Move: ");
			Move move = ParseMove (Console.ReadLine ());

			State [move.Line, move.Column] = this.Player;
		}

		private Move ParseMove(string move)
		{
			Regex regex = new Regex (@"([a-c])([1-3])");
			Match m = regex.Match (move);
			Move moveResult = new Move();

			if (!m.Success) {
				return null;
			}

			moveResult.Column = m.Groups [1].ToString ().ToCharArray () [0] - 'a';
			moveResult.Line = m.Groups [2].ToString ().ToCharArray () [0] - '1';

			return moveResult;
		}
	}
}

