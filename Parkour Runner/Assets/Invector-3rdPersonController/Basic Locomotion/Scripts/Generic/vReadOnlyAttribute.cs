using UnityEngine;

namespace Basic_Locomotion.Scripts.Generic
{
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class vReadOnlyAttribute : PropertyAttribute
    {
        public readonly bool justInPlayMode;

        public vReadOnlyAttribute(bool justInPlayMode = true)
        {
            this.justInPlayMode = justInPlayMode;
        }
    }
}
