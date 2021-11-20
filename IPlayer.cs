using System;

namespace IA1CS
{
	public interface IPlayer
	{
		int Player { get; set; }
		int[,] State { get; set; }
		string Name { get; set; }

		void Play();
	}
}

