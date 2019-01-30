// A-Engine, Code version: 1

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

namespace AEngine
{
	public static class AEditorTool
	{
		public enum SeparationStyle 
		{
			Default,
			SmallDark,
			BigDark,
			SmallLight,
			BigLight
		}

		public static void DrawSeparator(SeparationStyle style = SeparationStyle.Default)
		{	
			if (Event.current.type == EventType.Repaint)
			{			
				Texture2D tex = EditorGUIUtility.whiteTexture;
				Rect rect = GUILayoutUtility.GetLastRect();

				Color defaultColor = GUI.color;

				switch (style) {
				case SeparationStyle.Default:
					GUI.color = new UnityEngine.Color(0f, 0f, 0f, 0.25f);
					GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 2f), tex);
					break;

				case SeparationStyle.SmallDark:
					GUI.color = new UnityEngine.Color(0f, 0f, 0f, 0.25f);
					GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 1f), tex);
					break;

				case SeparationStyle.BigDark:
					GUI.color = new UnityEngine.Color(0f, 0f, 0f, 0.25f);
					GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 1f), tex);
					GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 2f), tex);
					GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 1f), tex);
					break;

				case SeparationStyle.SmallLight:
					GUI.color = new UnityEngine.Color(0f, 0f, 0f, 0.4f);
					GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 1f), tex);
					break;

				case SeparationStyle.BigLight:
					GUI.color = new UnityEngine.Color(0f, 0f, 0f, 0.15f);
					GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 4f), tex);
					GUI.color = new UnityEngine.Color(0f, 0f, 0f, 0.4f);
					GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 1f), tex);
					GUI.DrawTexture(new Rect(0f, rect.yMin + 9f, Screen.width, 1f), tex);
					GUI.color = UnityEngine.Color.white;
					break;
				}

				GUI.color = defaultColor;
			}
		}



		public static void SaveGuiChanges(this Editor editor)
		{
			if (!Application.isPlaying && GUI.changed)
			{
				EditorUtility.SetDirty(editor.target);
				editor.serializedObject.SetIsDifferentCacheDirty();
				editor.serializedObject.ApplyModifiedProperties();
				EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
			}
		}

		public static void DrawColorLabel (this Editor editor, Color color, string label, params GUILayoutOption[] options)
		{
			Color defaultColor = GUI.color;
			GUI.color = color;

			EditorGUILayout.LabelField(label, options);

			GUI.color = defaultColor;
		}

		public static void DrawListButtons<T> (List<T> list, string addBtnText, Action addBtnClick, string clearBtnText, Action clearBtnClick, 
			float singleWidth = 0f, float addWidth = 0f, float clearWidth = 0f)
		{
			if (list != null)
			{							
				EditorGUILayout.BeginHorizontal();
				if (list.Count == 0)
				{
					DrawButtonByWidth(addBtnText, singleWidth, addBtnClick);
				}
				else
				{
					DrawButtonByWidth(addBtnText, addWidth, addBtnClick);
					DrawButtonByWidth(clearBtnText, clearWidth, clearBtnClick);
				}
				EditorGUILayout.EndHorizontal();
			}
		}

		private static void DrawButtonByWidth(string text, float width, Action action)
		{
			if (width > 0)
			{
				if (GUILayout.Button(text, GUILayout.Width(width)))
				{
					action.SafeInvoke();
				}
			}
			else
			{
				if (GUILayout.Button(text))
				{
					action.SafeInvoke();
				}
			}
		}
	}
}
