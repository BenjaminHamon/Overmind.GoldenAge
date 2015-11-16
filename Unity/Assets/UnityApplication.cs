using Overmind.Core;

namespace Overmind.GoldenAge.Unity
{
	public static class UnityApplication
	{
		private static bool isInitialized = false;

		public static void Initialize()
		{
			if (isInitialized == false)
			{
				ApplicationSingleton.Logger = new UnityLogger();
				isInitialized = true;
			}
		}
	}
}
