namespace Overmind.GoldenAge.Model
{
	public class Terrain
	{
		public Terrain(string name, float textureOffset)
		{
			this.Name = name;
			this.TextureOffset = textureOffset;
		}

		public readonly string Name;
		public readonly float TextureOffset;
	}
}
