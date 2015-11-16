using Overmind.Core;
using System.Collections.Generic;

namespace Overmind.GoldenAge.Model
{
	public class Map
	{
		public Map(int width, int height)
		{
			this.Width = width;
			this.Height = height;

			tileCollection = new List<MapTile>(Width * Height);

			for (int i = 0; i < Width * Height; i++)
			{
				tileCollection.Add(new MapTile());
			}
		}

		public readonly int Width;
		public readonly int Height;

		private readonly IList<MapTile> tileCollection;

		public MapTile GetTile(int column, int row)
		{
			if ((column < 0) || (column >= Width) || (row < 0) || (row > Height))
				throw new OvermindException("[Map.GetTile] Tile coordinates are out of bounds.");
			return tileCollection[row * Width + column];
		}
	}
}
