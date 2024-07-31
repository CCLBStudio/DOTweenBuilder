using System.Collections.Generic;
using UnityEditor;

#if UNITY_EDITOR
namespace CCLBStudio.DTB
{
    public abstract class DOTweenEditorModifier
    {
        public abstract void ModifySerializedProperties(Dictionary<string, SerializedProperty> properties);
    }
}
#endif
