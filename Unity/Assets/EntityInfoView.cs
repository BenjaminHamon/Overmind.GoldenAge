using Overmind.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.GoldenAge.Unity
{
	public class EntityInfoView : MonoBehaviourBase
	{
		public EntityView Entity { get; private set; }

		public void SetEntity(EntityView entity, PlayerView activePlayer)
		{
			this.Entity = entity;
		}

		public override void Update()
		{
			if (Entity != null)
			{
				NameText.text = Entity.name;
			}
			else
			{
				NameText.text = null;
			}
		}

		[SerializeField]
		private Text NameText;
	}
}
