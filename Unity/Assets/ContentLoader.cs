using Overmind.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overmind.GoldenAge.Unity
{
	/// <summary>
	/// Manages loading of assets.
	/// </summary>
	public class ContentLoader : IDependency
	{
		public ContentLoader(string baseUrl)
		{
			this.baseUrl = baseUrl;
		}

		private readonly string baseUrl;
		private readonly IDictionary<string, AssetBundle> assetBundleCollection = new Dictionary<string, AssetBundle>();

		public AssetBundle GetAssetBundle(string assetBundleName)
		{
			return assetBundleCollection[assetBundleName];
		}

		public AssetBundle LoadAssetBundle(string assetBundleName)
		{
			throw new NotImplementedException();

			assetBundleName = assetBundleName.ToLowerInvariant(); // Asset bundle names are lowercase
			string url = baseUrl + "/AssetBundles/" + assetBundleName;

			using (WWW download = new WWW(url))
			{
				bool forceExit = false;
				while (download.isDone == false) // download does not update/start
					if (forceExit) throw new Exception("Forced to exit");
				if (String.IsNullOrEmpty(download.error) == false)
					throw new Exception("[ContentLoader.LoadAssetBundle] Download error: " + download.error);
				return download.assetBundle;
			}
		}

		public event Action<IDependency> Ready;

		public IEnumerator LoadAssetBundleAsync(string assetBundleName)
		{
			if (assetBundleCollection.ContainsKey(assetBundleName))
				ApplicationSingleton.Logger.LogWarning("[ContentLoader.LoadAssetBundleAsync] Asset bundle is already loaded: " + assetBundleName);
			else
			{
				string url = baseUrl + "/AssetBundles/" + assetBundleName.ToLowerInvariant(); // Asset bundle names are lowercase

				using (WWW download = new WWW(url))
				{
					yield return download;
					if (String.IsNullOrEmpty(download.error) == false)
						throw new Exception("[ContentLoader.LoadAssetBundleAsync] Download error: " + download.error);

					AssetBundle assetBundle = download.assetBundle;
					assetBundleCollection.Add(assetBundleName, assetBundle);
					ApplicationSingleton.Logger.LogInfo("[ContentLoader.LoadAssetBundleAsync] Asset bundle loaded: " + assetBundleName);
					if (Ready != null)
						Ready(this);
				}
			}
		}

	}
}
