// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AEngine
{
	public abstract class BasePool<T> 
	{
		protected List<T> _objects;
		
		public int Count { get { return _objects.Count; } }
		
	#region Counstructors
		public BasePool()
		{
			_objects = new List<T>();
		}
	#endregion
		
		protected abstract T CreateInstance();
		
		public virtual T Get()
		{
			T result = default(T);
			
			if (_objects.Count == 0)
			{
				result = CreateInstance();
			}
			else
			{
				result = _objects[_objects.Count - 1];
				_objects.RemoveAt(_objects.Count - 1);
			}
			
			return result;
		}

		public virtual void Put(T t)
		{
			_objects.Add(t);
		}
		
		public void Clear()
		{
			_objects.Clear();
		}
	}
}