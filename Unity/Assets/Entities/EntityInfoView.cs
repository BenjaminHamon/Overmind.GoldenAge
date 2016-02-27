using Overmind.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.GoldenAge.Unity.Entities
{
	public class EntityInfoView : MonoBehaviourBase
	{
		public EntityView Entity;
		
		[SerializeField]
		private Text HeaderText;

		public override void Start()
		{
			ResetInfo();
		}

		public override void Update()
		{
			if (Entity != null)
				UpdateInfo();
			else
				ResetInfo();
		}

		private void UpdateInfo()
		{
			HeaderText.text = Entity.name;
		}

		private void ResetInfo()
		{
			HeaderText.text = null;
		}
	}
}
