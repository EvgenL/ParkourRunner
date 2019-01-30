// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AEngine
{
	public class AbstractFactory<T>
	{
		private Dictionary<string, CurrentFactory<T>> factoryGroup;
		private ChanceSystem<string> chanceSystem;

		public AbstractFactory ()
		{
			factoryGroup = new Dictionary<string, CurrentFactory<T>> ();
			chanceSystem = new ChanceSystem<string> ();
		}

		public void AddFactory (CurrentFactory<T> factory, float chanceWeight = 0)
		{
			if (factoryGroup.ContainsKey (factory.name)) {
				Debug.Log ("[Class = AbstractFactory, method = AddFactory] : Current AbstractFactory already contains CurrentFactory with name: " + factory.name);
			} else {
				factoryGroup.Add (factory.name, factory);
				if (chanceWeight > 0) {
					chanceSystem.Add (factory.name, chanceWeight);
					chanceSystem.CalculateChanceWeights ();
				}
			}
		}

		public T CreateInstance (string key)
		{
			if (!factoryGroup.ContainsKey (key)) {
				Debug.LogError ("[Class = AbstractFactory, method = CreateInstance] : Current AbstractFactory not contains CurrentFactory with name: " + key);
				return default(T);
			}
			
			return factoryGroup[key].CreateInstance ();
		}

		public T CreateRandomInstance ()
		{
			if (chanceSystem.Length == 0) {
				Debug.LogError ("[Class = AbstractFactory, method = CreateRandomInstance] : ChanceSystem not configured for this Factory");
				return default(T);
			}
			
			string key = chanceSystem.Get ();
			return factoryGroup [key].CreateInstance ();
		}

		public void Clear ()
		{
			factoryGroup.Clear ();
		}
	}
}
