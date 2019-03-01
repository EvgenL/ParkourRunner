using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[ExecuteInEditMode]
public class UPplatform_fast : MonoBehaviour
{

    public GameObject under;
    public Quaternion underRotation;

    void Update()
	{
	    var objs = FindObjectsOfType<Transform>().ToList();
	    objs = objs.FindAll(x => x.name == "UPplatform_fast");

	    foreach (var v in objs)
	    {
	        var pos = v.position + under.transform.localPosition;
	        Instantiate(under, pos, underRotation, v);
	    }

	    print("set " + objs.Count + " UPplatform_fast");
	}
	
}
