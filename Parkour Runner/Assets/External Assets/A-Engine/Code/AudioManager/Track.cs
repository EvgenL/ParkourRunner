// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;

namespace AEngine
{
	public class Track
	{
		public AudioClip clip;
		public string name;
		public string path;

		private float volume;
		public float Volume {
			get { return volume; }
			set {
				volume = value;
				if (volume > 1)
					volume = 1;
				if (volume < 0)
					volume = 0;
			}
		}

		public Track ()
		{
			name = null;
			path = null;
			volume = 1;
		}

		public void LoadAudioClip ()
		{
			clip = Resources.Load (path) as AudioClip;
		}

		public void Play (AudioSource source, float systemVolume)
		{
			if (systemVolume < 0)
				systemVolume = 0;
			if (systemVolume > 1)
				systemVolume = 1;
			
			source.clip = clip;
			source.volume = systemVolume * volume;
			source.Play ();
		}
	}
}
