using Overmind.Core;
using Overmind.GoldenAge.Model.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Overmind.GoldenAge.Model
{
	public class Player : IDisposable
	{
		public Player(Game game, string name, Color? color = null)
		{
			this.game = game;
			this.name = name;
			this.Color = color;
		}

		protected readonly Game game;
		private readonly string name;
		public readonly Color? Color;
		public string Name { get { return name; } }
		public int Score { get; protected set; }

		public IList<Resource> ResourceCollection = new List<Resource>();

		public IStrategy Strategy { get; set; }
		public IEnumerable<ICommand> CommandCollection;

		public TEntity FindEntity<TEntity>(Vector position)
			where TEntity : Entity
		{
			return EntityCollection.OfType<TEntity>().FirstOrDefault(p => p.Position == position);
		}

		public IList<Entity> EntityCollection = new List<Entity>();

		public event EventHandler<Player, Entity> EntityAdded;

		public void AddEntity(Entity entity)
		{
			EntityCollection.Add(entity);
			if (EntityAdded != null)
				EntityAdded(this, entity);
		}

		public override string ToString()
		{
			return String.Format("Player {0}", name);
		}

		protected virtual void Update()
		{
			//if (Strategy != null)
			//	Strategy.Update();
		}

		/// <summary>True if the player turn should end automatically.</summary>
		public virtual bool AutoEndTurn { get { return false; } }
		/// <summary>True if the player cannot do any more actions this turn.</summary>
		public bool IsTurnOver { get; protected set; }

		public void EndTurn()
		{
			Update();
			IsTurnOver = false;
			game.StartNewTurn();
		}

		public void Dispose()
		{
			if (Strategy is IDisposable)
				((IDisposable)Strategy).Dispose();
		}
	}
}
