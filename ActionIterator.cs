using System.Collections;
using System.Collections.Generic;

public class ActionIterator
{
	private int[,] estado;
	private List<Move> vazios;
	private int jogada;
	private int player;

	public ActionIterator(int[,] estado, List<Move> vazios, int player) {
		this.estado = estado;
		this.vazios = vazios;
		this.player = player;
		this.jogada = 0;
	}

	public bool next() {
		this.jogada++;

		return this.jogada < this.vazios.Count;
	}

	public List<Move> getVazios() {
		List<Move> ret = new List<Move> (vazios);
		ret.Remove (ret [this.jogada]);
		return ret;
	}

	public int[,] getEstado() {
		int[,] estado = (int[,]) this.estado.Clone();
		Move p = vazios [this.jogada];
		estado [p.Line, p.Column] = this.player;
		return estado;
	}

	public Move getMove() {
		return vazios [this.jogada];
	}
}

