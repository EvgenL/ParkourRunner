using System;
using UnityEngine;

namespace Basic_Locomotion.Scripts.Generic
{
    [AttributeUsage(AttributeTargets.All, Inherited = true,AllowMultiple =true)]
    public class vHideInInspectorAttribute : PropertyAttribute
    {   
        public string refbooleanProperty;
        public bool invertValue;
        public vHideInInspectorAttribute(string refbooleanProperty,bool invertValue = false)
        {        
            this.refbooleanProperty = refbooleanProperty;
            this.invertValue = invertValue;     
        }
    }
}
