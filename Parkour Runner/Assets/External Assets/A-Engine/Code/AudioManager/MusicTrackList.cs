// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AEngine
{
	public class MusicTrackList : TrackList
	{
		public float delay;
		public int musicNotReplyCount;

		private List<string> backgroundTracks;
		private string[] lastTracks;

		public MusicTrackList ()
		{
			backgroundTracks = new List<string> ();
			musicNotReplyCount = 1;
			delay = 0;
		}

		public override List<string> GetTracksNames()
		{
			List<string> result = base.GetTracksNames();

			foreach (string item in backgroundTracks)
			{
				if (!result.Contains(item))
				{
					result.Add(item);
				}
			}

			return result;
		}

		public void AddTrackToBackgroundMusic (Track track)
		{
			backgroundTracks.Add (track.name);
		}

		public bool IsBackgroundMusic (Track track)
		{
			return backgroundTracks.Contains(track.name);
		}

		public void RemoveFromBackgroundMusic (Track track)
		{
			if (!IsBackgroundMusic (track))
				return;

			backgroundTracks.Remove (track.name);
		}

		public override void Clear ()
		{
			base.Clear ();

			if (backgroundTracks != null)
				backgroundTracks.Clear ();

			if (lastTracks != null)
				for (int i = 0; i < lastTracks.Length; i++)
					lastTracks[i] = "";

			delay = 0;
		}

		public string GetRandomBackgroundTrack ()
		{
			if (backgroundTracks.Count == 0) {
				return null;
			}

			if (lastTracks == null) {
				lastTracks = new string[musicNotReplyCount];
				for (int i = 0; i < lastTracks.Length; i++)
					lastTracks [i] = "";
			}

			int index = Random.Range (0, backgroundTracks.Count);

			if (ContainsInLastTracks(backgroundTracks[index])) {
				if (ChanceSystem<bool>.IsChanceByPercent (50)) {
					for (int i = 0; i < backgroundTracks.Count; i++)
						if (!ContainsInLastTracks (backgroundTracks[i])) {
							return backgroundTracks[i];
						}
				} else {
					for (int i = backgroundTracks.Count - 1; i >= 0; i--)
						if (!ContainsInLastTracks(backgroundTracks[i])) {
							return backgroundTracks [i];
						}
				}
				
				index = 0;
			}

			return backgroundTracks [index];
		}

		public override void PlayRandomTrack (AudioSource source, float systemMusicVolume)
		{
			string newTrack = GetRandomBackgroundTrack ();
			if (newTrack == null) {
				source.Stop ();
				return;
			}

			tracks [newTrack].Play (source, systemMusicVolume);
			AddToLastTracks(newTrack);
		}

		private bool ContainsInLastTracks (Track track)
		{
			for (int i = 0; i < lastTracks.Length; i++)
				if (track.name ==  lastTracks[i])
					return true;

			return false;
		}

		private bool ContainsInLastTracks (string trackName)
		{
			for (int i = 0; i < lastTracks.Length; i++)
				if (trackName ==  lastTracks[i])
					return true;

			return false;
		}

		private void AddToLastTracks (string name)
		{
			for (int i = 0; i < lastTracks.Length; i++)
				if (lastTracks [i] == "") {
					lastTracks [i] = name;
					return;
				}

			for (int i = 0; i < lastTracks.Length - 1; i++)
				lastTracks [i] = lastTracks [i + 1];
			lastTracks [lastTracks.Length - 1] = name;
		}
			
	}
}
