using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Overmind.GoldenAge.Model
{
	public class Terrain
	{
		public Terrain(float textureOffset)
		{
			this.TextureOffset = textureOffset;
		}

		public readonly float TextureOffset;
	}
}
