// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AEngine
{
	public static class AEngineTool
	{
	#region Action Extensions
		public static void SafeInvoke(this System.Action action)
		{
			if (action != null)
			{
				action();
			}
		}

		public static void SafeInvoke<T>(this System.Action<T> action, T arg)
		{
			if (action != null)
			{
				action(arg);
			}
		}

		public static void SafeInvoke<T1, T2>(this System.Action<T1, T2> action, T1 arg1, T2 arg2)
		{
			if (action != null)
			{
				action(arg1, arg2);
			}
		}
	#endregion

        public static float ParseFloat(string target, float defaultValue)
        {
            float result = defaultValue;
            float.TryParse(target, out result);

            return result;
        }

	#region Array Extensions
		public static void Randomize<T>(this T[] array)
		{
			for (int i = 0; i < array.Length; ++i)
			{
				int r = UnityEngine.Random.Range(i, array.Length);
			
				T buffer = array[i];
				array[i] = array[r];
				array[r] = buffer;
			}
		}

		public static bool IsNullOrEmpty<T>(this T[] array)
		{
			return (array == null || array.Length == 0) ? true : false;
		}

		public static bool IsNullOrEmpty<T>(this List<T> list)
		{
			return (list == null || list.Count == 0) ? true : false;
		}
	#endregion

		public static void LogError(string className, string methodName, string text)
		{
			Debug.Log(string.Format("[Class = {0}, method = {1}] {2}", className, methodName, text));
		}
	}
}