using Overmind.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Overmind.GoldenAge.Model
{
	[DataContract]
	public class Game : IDisposable
	{
		public Game()
		{
			Map = new Map(100, 100);

			Player player = new Player(this, "you");
			player.ResourceCollection.Add(new Resource() { Name = Resource.Gold, Quantity = 100 });

			AddPlayer(player);
		}

		public Game(Map map)
		{
			this.Map = map;
		}

		public readonly Map Map;

		public void Start()
		{
			Turn = 1;
			ActivePlayer = playerCollection.First();
			if (TurnStarted != null)
				TurnStarted(this);
		}

		public void AddPlayer(Player player)
		{
			playerCollection.Add(player);
		}

		private readonly List<Player> playerCollection = new List<Player>();
		public IEnumerable<Player> PlayerCollection { get { return playerCollection.AsReadOnly(); } }
		public Player ActivePlayer { get; private set; }

		public TEntity FindEntity<TEntity>(Vector position)
			where TEntity : Entity
		{
			foreach (Player player in PlayerCollection)
			{
				TEntity entity = player.FindEntity<TEntity>(position);
				if (entity != null)
					return entity;
			}

			return null;
		}

		public IEnumerable<Entity> AllEntities
		{
			get 
			{
				return playerCollection.Select(player => player.EntityCollection)
					.Cast<IEnumerable<Entity>>().Aggregate((first, second) => first.Concat(second));
			}
		}

		public int Turn { get; private set; }

		internal void StartNewTurn()
		{
			Turn++;
			ActivePlayer = playerCollection.ElementAtOrDefault(playerCollection.IndexOf(ActivePlayer) + 1) ?? playerCollection.First();
			if (TurnStarted != null)
				TurnStarted(this);
		}

		public event Core.EventHandler<Game> TurnStarted;

		[DataMember]
		private IEnumerable<string> CommandHistory;

		public void Dispose()
		{
			foreach (Player player in playerCollection)
				player.Dispose();
		}
	}
}
