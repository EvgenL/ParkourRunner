using System;
using System.Collections;
using System.Collections.Generic;
using Assets.ParkourRunner.Scripts;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    #region Singleton

    public static AudioManager Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    public Sound[] Sounds;

    private void Start()
    {
        foreach (var s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();

            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    public void PlaySound(string soundName)
    {
        Sound s = Array.Find(Sounds, x => x.Name == soundName);
        if (s == null) return;
        s.Source.Play();
    }

}
