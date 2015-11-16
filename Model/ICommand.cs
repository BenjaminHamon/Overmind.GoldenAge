using Overmind.Core;

namespace Overmind.GoldenAge.Model
{
	public interface ICommand
	{
		string Name { get; }
		void Execute(Vector position);
	}
}
