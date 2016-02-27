using Overmind.Core;
using Overmind.GoldenAge.Model;
using Overmind.GoldenAge.Model.Entities;
using Overmind.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.GoldenAge.Unity.Entities
{
	public class EntityView : MonoBehaviourBase
	{
		public void Initialize(GameView game, Entity entity)
		{
			this.game = game;
			this.Entity = entity;

			name = entity.GetType().Name;
			transform.localPosition = ToUnityVector3(entity.Position);

			entity.Moved += OnMoved;
			entity.Destroyed += OnDestroyed;
		}

		private GameView game;
		public Entity Entity { get; private set; }

		private void OnMoved(Entity sender)
		{
			transform.localPosition = ToUnityVector3(Entity.Position);
		}

		private void OnDestroyed(Entity sender)
		{
			Entity.Moved -= OnMoved;
			Entity.Destroyed -= OnDestroyed;

			Destroy(gameObject);
		}

		public void OnMouseDown()
		{
			game.EntityInfo.Entity = this;
		}


		public static Vector3 ToUnityVector3(Vector vector)
		{
			switch (vector.Count)
			{
				case 1: return new Vector3(Convert.ToSingle(vector[0]), 0, 0);
				case 2: return new Vector3(Convert.ToSingle(vector[0]), Convert.ToSingle(vector[1]), 0);
				case 3: return new Vector3(Convert.ToSingle(vector[0]), Convert.ToSingle(vector[1]), Convert.ToSingle(vector[2]));
				default: throw new ArgumentException("Vector must have 3 dimensions at maximum", "vector");
			}
		}
	}
}
