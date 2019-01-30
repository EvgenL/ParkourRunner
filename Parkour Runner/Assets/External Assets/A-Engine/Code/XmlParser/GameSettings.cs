// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace AEngine
{
	public class GameSettings 
	{	
		private const int IntEncryptingKey = 3;

		public enum GameSettingsParsingMode
		{
			Default,
			Encrypting,
			DublicateWithPlayerPrefs,
			EncryptingAndDublicateWithPlayerPrefs
		}

		private static Dictionary<string, string> data = null;
		private static bool isInited = false;

		public static void SetInt (string key, int value, GameSettingsParsingMode mode = GameSettingsParsingMode.Default)
		{
			if (!isInited)
				Init ();

			//int actualValue = value;
			switch (mode) {
			case GameSettingsParsingMode.Default:
				break;
				
			case GameSettingsParsingMode.Encrypting:
				break;
			}

			if (data.ContainsKey (key))
				data [key] = value.ToString();
			else
				data.Add (key, value.ToString());
		}

		public static int GetInt (string key, GameSettingsParsingMode mode = GameSettingsParsingMode.Default)
		{
			if (!isInited)
				Init ();
			
			if (data.ContainsKey (key))
				return int.Parse (data [key]);
			else {
				Debug.LogError ("[Class = GameSettings, method = GetInt] : Couldn't find value by key = " + key);
				return 0;
			}
		}

		public static void SetFloat (string key, float value)
		{
			if (!isInited)
				Init ();

			if (data.ContainsKey (key))
				data [key] = value.ToString ();
			else
				data.Add (key, value.ToString ());
		}

		public static float GetFloat (string key)
		{
			if (!isInited)
				Init ();

			if (data.ContainsKey (key))
				return float.Parse (data [key]);
			else {
				Debug.LogError ("[Class = GameSettings, method = GetFloat] : Couldn't find value by key = " + key);
				return 0;
			}
		}

		public static void SetString (string key, string value)
		{
			if (!isInited)
				Init ();

			if (data.ContainsKey (key))
				data [key] = value;
			else
				data.Add (key, value);
		}

		public static string GetString (string key)
		{
			if (!isInited)
				Init ();

			if (data.ContainsKey (key))
				return data [key];
			else {
				Debug.LogError ("[Class = GameSettings, method = GetString] : Couldn't find value by key = " + key);
				return "";
			}
		}

		public static void SetBool (string key, bool value)
		{
			if (!isInited)
				Init ();

			if (data.ContainsKey (key))
				data [key] = value.ToString ();
			else
				data.Add (key, value.ToString ());
		}

		public static bool GetBool (string key)
		{
			if (!isInited)
				Init ();

			if (data.ContainsKey (key))
				return bool.Parse (data [key]);
			else {
				Debug.LogError ("[Class = GameSettings, method = GetBool] : Couldn't find value by key = " + key);
				return false;
			}
		}

		public static bool HasKey (string key)
		{			
			if (!isInited)
				Init ();
			
			return data.ContainsKey (key);
		}

		public static void DeleteKey (string key)
		{
			if (!isInited)
				Init ();

			if (data.ContainsKey (key)) {
				data.Remove (key);
				Save ();
			}
		}

		public static void DeleteAll ()
		{
			if (!isInited)
				Init ();
			
			data.Clear ();
			Save ();
		}

		private static void Init ()
		{
			if (isInited)
				return;

			isInited = true;
			data = new Dictionary<string, string> ();

			if (!XmlDataParser.ExistsXmlFile ("", "GameSettings")) {
				XmlDocument xmlDocument = new XmlDocument ();
				
				XmlDataParser.CreateRootNode (xmlDocument, "GameSettings");
				XmlDataParser.SaveXmlDocument (xmlDocument, "", "GameSettings");
			}

			Load ();
		}

		private static void Load ()
		{			
			XmlDocument xmlDocument = XmlDataParser.LoadXmlDocumentFromFile("", "GameSettings");
			XmlNode rootNode = XmlDataParser.FindUniqueTag (xmlDocument, "GameSettings");

			if (!XmlDataParser.IsAnyTagInChildExist (rootNode, "Item"))
				return;

			data.Clear ();
			foreach (XmlNode item in XmlDataParser.FindAllTagsInChild(rootNode, "Item")) 
			{
				data.Add (item.Attributes ["Key"].Value, item.Attributes ["Value"].Value);
			}
		}

		public static void Save ()
		{
			if (!isInited)
				Init ();
			
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode rootNode = XmlDataParser.CreateRootNode (xmlDocument, "GameSettings");

			foreach (KeyValuePair<string, string> item in data) 
			{
				XmlNode itemNode = xmlDocument.CreateElement ("Item");

				XmlDataParser.AddAttributeToNode (xmlDocument, itemNode, "Key", item.Key);
				XmlDataParser.AddAttributeToNode (xmlDocument, itemNode, "Value", item.Value);

				rootNode.AppendChild(itemNode);
			}

			XmlDataParser.SaveXmlDocument (xmlDocument, "", "GameSettings");
		}
	}
}
