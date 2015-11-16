using Newtonsoft.Json;
using Overmind.Core.Extensions;
using Overmind.GoldenAge.Model;
using Overmind.Unity;
using System.Collections.Generic;
using UnityEngine;

namespace Overmind.GoldenAge.Unity
{
	public class MainMenuView : MonoBehaviourBase
	{
		public override void Awake()
		{
			UnityApplication.Initialize();
		}

		[SerializeField]
		private TextAsset ConfigurationFile;

		public override void Start()
		{
			ApplicationConfiguration configuration = JsonConvert.DeserializeObject<ApplicationConfiguration>(ConfigurationFile.text);
		}

		public void Exit()
		{
			Application.Quit();
		}
	}
}