// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;

namespace AEngine
{
	public static class AGameObjectTool
	{
	#region Extansions
		public static void AttachChild(this Transform transform, Transform child, bool implementParentLayerForChild = true)
		{
			child.parent = transform;
			
			child.localPosition = Vector3.zero;
			child.localRotation = Quaternion.identity;
			child.localScale = Vector3.one;
			
			if (implementParentLayerForChild)
			{
				child.gameObject.layer = transform.gameObject.layer;
			}
		}

		public static void AttachChild(this GameObject gameObject, GameObject child, bool implementParentLayerForChild = true)
		{
			gameObject.transform.AttachChild(child.transform, implementParentLayerForChild);
		}
	#endregion

		public static GameObject CreateGameObject(string name, GameObject parent = null)
		{
			GameObject result = new GameObject(name);
			
			if (parent != null)
			{
				result.transform.parent = parent.transform;
			}
			
			result.transform.localPosition = Vector3.zero;
			result.transform.localRotation = Quaternion.identity;
			result.transform.localScale = Vector3.one;
			
			return result;
		}
	}
}