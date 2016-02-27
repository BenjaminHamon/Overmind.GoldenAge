using Overmind.Core;
using System;
using System.Collections.Generic;

namespace Overmind.GoldenAge.Model.Entities
{
	/// <summary>
	/// Base class for a game entity, any object which can be placed in the game world.
	/// </summary>
	public abstract class Entity
	{
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

		public override string ToString() { return String.Format("{0} (Position: {1})", GetType().Name, Position); }

		public abstract string Symbol { get; }

		//public virtual string Image
		//{
		//	get
		//	{
		//		if (Owner.Color == null)
		//			return GetType().Name;
		//		return Owner.Color.Value.Name + GetType().Name;
		//	}
		//}

		public event Core.EventHandler<Entity> Moved;

		public event Core.EventHandler<Entity> Destroyed;

		public void Destroy()
		{
			if (Destroyed != null)
				Destroyed(this);
		}
	}
}
