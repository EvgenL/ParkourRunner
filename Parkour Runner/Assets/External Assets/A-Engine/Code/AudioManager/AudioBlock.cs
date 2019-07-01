// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Xml;
using System;

namespace AEngine
{
	[Serializable]
	public class AudioBlock
	{
		public string name;
		public MusicTrackList music;
		public SoundTrackList sound;

		public AudioBlock ()
		{
			music = new MusicTrackList ();
			sound = new SoundTrackList ();
		}

		public void LoadAudioResources ()
		{
			music.LoadAudioResources ();
			sound.LoadAudioResources ();
		}

		public void PlayRandomMusic (AudioSource source, float systemMusicVolume)
		{
			if (music.IsFilled)
				music.PlayRandomTrack (source, systemMusicVolume);
			else
				source.Stop ();
		}

		public void PlayMusic (AudioSource source, string trackName, float systemMusicVolume)
		{
			if (music.IsFilled)
				music.tracks [trackName].Play (source, systemMusicVolume);
			else
				source.Stop ();
		}

		public string GetRandomMusic ()
		{
			if (music.IsFilled)
				return music.GetRandomBackgroundTrack ();
			else
				return null;
		}

		public void PlaySoundTrack (AudioSource source, string trackName, float systemSoundVolume)
		{
			if (sound.IsFilled) {
				sound.tracks [trackName].Play (source, systemSoundVolume);
			}
		}

		public void LoadFromXml (XmlNode target)
		{
			name = target.Attributes ["Name"].Value;

			if (music == null)
				music = new MusicTrackList ();
			if (sound == null)
				sound = new SoundTrackList ();
			music.Clear ();
			sound.Clear ();

			if (XmlDataParser.IsAnyTagInChildExist (target, "Music"))
            {
				XmlNode musicNode = XmlDataParser.FindUniqueTagInChild (target, "Music");
				if (XmlDataParser.IsAnyTagInChildExist (musicNode, "Track"))
                {
					if (musicNode.Attributes ["DelayBetweenTracks"] != null)
                        music.delay = AEngineTool.ParseFloat(musicNode.Attributes ["DelayBetweenTracks"].Value, 0f);
					if (musicNode.Attributes ["MusicNotReplyLength"] != null)
						music.musicNotReplyCount = int.Parse (musicNode.Attributes ["MusicNotReplyLength"].Value);

                    foreach (XmlNode musicTrack in XmlDataParser.FindAllTagsInChild(musicNode, "Track"))
                    {
						Track track = LoadTrackFromXml (musicTrack);
						music.tracks.Add (track.name, track);
						if (musicTrack.Attributes ["Special"] == null)
							music.AddTrackToBackgroundMusic (track);
					}
				}
			}

			if (XmlDataParser.IsAnyTagInChildExist (target, "Sound"))
            {
				XmlNode soundNode = XmlDataParser.FindUniqueTagInChild (target, "Sound");
				if (XmlDataParser.IsAnyTagInChildExist (soundNode, "Track"))
                {
					foreach (XmlNode soundTrack in XmlDataParser.FindAllTagsInChild(soundNode, "Track"))
                    {
						Track track = LoadTrackFromXml (soundTrack);
						sound.tracks.Add (track.name, track);
					}
				}
			}
		}

		public void SaveToXml (XmlDocument xmlDocument, XmlNode target)
		{
			XmlDataParser.AddAttributeToNode (xmlDocument, target, "Name", name);

			if (music.IsFilled) {
				XmlNode musicNode = xmlDocument.CreateElement ("Music");

                if (music.delay > 0)
					XmlDataParser.AddAttributeToNode(xmlDocument, musicNode, "DelayBetweenTracks", music.delay.ToString());

                XmlDataParser.AddAttributeToNode(xmlDocument, musicNode, "MusicNotReplyLength", music.musicNotReplyCount.ToString());

                foreach (var musicItem in music.tracks)
                {
					SaveTrackToXml(xmlDocument, musicNode, musicItem.Value, music.IsBackgroundMusic(musicItem.Value) ? false : true);
				}

                target.AppendChild (musicNode);
			}

			if (sound.IsFilled)
            {
				XmlNode soundNode = xmlDocument.CreateElement ("Sound");
				foreach (var soundItem in sound.tracks) {
					SaveTrackToXml (xmlDocument, soundNode, soundItem.Value);
				}
				target.AppendChild (soundNode);
			}
		}

		private Track LoadTrackFromXml (XmlNode target)
		{
			Track track = new Track ();
			
			track.name = target.Attributes ["Name"].Value;
			if (target.Attributes ["Path"] != null)
				track.path = target.Attributes ["Path"].Value;
			if (target.Attributes ["Volume"] != null)
				track.Volume = AEngineTool.ParseFloat(target.Attributes ["Volume"].Value, 1f);
			
			return track;
		}

		private void SaveTrackToXml (XmlDocument xmlDocument, XmlNode target, Track track, bool special = false)
		{
			XmlNode trackNode = xmlDocument.CreateElement ("Track");

			XmlDataParser.AddAttributeToNode (xmlDocument, trackNode, "Name", track.name);
			if (track.path != null && track.path != "")
				XmlDataParser.AddAttributeToNode (xmlDocument, trackNode, "Path", track.path);

            XmlDataParser.AddAttributeToNode (xmlDocument, trackNode, "Volume", track.Volume.ToString());
			if (special)
				XmlDataParser.AddAttributeToNode (xmlDocument, trackNode, "Special", "");

			target.AppendChild(trackNode);
		}
	}
}