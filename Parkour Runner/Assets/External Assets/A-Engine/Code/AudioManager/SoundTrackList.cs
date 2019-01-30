// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;

namespace AEngine
{
	public class SoundTrackList : TrackList
	{
		public override void PlayRandomTrack (AudioSource source, float systemMusicVolume)
		{
			//int index = Random.Range (0, tracks.Count);

			Track[] mas = new Track[tracks.Count];
			tracks.Values.CopyTo (mas, 0);
		}			
	}
}
