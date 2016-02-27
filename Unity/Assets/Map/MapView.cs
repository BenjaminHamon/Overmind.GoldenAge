using Overmind.GoldenAge.Model;
using Overmind.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Overmind.GoldenAge.Unity.Map
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshCollider))]
	public class MapView : MonoBehaviourBase
	{
		public void Initialize(Model.Map map)
		{
			//if (map == null)
			//	throw new ArgumentNullException("map");

			//this.map = map;

			BuildMap();
		}

		private Model.Map map;

		[SerializeField]
		private GameView game;

		[SerializeField]
		private Texture2D sourceImage;
		
		private new Transform transform;
		private new Collider collider;
		[SerializeField]
		private new Camera camera;

		public override void Start()
		{
			transform = base.transform;
			collider = GetComponent<MeshCollider>();
		}

		public void OnMouseDown()
		{
			game.EntityInfo.Entity = null;
		}

		public void BuildMap()
		{
			if (sourceImage != null)
			{
				IDictionary<string, Model.Terrain> terrainDictionary = new Dictionary<string, Model.Terrain>()
				{
					{ "sea", new Model.Terrain("Sea", 0) },
					{ "plain", new Model.Terrain("Plain", 1) },
					{ "mountain", new Model.Terrain("Moutain", 3) },
				};

				IList<MapTile> tileCollection = sourceImage.GetPixels32().Select(color => {
					if (color == Color.blue) return new MapTile(terrainDictionary["sea"]);
					if (color == Color.green) return new MapTile(terrainDictionary["plain"]);
					if (color.Equals((Color32)Color.gray)) return new MapTile(terrainDictionary["mountain"]);
					return new MapTile(new Model.Terrain("NULL", 0));
				}).ToList();

				map = new Model.Map(sourceImage.width, sourceImage.height, tileCollection);
			}
			else
			{
				IList<MapTile> tileCollection = new List<MapTile>(100 * 100);
				for (int i = 0; i < 100 * 100; i++)
					tileCollection.Add(new MapTile(new Model.Terrain("NULL", 0)));
				map = new Model.Map(100, 100, tileCollection);
			}

			BuildMesh();
		}

		private void BuildMesh()
		{
			Vector3[] vertices = new Vector3[map.Width * map.Height * 4];
			int[] triangles = new int[map.Width * map.Height * 2 * 3];
			Vector2[] uv = new Vector2[map.Width * map.Height * 4];

			for (int z = 0; z < map.Height; z++)
			{
				for (int x = 0; x < map.Width; x++)
				{
					int tileOffset = z * map.Width + x;
					int vertexOffset = tileOffset * 4;

					vertices[vertexOffset + 0] = new Vector3(x,     0, z    );
					vertices[vertexOffset + 1] = new Vector3(x + 1, 0, z    );
					vertices[vertexOffset + 2] = new Vector3(x,     0, z + 1);
					vertices[vertexOffset + 3] = new Vector3(x + 1, 0, z + 1);

					MapTile tile = map.GetTile(x, z);
					uv[vertexOffset + 0] = new Vector2(tile.Terrain.TextureOffset / 4, 0);
					uv[vertexOffset + 1] = new Vector2((tile.Terrain.TextureOffset + 1) / 4, 0);
					uv[vertexOffset + 2] = new Vector2(tile.Terrain.TextureOffset / 4, 1f);
					uv[vertexOffset + 3] = new Vector2((tile.Terrain.TextureOffset + 1) / 4, 1f);

					int triangleOffset = tileOffset * 6;

					triangles[triangleOffset + 0] = vertexOffset;
					triangles[triangleOffset + 1] = vertexOffset + 2;
					triangles[triangleOffset + 2] = vertexOffset + 3;

					triangles[triangleOffset + 3] = vertexOffset;
					triangles[triangleOffset + 4] = vertexOffset + 3;
					triangles[triangleOffset + 5] = vertexOffset + 1;
				}
			}
			
			Mesh mesh = new Mesh()
			{
				name = "MapMesh",
				vertices = vertices,
				triangles = triangles,
				uv = uv,
			};
			mesh.RecalculateNormals();
			GetComponent<MeshFilter>().mesh = mesh;
			GetComponent<MeshCollider>().sharedMesh = mesh;
		}

		public MapTile GetTargetTile()
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (collider.Raycast(ray, out raycastHit, Single.PositiveInfinity))
			{
				Vector3 point = transform.worldToLocalMatrix.MultiplyPoint(raycastHit.point);
				return map.GetTile(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.z));
			}
			return null;
		}
	}
}


/*
Vector3[] vertices = new Vector3[(Map.Width + 1) * (Map.Height + 1)];
			// Triangle vertex count = tile count * triangle count per tile * vertice count per triangle
			int[] triangles = new int[Map.Width * Map.Height * 2 * 3];
			Vector3[] normals = new Vector3[(Map.Width + 1) * (Map.Height + 1)];
			Vector2[] uv = new Vector2[(Map.Width + 1) * (Map.Height + 1)];

			for (int z = 0; z < (Map.Height + 1); z++)
			{
				for (int x = 0; x < (Map.Width + 1); x++)
				{
					vertices[z * (Map.Width + 1) + x] = new Vector3(x, 0, z);
					normals[z * (Map.Width + 1) + x] = Vector3.up;
					uv[z * (Map.Width + 1) + x] = new Vector2((float)x / Map.Width, (float)z / Map.Height);
				}
			}

			for (int z = 0; z < Map.Height; z++)
			{
				for (int x = 0; x < Map.Width; x++)
				{
					int triangleOffset = (z * Map.Width + x) * 6;
					int triangleOrigin = z * (Map.Width + 1) + x;

					triangles[triangleOffset + 0] = triangleOrigin;
					triangles[triangleOffset + 1] = triangleOrigin + (Map.Width + 1);
					triangles[triangleOffset + 2] = triangleOrigin + (Map.Width + 1) + 1;

					triangles[triangleOffset + 3] = triangleOrigin;
					triangles[triangleOffset + 4] = triangleOrigin + (Map.Width + 1) + 1;
					triangles[triangleOffset + 5] = triangleOrigin + 1;
				}
			}
*/