using Overmind.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Overmind.GoldenAge.Model.Entities
{
	public class City : Entity
	{
		public string Name;
		public int Population;

		public override string Symbol { get { return "C"; } }
	}
}
