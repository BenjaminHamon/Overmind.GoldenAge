using Overmind.Core;
using Overmind.GoldenAge.Model;
using System;
using System.IO;

namespace Overmind.GoldenAge.Console
{
	/// <summary>View element for a game. Prints the game board and information each time a new turn starts.</summary>
	public class GameView
	{
		public GameView(Game game, TextWriter output)
		{
			if (game == null)
				throw new ArgumentNullException("game", "[GameView.Constructor] Game must not be null.");
			if (output == null)
				throw new ArgumentNullException("output", "[GameView.Constructor] Output must not be null.");

			this.game = game;
			this.output = output;
			
			grid = new GridView<Entity>(output);
			if (output == System.Console.Out)
			{
				grid.PreWrite = entity => System.Console.ForegroundColor = entity.Owner.Color.Value.ToConsoleColor();
				grid.PostWrite = entity => System.Console.ResetColor();
			}

			game.TurnStarted += _ => Draw();
		}

		private readonly Game game;
		private readonly TextWriter output;
		private readonly GridView<Entity> grid;

		public Vector LookAt = new Vector(0, 0);

		private void Draw()
		{
			if (output == System.Console.Out)
				System.Console.Clear();

			grid.Width = System.Console.WindowWidth;
			grid.Height = System.Console.WindowHeight - 5;

			output.WriteLine();

			grid.Draw(LookAt, game.AllEntities, p => p.Position, p => p.ToShortString());
			//grid.Draw(1, game.BoardSize, 1, game.BoardSize, game.AllEntities, p => p.Position, p => p.ToShortString());

			output.WriteLine();
			output.WriteLine("Turn " + game.Turn);
			output.WriteLine(game.ActivePlayer + " is playing");
		}
	}
}
