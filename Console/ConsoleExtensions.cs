using System;
using System.Drawing;

namespace Overmind.GoldenAge.Console
{
	/// <summary>Helpers and extensions for System.Console.</summary>
	public static class ConsoleExtensions
	{
		/// <summary>Converts a <see cref="Color"/> to a <see cref="ConsoleColor"/>.</summary>
		/// <exception cref="Exception">Thrown if no corresponding ConsoleColor exists or if this color conversion is not implemented.</exception>
		public static ConsoleColor ToConsoleColor(this Color color)
		{
			if (color == Color.Black)
				return ConsoleColor.Red;
			if (color == Color.White)
				return ConsoleColor.Yellow;
			throw new Exception("Cannot convert " + color + " to ConsoleColor");
		}
	}
}
