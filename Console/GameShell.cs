using Overmind.Core;
using Overmind.GoldenAge.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Overmind.GoldenAge.Console
{
	/// <summary>Console user interface for a game.</summary>
	public class GameShell : Shell
	{
		public GameShell()
		{
			commandInterpreter.RegisterCommand("play", _ => Play());
		}

		private Game game;
		private GameView gameView;

		protected override void Initialize(IList<string> arguments)
		{
			game = new Game(new Map(10, 10));
			game.AddPlayer(new Player(game, "You"));
			gameView = new GameView(game, Output);
			game.Start();
		}

		private void Play()
		{
			Vector source = ReadVector("Source");
			Entity entity = game.ActivePlayer.FindEntity<Entity>(source);
			if (entity == null)
				throw new Exception("No entity found");

			string commandName = Read("Command");
			ICommand command = entity.CommandCollection.First(c => c.Name == commandName);

			Vector destination = ReadVector("Destination");
			command.Execute(destination);
			game.ActivePlayer.EndTurn();
		}

		private Vector ReadVector(string message)
		{
			IList<string> arguments = Read(message).Split(new char[] { ' ' });
			return new Vector(Double.Parse(arguments[0]), Double.Parse(arguments[1]));
		}

		protected override void Exit()
		{
			game.Dispose();
			if (Output == System.Console.Out)
				System.Console.Clear();

			base.Exit();
		}
	}
}
