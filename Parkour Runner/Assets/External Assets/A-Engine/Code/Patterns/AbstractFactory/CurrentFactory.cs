// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;

namespace AEngine
{
	abstract public class CurrentFactory<T>
	{
		public string name;

		abstract public T CreateInstance ();
	}
}
