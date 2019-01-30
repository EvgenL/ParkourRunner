// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AEngine
{
	public class ObjectPool 
	{
        //==================================================
        // Fields
        //==================================================

        private GameObject objectPrefab = null;
		private List<GameObject> objectsList = null;
        private Transform objectsContainer = null;

		public int Count
		{
			get { return objectsList.Count; }
		}

        public bool HasObjectsPrefab
        {
            get { return (objectPrefab == null) ? false : true; }
        }

        public bool HasObjectsContainer
        {
            get { return (objectsContainer == null) ? false : true; }
        }

        //==================================================
        // Constructors
        //==================================================

        public ObjectPool ()
		{            
			objectsList = new List<GameObject>();
			objectPrefab = null;
		}

        public ObjectPool (GameObject newObjectPrefab)
        {
            objectsList = new List<GameObject>();
            objectPrefab = newObjectPrefab;
        }

        public ObjectPool (string prefabAssetsPath)
        {
            objectsList = new List<GameObject>();
            objectPrefab = Resources.Load(prefabAssetsPath) as GameObject;
        }

        //==================================================
        // Methods
        //==================================================

        public void SetNewPoolPrefab (GameObject newObjectPrefab)
		{
			objectsList.Clear ();
            objectPrefab = newObjectPrefab;
		}

        public void SetNewPoolPrefab (string prefabAssetsPath)
        {
            objectsList.Clear();
            objectPrefab = Resources.Load(prefabAssetsPath) as GameObject;
        }

        public void SetNewObjectsContainer (GameObject newObjectsContainer)
        {
			if (newObjectsContainer == null)
				return;
			
            objectsContainer = newObjectsContainer.transform;

            for (int i = 0; i < objectsList.Count; i++)
                objectsList[i].transform.parent = objectsContainer;
        }

        public void GenerateObjectsInPool (int count)
		{
			if (objectPrefab == null)
            {
				Debug.LogError ("[Class = ObjectPool, method = GenerateObjectsInPool] : base prefab = null.");
				return;
			}
            
			for (int i = 0; i < count; i++) 
			{
				GameObject newObject = GameObject.Instantiate (objectPrefab) as GameObject;
				newObject.SetActive (false);
                if (objectsContainer != null)
                    newObject.transform.parent = objectsContainer;
				objectsList.Add (newObject);
			}
		}

        public GameObject Get ()
        {
            if (objectPrefab == null)
            {
                Debug.LogError("[Class = ObjectPool, method = Get] : base prefab = null.");
                return null;
            }

            GameObject resultObject = null;

            if (objectsList.Count == 0)
            {
                resultObject = GameObject.Instantiate(objectPrefab) as GameObject;
                resultObject.SetActive(false);
            }
            else
            {
                resultObject = objectsList[objectsList.Count - 1];
                objectsList.RemoveAt(objectsList.Count - 1);
            }

			resultObject.transform.parent = null;

            return resultObject;
        }

        public void Put (GameObject objectForPool, bool clearParent = false) 
		{
			objectForPool.SetActive (false);

            if (clearParent)
                objectForPool.transform.parent = null;
            else if (objectsContainer != null)
                objectForPool.transform.parent = objectsContainer;

			objectsList.Add (objectForPool);
        }

        public void Clear ()
		{
			for (int i = objectsList.Count-1; i >= 0; i--)
				GameObject.Destroy (objectsList [i]);
			objectsList.Clear ();
		}        
	}
}
