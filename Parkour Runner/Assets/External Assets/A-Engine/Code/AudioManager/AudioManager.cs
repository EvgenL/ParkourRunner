using UnityEngine;
using System.Collections.Generic;
using System.Xml;

namespace AEngine
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
		private enum AudioState
		{
			Default = 0,
			FadeOffForNewMusic = 1,
			FadeOnForNewMusic = 2
		}
		private AudioState state;

        private bool isMusic = true;
        public bool IsMusic
        {
            get { return isMusic; }
            set
            {
                if (isMusic != value) {
					isMusic = value;
					SaveAudioSettings ();
					if (isMusic)
						PlayMusic ();
                }				              
            }
        }

		private float musicVolumme = 1;
		public float MusicVolumme
		{
			get { return musicVolumme; }
			set { musicVolumme = value; }
		}

        private bool isSound = true;
        public bool IsSound
        {
            get { return isSound; }
            set
            {
				if (isSound != value) {
					isSound = value;
					SaveAudioSettings ();                
				}
            }
        }

		private float soundVolumme = 1;
		public float SoundVolumme
		{
			get { return soundVolumme; }
			set { soundVolumme = value; }
		}

		private int maxSoundSourceCount;
		private float fadeTime;
		private bool fadeOn;

		private AudioBlock audioBlock;
		private float delay;

        private AudioSource musicSource = null;
        private List<AudioSource> soundSource = null;

		private float musicTrackVolume;
		private string nextTrackName;

        protected override void Init()
        {
            base.Init();
            DontDestroyOnLoad(this);

            LoadAudioSettings();
            LoadAudioConfiguration();

            musicSource = AddAudioSource();
            soundSource = new List<AudioSource>();
            soundSource.Add(AddAudioSource());

            audioBlock = new AudioBlock();
            delay = 0;
            musicTrackVolume = 0;
            state = AudioState.Default;
        }
        
		private AudioSource AddAudioSource ()
		{
			AudioSource source = gameObject.AddComponent<AudioSource> ();
			source.playOnAwake = false;
			source.loop = false;

			return source;
		}

        public bool LoadAudioBlock(string blockName)
        {	
			if (audioBlock.name == blockName)
				return false;

			XmlDocument xmlDocument = XmlDataParser.LoadXmlDocumentFromResources (BaseEngineConstants.AudioResConfigurationPath, BaseEngineConstants.AudioConfigurationShortFileName);
			XmlNode rootNode = XmlDataParser.FindUniqueTag (xmlDocument, "AudioData");
			
			if (!XmlDataParser.IsAnyTagInChildExist (rootNode, "AudioBlock"))
				return false;

            foreach (XmlNode item in XmlDataParser.FindAllTagsInChild(rootNode, "AudioBlock"))
            {
                if (blockName == item.Attributes["Name"].Value)
                {
                    audioBlock.LoadFromXml(item);
                    audioBlock.LoadAudioResources();
                    break;
                }
            }

			return true;
        }

		public bool LoadAudioBlock(AudioBlocks block)
		{
			return LoadAudioBlock(block.ToString());
		}

		public void PlayMusic (bool fade = false)
		{
			if (!isMusic)
				return;

			if (fade && fadeTime > 0)
            {
				state = AudioState.FadeOffForNewMusic;
				return;
			}

			audioBlock.PlayRandomMusic (musicSource, musicVolumme);
			musicTrackVolume = musicSource.volume;
			delay = audioBlock.music.delay;
		}

		public void PlayMusic (string trackName)
		{
			if (!isMusic)
				return;

			audioBlock.PlayMusic (musicSource, trackName, musicVolumme);
			musicTrackVolume = musicSource.volume;
			delay = audioBlock.music.delay;
		}

		public void PlayMusic (Musics musicTrack)
		{
			PlayMusic(musicTrack.ToString());
		}

		public bool IsPlayingSound (string soundName)
		{
			for (int i = 0; i < soundSource.Count; i++) {
				if (soundSource [i].isPlaying && soundSource [i].clip.name == soundName)
					return true;
			}

			return false;
		}

		public bool IsPlayingSound (Sounds soundTrack)
		{
			return IsPlayingSound(soundTrack.ToString());
		}

		public void PlayUniqueSound (params string [] soundName)
		{
			for (int i = 0; i < soundName.Length; i++) {
				if (!IsPlayingSound (soundName[i])) {
					PlaySound (soundName [i]);
					break;
				}					
			}
		}

		public void PlayUniqueSound (params Sounds[] soundTracks)
		{
			for (int i = 0; i < soundTracks.Length; i++) {
				if (!IsPlayingSound (soundTracks[i])) {
					PlaySound (soundTracks[i]);
					break;
				}					
			}
		}

		public void PlayRandomSound (params string [] soundNames)
		{
			int index = Random.Range (0, soundNames.Length);
			PlaySound (soundNames[index]);
		}

		public void PlayRandomSound (params Sounds[] soundTracks)
		{
			int index = Random.Range (0, soundTracks.Length);
			PlaySound (soundTracks[index]);
		}

		public void PlaySound(string soundName, bool dontPlayIfSameIsPlaying = false)
		{
			if (!isSound)
				return;

			if (dontPlayIfSameIsPlaying && IsPlayingSound (soundName))
				return;
			
			int index = -1;
			for (int i = 0; i < soundSource.Count; i++) {
				if (!soundSource [i].isPlaying) {
					index = i;
					break;
				}
			}
			if (index == -1) {
				if (soundSource.Count < maxSoundSourceCount) {
					soundSource.Add (AddAudioSource ());
					index = soundSource.Count - 1;
				} else
					index = 0;				
			}

			audioBlock.PlaySoundTrack (soundSource [index], soundName, soundVolumme);
		}

		public void PlaySound (Sounds soundTrack, bool dontPlayIfSameIsPlaying = false)
		{
			PlaySound(soundTrack.ToString(), dontPlayIfSameIsPlaying);
		}

		public void StopSound (string soundName)
		{
			if (!isSound)
				return;
			
			for (int i = 0; i < soundSource.Count; i++) {
				if (soundSource [i].clip.name == soundName && soundSource [i].isPlaying) {
					soundSource [i].Stop ();
					return;
				}
			}
		}

		public void StopSound(Sounds soundTrack)
		{
			StopSound(soundTrack.ToString());
		}

        void Update ()
		{
			if (musicSource.isPlaying) {
				if (!isMusic) {
                    if (Fade (false))
                    	musicSource.Stop ();
                    musicSource.Stop();
				}

				if (state == AudioState.Default)
					return;
			}

			if (!isMusic)
				return;
			
			if (state == AudioState.FadeOffForNewMusic) {
				if (musicSource.isPlaying) {
					if (Fade (false)) {
						if (fadeOn) {
							nextTrackName = audioBlock.GetRandomMusic ();
							musicTrackVolume = musicVolumme * audioBlock.music.tracks [nextTrackName].Volume;
							audioBlock.PlayMusic (musicSource, nextTrackName, 0);
							state = AudioState.FadeOnForNewMusic;
							return;
						}
						PlayMusic ();
						state = AudioState.Default;
						return;
					}
				} else {
					if (fadeOn) {
						nextTrackName = audioBlock.GetRandomMusic ();
						musicTrackVolume = musicVolumme * audioBlock.music.tracks [nextTrackName].Volume;
						audioBlock.PlayMusic (musicSource, nextTrackName, 0);
						state = AudioState.FadeOnForNewMusic;
						return;
					}
					state = AudioState.Default;
					PlayMusic ();
					return;
				}
				return;
			} else if (state == AudioState.FadeOnForNewMusic) {
				if (Fade (true))
					state = AudioState.Default;
				return;
			}

            delay -= Time.unscaledDeltaTime;
			if (delay <= 0) {
				PlayMusic ();
			}
		}

		void OnApplicationFocus (bool focus)
		{
			if (focus)
            {
				if (musicSource.volume == 0)
					PlayMusic ();
			} else
            {
				musicSource.volume = 0;
			}
		}

		void OnApplicationPause (bool pause)
		{
			if (!pause)
            {
				if (musicSource.volume == 0)
					PlayMusic ();
			} else
            {
				musicSource.volume = 0;
			}
		}

		private bool Fade (bool On)
		{
			if (fadeTime == 0) {
				musicSource.volume = (On) ? musicTrackVolume : 0;
				return true;
			}

			float deltaVolume = (ATime.actualDeltaTime / fadeTime) * musicTrackVolume;
			if (On)
            {
				musicSource.volume += deltaVolume;
				if (musicSource.volume >= musicTrackVolume)
                {
					musicSource.volume = musicTrackVolume;
					return true;
				}
			} else
            {
				musicSource.volume -= deltaVolume;
				if (musicSource.volume <= 0)
                {
					musicSource.volume = 0;
					return true;
				}
			}

			return false;
		}

		private void LoadAudioSettings ()
		{		
			XmlDocument xmlDocument;
			bool needSave = false;

			if (!XmlDataParser.ExistsXmlFile (BaseEngineConstants.BaseSettingsPath, BaseEngineConstants.AudioSettingsShortFileName))
            {
                if (!XmlDataParser.ExistsInResourcesXmlFile (BaseEngineConstants.AudioResConfigurationPath, BaseEngineConstants.AudioConfigurationShortFileName))
                {
					SaveAudioSettings ();
					xmlDocument = XmlDataParser.LoadXmlDocumentFromFile (BaseEngineConstants.BaseSettingsPath, BaseEngineConstants.AudioSettingsShortFileName);
				} else
                {
					xmlDocument = XmlDataParser.LoadXmlDocumentFromResources (BaseEngineConstants.AudioResConfigurationPath, BaseEngineConstants.AudioConfigurationShortFileName);
					needSave = true;
				}
			} else
            {
                xmlDocument = XmlDataParser.LoadXmlDocumentFromFile (BaseEngineConstants.BaseSettingsPath, BaseEngineConstants.AudioSettingsShortFileName);
            }

			if (!XmlDataParser.IsAnyTagExist (xmlDocument, "AudioData"))
            {
				Debug.Log ("AudioData not founded"); 
				return;
			}
			XmlNode rootNode = XmlDataParser.FindUniqueTag (xmlDocument, "AudioData");

			if (!XmlDataParser.IsAnyTagInChildExist (rootNode, "AudioSettings"))
            {
				Debug.Log ("AudioSettings  not founded"); 
				return;
			}
            
            XmlNode audioNode = XmlDataParser.FindUniqueTagInChild (rootNode, "AudioSettings");
            
            isMusic = bool.Parse (audioNode.Attributes ["useMusic"].Value);
			musicVolumme = AEngineTool.ParseFloat(audioNode.Attributes ["musicVolume"].Value, 1f);

            isSound = bool.Parse (audioNode.Attributes ["useSound"].Value);
			soundVolumme = AEngineTool.ParseFloat(audioNode.Attributes ["soundVolume"].Value, 1f);

			if (needSave)
				SaveAudioSettings ();
		}

		private void SaveAudioSettings ()
		{
			XmlDocument xmlDocument = new XmlDocument ();
			XmlNode rootNode = XmlDataParser.CreateRootNode (xmlDocument, "AudioData");

			XmlNode audioNode = xmlDocument.CreateElement ("AudioSettings");
			XmlDataParser.AddAttributeToNode (xmlDocument, audioNode, "useMusic", isMusic.ToString ());
			XmlDataParser.AddAttributeToNode (xmlDocument, audioNode, "musicVolume", musicVolumme.ToString ());
			XmlDataParser.AddAttributeToNode (xmlDocument, audioNode, "useSound", isSound.ToString ());
			XmlDataParser.AddAttributeToNode (xmlDocument, audioNode, "soundVolume", soundVolumme.ToString ());
			rootNode.AppendChild (audioNode);
			
			XmlDataParser.SaveXmlDocument (xmlDocument, BaseEngineConstants.BaseSettingsPath, BaseEngineConstants.AudioSettingsShortFileName);
		}

		private void LoadAudioConfiguration ()
		{
			// Default settings
			maxSoundSourceCount = 3;
			fadeTime = 0;
			fadeOn = false;
            
			if (!XmlDataParser.ExistsInResourcesXmlFile (BaseEngineConstants.AudioResConfigurationPath, BaseEngineConstants.AudioConfigurationShortFileName))
				return;
            
            XmlDocument xmlDocument = XmlDataParser.LoadXmlDocumentFromResources (BaseEngineConstants.AudioResConfigurationPath, BaseEngineConstants.AudioConfigurationShortFileName);
            
            XmlNode rootNode = XmlDataParser.FindUniqueTag (xmlDocument, "AudioData");
            
            if (!XmlDataParser.IsAnyTagInChildExist (rootNode, "AudioConfiguration"))
				return;

			XmlNode configNode = XmlDataParser.FindUniqueTagInChild (rootNode, "AudioConfiguration");
			maxSoundSourceCount = int.Parse (configNode.Attributes ["SoundSourceCount"].Value);	
			fadeTime = AEngineTool.ParseFloat(configNode.Attributes ["fade"].Value, 0f);
			fadeOn = bool.Parse (configNode.Attributes ["fadeOn"].Value);
		}
    }
}
