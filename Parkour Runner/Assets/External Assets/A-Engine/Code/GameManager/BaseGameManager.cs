// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace AEngine
{
    public abstract class BaseGameManager<T> : MonoSingleton<T> where T : BaseGameManager<T>
    {		
		private AudioManager audioManager = null;
        public AudioManager Audio { get { return audioManager; } }
				               
        override protected void Init ()
        {
            base.Init ();
			                       
			if (audioManager == null)
				audioManager = gameObject.AddComponent<AudioManager> ();
        }

		protected virtual void Update ()
		{
			ATime.deltaTime = Time.deltaTime;
			//ATime.realDeltaTime = RealTime.deltaTime;
			ATime.actualDeltaTime = (Time.timeScale != 0) ? ATime.deltaTime : ATime.realDeltaTime;
		}
    }
}
