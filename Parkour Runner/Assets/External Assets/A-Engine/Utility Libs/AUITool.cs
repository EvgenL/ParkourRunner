// A-Engine, Code version: 1

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AEngine
{
	public static class AUITool
	{
		public static void AttachRectTransform(GameObject target)
		{
			RectTransform uiRect = target.GetComponent<RectTransform>();
			
			if (uiRect == null)
			{
				uiRect = target.AddComponent<RectTransform>();
				
				uiRect.anchorMin = Vector2.zero;
				uiRect.anchorMax = Vector2.one;

				uiRect.localPosition = Vector3.zero;

				uiRect.offsetMin = Vector2.zero;
				uiRect.offsetMax = Vector2.zero;
			}
		}

		public static Button CreateButton(string name, bool useRawImage)
		{
			GameObject root = AGameObjectTool.CreateGameObject(name);

			root.AddComponent<CanvasRenderer>();

			MaskableGraphic targetGraphic;
			if (useRawImage)
			{
				targetGraphic = root.AddComponent<RawImage>();
			}
			else
			{
				targetGraphic = root.AddComponent<Image>();
			}
			
			Button button = root.AddComponent<Button>();
			button.targetGraphic = targetGraphic;
			
			return button; 
		}
	}
}