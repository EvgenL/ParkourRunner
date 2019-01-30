// A-Engine, Code version: 1

using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

namespace AEngine
{
	[Serializable]
	public class DefaultAudioSettings 
	{
		public bool useMusic = true;
		public float musicVolume = 1;
		public bool useSound = true;
		public float soundVolume = 1;

		public bool needInLoading = true;

		public void DrawGUI ()
		{
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Default audio settings", EditorStyles.boldLabel);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Use music", GUILayout.Width (100));
			useMusic = EditorGUILayout.Toggle (useMusic, GUILayout.Width(100));
			EditorGUILayout.LabelField ("Music volume", GUILayout.Width (100));
			musicVolume = EditorGUILayout.Slider (musicVolume, 0, 1, GUILayout.Width (200));
			EditorGUILayout.EndHorizontal ();

			GUILayout.Space (3);

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Use sound", GUILayout.Width (100));
			useSound = EditorGUILayout.Toggle (useSound, GUILayout.Width(100));
			EditorGUILayout.LabelField ("Sound volume", GUILayout.Width (100));
			soundVolume = EditorGUILayout.Slider (soundVolume, 0, 1, GUILayout.Width (200));
			EditorGUILayout.EndHorizontal ();
		}
	}
}
