// A-Engine, Code version: 1

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace AEngine
{
	public static class CodeManager 
	{
		public static void AddItemsToBlock (string fileName, string partOfPath, string blockName, string[] newItems, int tabBlockOffsetCount)
		{
			string filePath = null;

			if (newItems.IsNullOrEmpty() || !IsSingleFileExists(fileName, partOfPath, ref filePath))
			{
				return;
			}

			string content = File.ReadAllText(filePath);

			int startIndex = content.IndexOf(blockName);
			int endIndex = content.IndexOf("}", startIndex);

			int subContentStartIndex = content.IndexOf("{", startIndex);
			int length = endIndex - subContentStartIndex + 1;
			string menuContent = content.Substring(subContentStartIndex, length);
			menuContent.Replace("{", "");
			menuContent.Replace("}", "");

			char[] sepatator = new char[] { ',' };
			List<string> menuList = menuContent.Split(sepatator, StringSplitOptions.RemoveEmptyEntries).ToList();
			if (menuList == null)
			{
				menuList = new List<string>();
			}

			for (int i = 0; i < menuList.Count; ++i)
			{
				menuList[i] = new string(menuList[i].Where(c => char.IsLetterOrDigit(c)).ToArray());
				
				if (menuList[i].Length == 0)
				{
					menuList.RemoveAt(i);
					i--;
				}
			}

			for (int i = 0; i < newItems.Length; i++)
			{
				if (menuList.Count == 0 || !menuList.Contains(newItems[i]))
				{
					menuList.Add(newItems[i]);
				}
			}

			string result = "";
			for (int i = 0; i < menuList.Count; i++)
			{
				result += GetTabulationOffsetText(tabBlockOffsetCount + 1) + menuList[i];

				if (i != menuList.Count - 1)
				{
					result += ",";
				}

				result += "\n";
			}

			menuContent = content.Substring(startIndex, endIndex - startIndex + 1);
			content = content.Replace(menuContent, blockName + "\n" + GetTabulationOffsetText(tabBlockOffsetCount) + "{\n" + result + GetTabulationOffsetText(tabBlockOffsetCount) + "}");

			File.WriteAllText(filePath, content);
			AssetDatabase.Refresh();
		}

		public static void ClearBlock (string fileName, string partOfPath, string blockName, int tabBlockOffsetCount)
		{
			string filePath = null;

			if (!IsSingleFileExists(fileName, partOfPath, ref filePath))
			{
				return;
			}

			string content = File.ReadAllText(filePath);

			int startIndex = content.IndexOf(blockName);
			int endIndex = content.IndexOf("}", startIndex);

			int subContentStartIndex = content.IndexOf("{", startIndex);
			int length = endIndex - subContentStartIndex + 1;
			string menuContent = content.Substring(subContentStartIndex, length);

			content = content.Replace(menuContent, "{\n" + GetTabulationOffsetText(tabBlockOffsetCount + 1) + "\n" + GetTabulationOffsetText(tabBlockOffsetCount) + "}");

			File.WriteAllText(filePath, content);
			AssetDatabase.Refresh();
		}

		private static bool IsSingleFileExists (string fileName, string partOfPath, ref string filePath)
		{
			filePath = null;

			if (string.IsNullOrEmpty(fileName))
			{
				return false;
			}
						
			try
			{
				filePath = Directory.GetFiles(Application.dataPath + partOfPath, fileName, SearchOption.AllDirectories).Single();
			}
			catch (Exception ex)
			{
				Debug.Log(ex.Message);
			}
			
			return !string.IsNullOrEmpty(filePath);
		}

		private static string GetTabulationOffsetText(int tabCount)
		{
			string result = "";
			
			for (int i = 0; i < tabCount; i++)
			{
				result += "\t";
			}
			
			return result;
		}
	}
}