// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AEngine
{
	public abstract class TrackList
	{
		public Dictionary<string, Track> tracks;

		public bool IsFilled
		{
			get { return (tracks == null || tracks.Count == 0) ? false : true; }
		}

		public TrackList ()
		{
			tracks = new Dictionary<string, Track> ();
		}

		public virtual List<string> GetTracksNames()
		{
			List<string> result = new List<string>();

			foreach (var item in tracks)
			{
				result.Add(item.Value.name);
			}

			return result;
		}

		public virtual void LoadAudioResources ()
		{
			if (!IsFilled)
				return;
			
			foreach (var item in tracks)
				item.Value.LoadAudioClip ();
		}

		public virtual void Clear ()
		{
			if (tracks != null)
				tracks.Clear ();
		}

		public virtual void PlayRandomTrack (AudioSource source, float systemMusicVolume) {}

		public float GetVolume (string trackName)
		{
			return tracks [trackName].Volume;
		}
	}
}
