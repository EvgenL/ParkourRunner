// A-Engine, Code version: 1

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Xml;

namespace AEngine
{
	public class BaseEngineMenuTool : Editor
	{
		[MenuItem("A-Engine/Audio Configuration...", false, 2)]
		static void ShowAudioConfigurationWindow()
		{
			EditorWindow.GetWindow<AudioConfigurationWindow>();
		}
	}
}
