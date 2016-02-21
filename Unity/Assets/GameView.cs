using Overmind.Core;
using Overmind.GoldenAge.Model;
using Overmind.GoldenAge.Unity.Map;
using Overmind.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.GoldenAge.Unity
{
	public class GameView : MonoBehaviourBase
	{
		public override void Awake()
		{
			UnityApplication.Initialize();
		}

		private Game game;
		private ContentLoader contentLoader;

		public override void Start()
		{
			game = new Game();
			map.Initialize(game.Map);

			//foreach (Player player in game.PlayerCollection)
			//{
			//	PlayerView playerView = Instantiate(PlayerViewPrefab).GetComponent<PlayerView>();
			//	playerView.gameObject.SetActive(false);
			//	playerView.Initialize(player, map);
			//	playerView.transform.SetParent(PlayerViewGroup, false);
			//	playerViewCollection.Add(playerView);
			//}

			game.TurnStarted += OnTurnStarted;
			game.Start();

			/*contentLoader = new ContentLoader("file:///E:/Projects/Overmind/Games/Mods");
			DependencyReadyStatus.Add(contentLoader, false);
			contentLoader.Ready += OnDependencyReady;
			StartCoroutine(contentLoader.LoadAssetBundleAsync(Mod));*/
			//DoStart();
		}

		private readonly IDictionary<object, bool> DependencyReadyStatus = new Dictionary<object, bool>();

		private void OnDependencyReady(IDependency dependency)
		{
			dependency.Ready -= OnDependencyReady;
			DependencyReadyStatus[dependency] = true;
			if (DependencyReadyStatus.Values.All(status => status))
				DoStart();
		}

		private void DoStart()
		{
		}
		
		[SerializeField]
		private PlayerView playerView;

		private void CreateEntityView(Player player, Entity entity)
		{
			//EntityView entityView = Instantiate(EntityPrefab).GetComponent<EntityView>();
			//entityView.Initialize(this, playerViewCollection.First(playerView => playerView.Player == player), entity, null);// contentLoader.GetAssetBundle(Mod));
			//entityView.transform.SetParent(GetCell(entity.Position).transform, false);
		}

		[SerializeField]
		private MapView map;

		[SerializeField]
		private Text TurnText;
		[SerializeField]
		private EntityInfoView EntityInfo;

		private void OnTurnStarted(Game sender)
		{
			playerView.SetPlayer(game.ActivePlayer);
		}
	}
}
