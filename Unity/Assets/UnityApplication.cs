using Overmind.Core;
using Overmind.Unity;
using UnityEngine;

namespace Overmind.GoldenAge.Unity
{
	/// <summary>
	/// Global application object for Unity.
	/// It should be included in any scene so that the application singleton is initialized.
	/// The application singleton contains shared dependencies such as logging.
	/// </summary>
	/// <remarks>
	/// The initialization does not work in edit mode after compiling.
	/// See https://issuetracker.unity3d.com/issues/awake-and-start-not-called-before-update-when-assembly-is-reloaded-for-executeineditmode-scripts
	/// </remarks>
	[ExecuteInEditMode]
	public class UnityApplication : MonoBehaviourBase
	{
		public override void Awake()
		{
			ApplicationSingleton.Logger = new UnityLogger();

			ApplicationSingleton.Logger.LogInfo("[UnityApplication] Application initialized");
		}
	}
}
