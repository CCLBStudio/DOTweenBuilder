using UnityEditor;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [CustomPropertyDrawer(typeof(DOTweenEase))]
    public class DOTweenEaseDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty useCustom = property.FindPropertyRelative(DOTweenEase.UseCustomProperty);
            SerializedProperty ease = property.FindPropertyRelative(DOTweenEase.EaseProperty);
            SerializedProperty curve = property.FindPropertyRelative(DOTweenEase.CurveProperty);

            float labelWidth = EditorGUIUtility.labelWidth;

            EditorGUI.LabelField(position, property.displayName);
            position.x += labelWidth - 15f;
            position.width -= labelWidth - 15f;
            
            EditorGUIUtility.labelWidth = 130f;
            EditorGUI.PropertyField(position, useCustom);
            
            position.x += labelWidth;
            position.width -= labelWidth;
            
            EditorGUIUtility.labelWidth = 60f;
            EditorGUI.PropertyField(position, useCustom.boolValue ? curve : ease);

            EditorGUIUtility.labelWidth = labelWidth;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}
