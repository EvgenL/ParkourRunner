using UnityEngine;

namespace Melee_Combat.Scripts.MeleeWeapon
{
    public class vEnumFlagAttribute : PropertyAttribute
    {
        public string enumName;

        public vEnumFlagAttribute() { }

        public vEnumFlagAttribute(string name)
        {
            enumName = name;
        }
    }
}