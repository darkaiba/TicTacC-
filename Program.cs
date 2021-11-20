using System;
using System.Text.RegularExpressions;

namespace IA1CS
{
	class Game
	{
		private int[,] state;

		public Game() {
			this.state = new int[3, 3];
		}

		public void Render() {
			Console.WriteLine ();
			Console.WriteLine ("    a   b   c");

			for (int i = 0; i < state.GetLength (0); i++) {
				Console.Write (i + 1);
				Console.Write ("  ");
				Console.Write (String.Format(" {0} ", GetSymbol (this.state [i, 0])));

				for (int j = 1; j < state.GetLength (1); j++) {
					Console.Write ('|');
					Console.Write (String.Format(" {0} ", GetSymbol (this.state [i, j])));
				}
				Console.WriteLine ();

				if (i < 2) {
					Console.WriteLine ("   -----------");
				}
			}
			Console.WriteLine ();
		}

		private char GetSymbol (int val) {
			if (val < 0)
				return 'O';
			else if (val == 0)
				return ' ';
			else
				return 'X';
		}

		public static void Main (string[] args)
		{
			Game game = new Game ();
			game.Render ();
			Console.WriteLine ("Jogo da Velha!");
			Console.WriteLine ("======================");
			int difficulty = -1;
			
			while(difficulty < 0 || difficulty > 3){
				Console.WriteLine ("Escolha a Diiculdade ");
				Console.WriteLine ("0 - Eu quero minha mãe ");
				Console.WriteLine ("1 - Ainda mijo na cama:");
				Console.WriteLine ("2 - I'm Batman! ");
				Console.WriteLine ("3 - Birl! Chuck Norris!");
				Console.WriteLine ();
				Console.Write("Difficulty: ");
				difficulty = Console.ReadLine ();
			}
			Console.WriteLine ("======================");
			Console.WriteLine ();
			int turn = -1;
			while(turn < 0 || turn > 1){
				Console.Write ("Se voce quer iniciar digite 0, senão 1: ");
				turn = Console.ReadLine ();
			}
			Console.WriteLine ("======================");
			Console.WriteLine ();
			AI ai = new AI (game.state, -1);
			ai.setDifficulty (difficulty);
			AI ai2 = new AI (game.state, 1);
			ai2.setDifficulty (difficulty);
			IPlayer[] players = new IPlayer[2];
			
			if(turn = 1){
				players [1] = new Human(game.state, 1);
				players [0] = ai;
			}else{
				players [0] = new Human(game.state, 1);
				players [1] = ai;
			}
			

			int i = 0;

			int? status;

			while ((status = AI.CheckVictory (game.state)) == null) {
				players [i].Play ();
				i = (i + 1) % 2;
				game.Render ();
			}

			if (status == -1) {
				Console.WriteLine ("Game Over, Patinho!");
			}

			if (status == 1) {
				Console.WriteLine ("You Won!");
			}

			if (status == 0) {
				Console.WriteLine ("Draw!");
			}
		}
	}
}
