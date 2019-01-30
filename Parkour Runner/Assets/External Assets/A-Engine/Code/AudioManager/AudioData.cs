using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace AEngine
{
    public class AudioData
    {
        public List<string> MusicList;
        public List<string> SoundList;
        public float Delay;

        public AudioData()
        {
            MusicList = new List<string>();
            SoundList = new List<string>();
            Delay = 0;
        }

		public void LoadAudioBlockData(string pathToAudioXmlFile, string shortXmlFileName, string blockName)
        {
			XmlDocument xmlDocument = XmlDataParser.LoadXmlDocumentFromResources(pathToAudioXmlFile, shortXmlFileName);
            XmlNode rootNode = XmlDataParser.FindUniqueTag(xmlDocument, "AudioData");
            XmlNode targetNode = XmlDataParser.FindTagByUniqueAttributeValueInChild(rootNode, "AudioBlock", "Name", blockName);

            XmlNode audioNode;
            MusicList.Clear();
            if (XmlDataParser.IsAnyTagInChildExist(targetNode, "Music"))
            {                
                audioNode = XmlDataParser.FindUniqueTagInChild(targetNode, "Music");
                Delay = float.Parse(audioNode.Attributes["DelayBetweenTracks"].Value);                
                if (XmlDataParser.IsAnyTagInChildExist(audioNode, "Track"))
                    foreach (XmlNode item in XmlDataParser.FindAllTagsInChild(audioNode, "Track"))
                    {
                        MusicList.Add(item.Attributes["Name"].Value);
                    }
            }

            SoundList.Clear();
            if (XmlDataParser.IsAnyTagInChildExist(targetNode, "Sound"))
            {
                audioNode = XmlDataParser.FindUniqueTagInChild(targetNode, "Sound");                
                if (XmlDataParser.IsAnyTagInChildExist(audioNode, "Track"))
                    foreach (XmlNode item in XmlDataParser.FindAllTagsInChild(audioNode, "Track"))
                    {
                        SoundList.Add(item.Attributes["Name"].Value);
                    }
            }
        }
    }
}
