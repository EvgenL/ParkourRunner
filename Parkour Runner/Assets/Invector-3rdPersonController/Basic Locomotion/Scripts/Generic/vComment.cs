using UnityEngine;

namespace Basic_Locomotion.Scripts.Generic
{
    [vClassHeader("vComment",false, "icon_v2")]
    public class vComment : vMonoBehaviour
    {
#if UNITY_EDITOR
        [TextArea (5, 3000)]
        public string comment;
#endif
    }
}
