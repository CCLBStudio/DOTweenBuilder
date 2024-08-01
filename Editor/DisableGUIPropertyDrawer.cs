using CCLBStudio.Attributes;
using UnityEditor;
using UnityEngine;

namespace CCLBStudioEditor
{
    [CustomPropertyDrawer(typeof(DisableGUIAttribute))]
    public class DisableGUIPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUILayout.PropertyField(property);
            GUI.enabled = true;
        }
    }
}