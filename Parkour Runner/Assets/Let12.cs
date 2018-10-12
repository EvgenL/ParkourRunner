using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Let12 : MonoBehaviour {

	void Start ()
	{
	    var objs = FindObjectsOfType<BoxCollider>();
	    foreach (var VARIABLE in objs)
	    {
	        if (VARIABLE.name == "Let_2" || VARIABLE.name == "Let_3")
	        {
	            var s = VARIABLE.size;
	            s.y = 0.730351f;
	            VARIABLE.size = s;


	        }

        }
        print("set " + objs.Length + " let1,2's");
	}
}
