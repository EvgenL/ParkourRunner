using UnityEngine;

namespace ParkourRunner.Scripts
{
    [System.Serializable]
    public class Sound
    {
        public string Name;

        public AudioClip Clip;

        [Range(0f, 1f)]
        public float Volume = 1f;
        [Range(.1f, 3f)]
        public float Pitch = 1f;

        public bool Loop;

        [HideInInspector] public AudioSource Source;
    }
}
