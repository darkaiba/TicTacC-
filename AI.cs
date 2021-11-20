using System;
using System.Collections;
using System.Collections.Generic;
using IA1CS;

public class AI : IPlayer
{
	int[,] matriz;
	int maxDepth;
	int player;
	int currentMaxDepth;

	public string Name { get; set; }

	public int[,] State {
		get { return matriz; }
		set { matriz = value; }
	}

	public int Player {
		get { return player; }
		set { player = value; }
	}

	public AI(int[,] matriz, int player){
		this.matriz = matriz;
		this.maxDepth = 1;
		this.player = player;

		Name = String.Format("AI: {0}", player);
	}

	public int[,] getMatriz(){
		return this.matriz;
	}

	public int getPlayer(){
		return this.player;
	}

	public void setMatriz(int[,] matriz){
		this.matriz = matriz;
	}

	public void setDifficulty(int difficulty){
		this.maxDepth = 1 + difficulty * 3;
	}

	public void setMatriz(int player){
		this.player = player;
	}

	private int HeuristicValue(int[] line)
	{
		int plus = 0;
		int minus = 0;

		for (int i = 0; i < line.Length; i++) {
			if (line [i] > 0)
				plus++;
			else if (line [i] < 0)
				minus++;
		}

		if (minus * plus != 0)
			return 0;

		return plus - minus;
	}

	private int? Heuristic(int[,] state, int depth, int emptySize)
	{
		int? end = CheckVictory (state);

		if (end != null)
		{
			return (int) Math.Exp (10) * end;
		}

		if (depth < currentMaxDepth)
		{
			return null;
		}

		int res = 0;

		int[] d1 = new int[3];
		int[] d2 = new int[3];
		int[] linha = new int[3];
		int[] coluna = new int[3];

		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				linha [j] = state [i, j];
				coluna [j] = state [j, i];
			}

			res += HeuristicValue (linha);
			res += HeuristicValue (coluna);

			d1 [i] = state [i, i];
			d2 [i] = state [i, 2 - i];
		}

		res += HeuristicValue (d1);
		res += HeuristicValue (d2);

		return res;
	}

	public static int? CheckVictory(int[,] state)
	{
		int d1 = 0;
		int d2 = 0;
		int used = 0;

		for (int i = 0; i < state.GetLength (0); i++) {
			int line = 0;
			int column = 0;

			for (int j = 0; j < state.GetLength (1); j++) {
				line += state [i, j];
				column += state [j, i];

				if (state [i, j] != 0)
					used++;
			}

			d1 += state [i, i];
			d2 += state [i, 2 - i];

			if (Math.Abs (line) == 3)
				return line / Math.Abs (line);
			if (Math.Abs (column) == 3)
				return column / Math.Abs (column);
		}

		if (Math.Abs (d1) == 3)
			return d1 / Math.Abs (d1);
		if (Math.Abs (d2) == 3)
			return d2 / Math.Abs (d2);

		if (used == 9)
			return 0;
		return null;
	}

	private int Max(int[,] state, List<Move> empty, int player, int alpha, int beta, int depth) {
		int? resultN = Heuristic (state, depth, empty.Count);

		if (resultN != null) {
			return this.player * (int) resultN;
		}

		int v;
		ActionIterator it = new ActionIterator (state, empty, player);

		v = Min (it.getEstado (), it.getVazios (), -player, alpha, beta, depth + 1);

		while (it.next ()) {
			int nv = Min (it.getEstado (), it.getVazios (), -player, alpha, beta, depth + 1);

			if (nv > v) {
				v = nv;
			}

			if (v > beta) {
				return v;
			}

			if (alpha < v) {
				alpha = v;
			}
		}

		return v;
	}

	private int Min(int[,] state, List<Move> empty, int player, int alpha, int beta, int depth) {
		int? resultN = Heuristic (state, depth, empty.Count);

		if (resultN != null) {
			return this.player * (int) resultN;
		}

		int v;
		ActionIterator it = new ActionIterator (state, empty, player);

		v = Max (it.getEstado (), it.getVazios (), -player, alpha, beta, depth + 1);

		while (it.next ()) {
			int nv = Max (it.getEstado (), it.getVazios (), -player, alpha, beta, depth + 1);

			if (nv < v) {
				v = nv;
			}

			if (v < alpha) {
				return v;
			}

			if (beta > v) {
				beta = v;
			}
		}

		return v;
	}

	public Move GetBestMove() {
		List<Move> empty = new List<Move> ();
		int[,] state = (int[,]) this.matriz.Clone ();

		for (var i = 0; i < 3; i++) {
			for (var j = 0; j < 3; j++) {
				if (state [i, j] == 0) {
					Move pos = new Move ();
					pos.Line = i;
					pos.Column = j;
					empty.Add (pos);
				}
			}
		}

		currentMaxDepth = maxDepth * empty.Count / 9;
		currentMaxDepth = currentMaxDepth < 1 ? 1 : currentMaxDepth;

		ActionIterator it = new ActionIterator (state, empty, this.player);
		int v = Min (it.getEstado (), it.getVazios (), -player, int.MinValue, int.MaxValue, 1);
		Move best = it.getMove ();

		while (it.next ()) {
			int nv = Min (it.getEstado (), it.getVazios (), -player, int.MinValue, int.MaxValue, 1);

			if (nv > v) {
				v = nv;
				best = it.getMove ();
			}
		}

		return best;
	}

	public void Play() {
		Move m = GetBestMove ();
		this.State [m.Line, m.Column] = this.Player;
	}
}
