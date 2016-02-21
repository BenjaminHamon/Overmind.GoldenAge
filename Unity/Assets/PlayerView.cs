using Overmind.GoldenAge.Model;
using Overmind.GoldenAge.Unity.Map;
using Overmind.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.GoldenAge.Unity
{
	public class PlayerView : MonoBehaviourBase
	{
		private Player player;

		public void SetPlayer(Player player)
		{
			this.player = player;
			UpdateResources();
		}

		[SerializeField]
		private MapView map;

		public override void Update()
		{
			if (Input.GetMouseButtonUp(0))
			{
				MapTile tile = map.GetTargetTile();
				if (tile != null)
				{
					tile.Owner = player;
				}
			}
		}

		private void UpdateResources()
		{
			IList<GameObject> childCollection = new List<GameObject>();
			foreach (GameObject child in childCollection)
				childCollection.Add(child);
			foreach (GameObject child in childCollection)
				Destroy(child);

			foreach (Resource resource in player.ResourceCollection)
			{
				ResourceView resourceView = Instantiate(ResourcePrefab).GetComponent<ResourceView>();
				resourceView.transform.SetParent(ResourceGroup, false);
				resourceView.SetResource(resource);
			}
		}

		[SerializeField]
		private Transform ResourceGroup;
		[SerializeField]
		private GameObject ResourcePrefab;

		[SerializeField]
		private Button EndTurnButton;
	}
}
