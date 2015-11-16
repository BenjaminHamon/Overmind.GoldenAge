using Overmind.GoldenAge.Model;
using Overmind.GoldenAge.Unity.Map;
using Overmind.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.GoldenAge.Unity
{
	public class PlayerView : MonoBehaviourBase
	{
		public void Initialize(Player player, MapView map)
		{
			if (player == null)
				throw new ArgumentNullException("player");
			this.Player = player;

			if (map == null)
				throw new ArgumentNullException("map");
			this.map = map;

			name = "Player " + player.Name;
		}

		public Player Player { get; private set; }
		private MapView map;

		public override void Update()
		{
			if (Input.GetMouseButtonUp(0))
			{
				MapTile tile = map.GetTargetTile();
				if (tile != null)
				{
					tile.Owner = Player;
				}
			}
		}

		[SerializeField]
		private Button EndTurnButton;
	}
}
