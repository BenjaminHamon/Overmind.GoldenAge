using Overmind.GoldenAge.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Overmind.GoldenAge.Console
{
	/// <summary>Main shell for the console application itself.</summary>
	public class ApplicationShell : Shell
	{
		public ApplicationShell()
		{
			commandInterpreter.RegisterCommand("play", args => new GameShell().Run(args));
		}

		protected override void Help(IList<string> arguments)
		{
			if (arguments.Count == 1)
				base.Help(arguments);
			else
			{
				string command = arguments[1];
				string helpFilePath = Path.Combine("Help", command + ".txt");

				if (File.Exists(helpFilePath) == false)
					Output.WriteLine("No help page found for: " + command);
				else
				{
					if (Output == System.Console.Out)
						System.Console.Clear();
					Output.WriteLine(File.ReadAllText(helpFilePath));
				}
			}
		}
	}
}
