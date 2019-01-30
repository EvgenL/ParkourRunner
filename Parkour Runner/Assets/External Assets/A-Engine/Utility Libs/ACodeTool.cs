// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.IO;

namespace AEngine
{
	public static class ACodeTool
	{
		public static string GetEngineRootDirectory(bool useShortVariation)
		{
			const string ENGINE_NAME = "A-Engine";
			
			string[] directories = Directory.GetDirectories(Application.dataPath, ENGINE_NAME, SearchOption.AllDirectories);

			if (directories.IsNullOrEmpty())
			{
				Debug.Log("Couldn't find root directory: ACodeTool.GetEngineRootDirectory");
				return null;
			}

			string result = null;
			foreach (string dir in directories)
			{
				if (Directory.Exists(dir + "/Menu Manager"))
				{
					result = dir;
					break;
				}
			}

			if (useShortVariation && !string.IsNullOrEmpty(result))
			{
				result = result.Substring(result.IndexOf("Assets")) + "/";
			}

			return result;
		}
	}
}