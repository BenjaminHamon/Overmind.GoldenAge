using Overmind.GoldenAge.Unity.Map;
using Overmind.Unity.Editor;
using UnityEditor;
using UnityEngine;

namespace Overmind.GoldenAge.Unity.Editor
{
	[CustomEditor(typeof(MapView))]
	public class MapViewEditor : MonoBehaviourBaseEditor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (GUILayout.Button("Generate"))
				((MapView)target).GenerateMesh();
		}
	}
}