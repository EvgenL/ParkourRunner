// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace AEngine
{
    public class XmlDataParser
    {
		private static string ConstructBaseAssetsPath (string baseAssetsPath)
		{
			if (baseAssetsPath == null || baseAssetsPath.Length == 0 || baseAssetsPath.Equals ("") || baseAssetsPath.Equals (" "))
				return "";
			
			return (baseAssetsPath.EndsWith ("/") ? baseAssetsPath : baseAssetsPath + "/");
		}

		public static bool ExistsXmlFile (string baseAssetsPath, string shortFileName, string extansion = ".xml")
		{
			string path = Application.persistentDataPath + "/" + ConstructBaseAssetsPath(baseAssetsPath) + shortFileName + extansion;
			return File.Exists (path);
		}

		public static bool ExistsInProjectXmlFile (string baseAssetsPath, string shortFileName, string extansion = ".xml")
		{
			string path = Application.dataPath + "/" + ConstructBaseAssetsPath(baseAssetsPath) + shortFileName + extansion;
			return File.Exists (path);
		}

		public static bool ExistsInResourcesXmlFile (string baseAssetsPath, string shortFileName, string extansion = ".xml")
		{
			string path = Application.dataPath + "/Resources/" + ConstructBaseAssetsPath(baseAssetsPath) + shortFileName + extansion;
			return File.Exists (path);
		}

		public static XmlDocument LoadXmlDocumentFromFile (string baseAssetsPath, string shortFileName, string extansion = ".xml")
		{
			string path = Application.persistentDataPath + "/" + ConstructBaseAssetsPath(baseAssetsPath) + shortFileName + extansion;

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load (path);

			return xmlDocument;
		}

		/// <summary>
		/// Load xmlDocument from Project/Assets/path, where path = baseAssetsPath + shortFileName.
		/// </summary>
		public static XmlDocument LoadXmlDocumentFromProject (string baseAssetsPath, string shortFileName, string extansion = ".xml")
		{
			string path = Application.dataPath + "/" + ConstructBaseAssetsPath(baseAssetsPath) + shortFileName + extansion;

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load (path);

			return xmlDocument;
		}

		public static XmlDocument LoadXmlDocumentFromResources (string baseAssetsPath, string shortFileName)
		{
			string path = ConstructBaseAssetsPath(baseAssetsPath) + shortFileName;
			TextAsset xmlAsset = Resources.Load(path) as TextAsset;

			if (xmlAsset == null || xmlAsset.Equals(null))
			{
				Debug.Log(string.Format("[Class = XmlDataParser, method = LoadXmlDocumentFromAssets] Couldn't load xmlAsset with asset path \'{0}\'", path));
				return null;
			}

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xmlAsset.text);

			return xmlDocument;
		}

		/// <summary>
		/// Save xml document to external application data place/path. Use it in play/edit mode.
		/// </summary>
		public static void SaveXmlDocument (XmlDocument xmlDocument, string baseAssetsPath, string shortFileName, string extansion = ".xml")
		{
			string path = Application.persistentDataPath + "/" + ConstructBaseAssetsPath (baseAssetsPath); //+ shortFileName + extansion;
			if (!Directory.Exists (path))
				Directory.CreateDirectory (path);
			path += shortFileName + extansion;
			xmlDocument.Save (path);
		}

		/// <summary>
		/// Save xml document to Project/Assets/path. Use it in edit mode.
		/// </summary>
		public static void SaveXmlDocumentToProject (XmlDocument xmlDocument, string baseAssetsPath, string shortFileName, string extansion = ".xml")
		{
			string path = Application.dataPath + "/" + ConstructBaseAssetsPath(baseAssetsPath) + shortFileName + extansion;
			xmlDocument.Save (path);
		}

		/// <summary>
		/// Save xml document to Assets/Resources/path. Use it in edit mode.
		/// </summary>
		public static void SaveXmlDocumentToResources (XmlDocument xmlDocument, string baseAssetsPath, string shortFileName, string extansion = ".xml")
		{
			string path = Application.dataPath + "/Resources/" + ConstructBaseAssetsPath(baseAssetsPath) + shortFileName + extansion;
			xmlDocument.Save (path);
		}

		public static XmlNode CreateRootNode (XmlDocument xmlDocument, string nodeName)
		{
			XmlNode rootNode = xmlDocument.CreateElement(nodeName);
			rootNode.RemoveAll();
			xmlDocument.AppendChild(rootNode);

			return rootNode;
		}

		public static void AddAttributeToNode (XmlDocument xmlDocument, XmlNode xmlNode, string attributeName, string attributeValue)
		{
			XmlAttribute attribute = xmlDocument.CreateAttribute (attributeName);
			attribute.Value = attributeValue;
			xmlNode.Attributes.Append (attribute);
		}

		/*
        public static XmlDocument LoadExistXmlDocument(string XmlFilePath, string endsWith = ".xml")
        {
            XmlDocument xmlDocument = new XmlDocument();

            if (XmlFilePath.EndsWith(endsWith))
            {
                xmlDocument.Load(XmlFilePath);
            }
            else
            {
                xmlDocument.Load(XmlFilePath + endsWith);
            }

            return xmlDocument;
        }

        public static XmlDocument GenerateAndLoadXmlDocument(string XmlFilePath)
        {
            TextAsset xmlAsset = Resources.Load(XmlFilePath) as TextAsset;

            if (xmlAsset == null || xmlAsset.Equals(null))
            {
                Debug.Log("[Class = XmlDataHelper, method = GenerateAndLoadXmlDocument] : Couldn't load xmlAsset.");
                return null;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlAsset.text);

            return xmlDocument;
        }
        */

        public static bool IsAnyTagExist(XmlDocument document, string tagName)
        {
            XmlNodeList xmlList = document.GetElementsByTagName(tagName);

            if (xmlList == null || xmlList.Equals(null) || xmlList.Count == 0)
                return false;
            else
                return true;
        }

        public static List<XmlNode> FindAllTags(XmlDocument document, string tagName)
        {
            List<XmlNode> xmlTagList = new List<XmlNode>();

            foreach (XmlNode itemNode in document.GetElementsByTagName(tagName))
                xmlTagList.Add(itemNode);

            if (xmlTagList.Count == 0)
            {
                Debug.Log("[Class = XmlDataParser, method = FindAllTags] Not any tag was founded.");
                return null;
            }

            return xmlTagList;
        }

        public static XmlNode FindUniqueTag(XmlDocument document, string tagName)
        {
            XmlNodeList xmlTagList = document.GetElementsByTagName(tagName);

            if (xmlTagList == null || xmlTagList.Equals(null) || xmlTagList.Count == 0)
            {
                Debug.Log("[Class = XmlDataParser, method = FindUniqueTag] Tag " + tagName + " was not founded.");
                return null;
            }

            if (xmlTagList.Count >= 2)
            {
                Debug.Log("[Class = XmlDataParser, method = FindUniqueTag] Tag count > 1, not unique");
            }

            return xmlTagList[0];
        }

        public static XmlNode FindTagByUniqueAttributeValue(XmlDocument document, string tagName, string attributeName, string attributeValue, bool fast = false)
        {
            XmlNodeList xmlTagList = document.GetElementsByTagName(tagName);

            if (xmlTagList == null || xmlTagList.Equals(null))
            {
                Debug.Log("[Class = XmlDataParser, method = FindTagByUniqueAttributeValue] Tag was not founded.");
                return null;
            }

            return FindTagByUniqueAttributeValueInList(xmlTagList, tagName, attributeName, attributeValue, fast);
        }

        public static bool IsAnyTagInChildExist(XmlNode parentNode, string tagName)
        {
            foreach (XmlNode itemNode in parentNode.ChildNodes)
                if (itemNode.Name.Equals(tagName))
                    return true;

            return false;
        }

        public static List<XmlNode> FindAllTagsInChild(XmlNode parentNode, string tagName)
        {
            List<XmlNode> xmlTagList = new List<XmlNode>();

            foreach (XmlNode itemNode in parentNode.ChildNodes)
            {
                if (itemNode.Name.Equals(tagName))
                    xmlTagList.Add(itemNode);
            }

            if (xmlTagList.Count == 0)
            {
                Debug.Log("[Class = XmlDataParser, method = FindAllTagsInChild] Not any tag was not founded.");
                return null;
            }

            return xmlTagList;
        }

        public static XmlNode FindUniqueTagInChild(XmlNode parentNode, string tagName, bool fast = false)
        {
            var childList = parentNode.ChildNodes;

            return FindUniqueTagInList(childList, tagName, fast);
        }

        public static XmlNode FindTagByUniqueAttributeValueInChild(XmlNode parentNode, string tagName, string attributeName, string attributeValue, bool fast = false)
        {
            var childList = parentNode.ChildNodes;

            return FindTagByUniqueAttributeValueInList(childList, tagName, attributeName, attributeValue, fast);
        }

        private static XmlNode FindUniqueTagInList(XmlNodeList xmlNodeList, string tagName, bool fast = false)
        {
            XmlNode targetNode = null;
            bool isItemWasFounded = false;

            foreach (XmlNode itemNode in xmlNodeList)
            {
                if (itemNode.Name.Equals(tagName))
                {
                    if (isItemWasFounded)
                    {
                        Debug.Log("[Class = XmlDataParser, method = FindUniqueTagInList] Tag is not unique.");
                        break;
                    }
                    else
                    {
                        targetNode = itemNode;

                        if (fast)
                            break;

                        isItemWasFounded = true;
                    }
                }
            }

            if (targetNode == null || targetNode.Equals(null))
            {
                Debug.Log("[Class = XmlDataParser, method = FindUniqueTagInList] Tag " + tagName + " was not founded.");
                return null;
            }

            return targetNode;
        }

        private static XmlNode FindTagByUniqueAttributeValueInList(XmlNodeList xmlNodeList, string tagName, string attributeName, string attributeValue, bool fast = false)
        {
            XmlNode targetNode = null;
            bool isItemWasFounded = false;

            foreach (XmlNode itemNode in xmlNodeList)
            {
                if (itemNode.Attributes[attributeName].Value.Equals(attributeValue))
                {
                    if (isItemWasFounded)
                    {
                        Debug.Log("[Class = XmlDataParser, method = FindTagByUniqueAttributeValueInList] Tag value is not unique.");
                        break;
                    }
                    else
                    {
                        targetNode = itemNode;

                        if (fast)
                            break;

                        isItemWasFounded = true;
                    }
                }
            }

            if (targetNode == null || targetNode.Equals(null))
            {
                Debug.Log("[Class = XmlDataParser, method = FindTagByUniqueAttributeValueInList] Tag value was not founded.");
                return null;
            }

            return targetNode;
        }
    }
}