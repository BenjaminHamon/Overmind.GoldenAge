using Overmind.GoldenAge.Model;
using Overmind.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.GoldenAge.Unity
{
	public class ResourceView : MonoBehaviourBase
	{
		public void SetResource(Resource resource)
		{
			this.resource = resource;
		}

		private Resource resource;

		public override void Update()
		{
			quantityText.text = resource.Quantity.Format();
		}

		[SerializeField]
		private Image icon = null;

		[SerializeField]
		private Text quantityText = null;
	}
}
