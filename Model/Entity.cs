using Overmind.Core;
using System;
using System.Collections.Generic;

namespace Overmind.GoldenAge.Model
{
	/// <summary>
	/// Base class for a game entity. It belongs to a player and is placed in the game world.
	/// </summary>
	public abstract class Entity
	{
		protected Entity(Player owner, Vector position)
		{
			this.Owner = owner;
			this.position = position;
		}

		public readonly Player Owner;

		private Vector position;
		public Vector Position
		{
			get { return position; }
			set
			{
				position = value;
				if (Moved != null)
					Moved(this);
			}
		}

		public IEnumerable<ICommand> CommandCollection;

		public override string ToString() { return String.Format("Entity Owner=[{0}] Position={1}", Owner, Position); }

		public abstract string ToShortString();

		public virtual string Image
		{
			get
			{
				if (Owner.Color == null)
					return GetType().Name;
				return Owner.Color.Value.Name + GetType().Name;
			}
		}

		public event Core.EventHandler<Entity> Moved;

		public event Core.EventHandler<Entity> Destroyed;

		public void Destroy()
		{
			Owner.EntityCollection.Remove(this);
			if (Destroyed != null)
				Destroyed(this);
		}
	}
}
