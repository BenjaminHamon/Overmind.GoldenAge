﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Overmind.GoldenAge.Model
{
	public class MapTile
	{
		public MapTile(Terrain terrain)
		{
			this.Terrain = terrain;
		}

		public readonly Terrain Terrain;
		public Player Owner;
	}
}
