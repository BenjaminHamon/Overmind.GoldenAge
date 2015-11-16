using Overmind.GoldenAge.Model;
using Overmind.GoldenAge.Unity.Map;
using Overmind.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.GoldenAge.Unity
{
	public class TileInfoView : MonoBehaviourBase
	{
		private MapTile currentTile;
		
		[SerializeField]
		private MapView map;

		[SerializeField]
		private Text ownerText;

		public override void Start()
		{
			ResetInfo();
		}

		public override void Update()
		{
			MapTile tile = map.GetTargetTile();
			if (currentTile != tile)
			{
				currentTile = tile;

				if (currentTile != null)
					UpdateInfo();
				else
					ResetInfo();
			}
		}

		private void UpdateInfo()
		{
			ownerText.text = (currentTile.Owner != null) ? String.Format("Owned by " + currentTile.Owner.Name) : null;
		}

		private void ResetInfo()
		{
			ownerText.text = null;
		}
	}
}