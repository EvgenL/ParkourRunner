// A-Engine, Code version: 1

using UnityEngine;

namespace AEngine
{
	public static class AEditorValues
	{
		public static float MIN_OFFSET = 1f;
		public static float SMALL_OFFSET = 3f;
		public static float OFFSET = 5f;
		public static float LARGE_OFFSET = 10f;
		public static float MAX_OFFSET = 15f;

		public static float MIN_SIZE = 20f;
		public static float SMALL_SIZE = 60f;
		public static float SIZE = 100f;
		public static float LARGE_SIZE = 150f;
		public static float MAX_SIZE = 200f;

		public static float GetPartOfWidth(float partInPercents)
		{
			return (Screen.width / 100f) * partInPercents;
		}
	}
}