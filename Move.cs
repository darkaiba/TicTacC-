using System;

public class Move
{
	public int Line { get; set; }
	public int Column { get; set; }

	public Move ()
	{
	}

	public Move (int line, int column)
	{
		Line = line;
		Column = column;
	}
}

