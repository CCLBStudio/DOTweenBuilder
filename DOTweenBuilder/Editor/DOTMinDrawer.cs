using System;
using CCLBStudioEditor;
using UnityEditor;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [CustomPropertyDrawer(typeof(DOTMinAttribute))]
    public class DOTMinDrawer : DOTweenVariableDrawer
    {
        private GUIStyle _badUseStyle;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            CheckForKnownProperty(property);
            var properties = knownProperties[property.propertyPath];
            DrawSwapButton(properties, ref position);

            DOTMinAttribute minAttribute = (DOTMinAttribute)attribute;
            Type propertyType = EditorExtender.GetUnderlyingPropertyType(properties.value);
            if(propertyType != typeof(float) && propertyType != typeof(int))
            {
                _badUseStyle ??= new GUIStyle(EditorStyles.boldLabel)
                {
                    normal = {textColor = Color.yellow}
                };
                EditorGUI.LabelField(position, "Use MinDtv Attribute with float or int DOTween variables", _badUseStyle);
                EditorGUI.EndProperty();
                return;
            }

            SerializedProperty useSo = property.FindPropertyRelative(DOTweenVariable<dynamic, DOTweenScriptableValue<dynamic>>.UseScriptableProperty);
            if (useSo.boolValue)
            {
                DrawVariableProperties(properties, ref position, label);
            }
            else
            {
                SerializedProperty value = property.FindPropertyRelative(DOTweenVariable<dynamic, DOTweenScriptableValue<dynamic>>.ValueProperty);
                if (propertyType == typeof(float))
                {
                    value.floatValue = Mathf.Max(value.floatValue, minAttribute.minF);
                }
                else
                {
                    value.intValue = Mathf.Max(value.intValue, minAttribute.min);
                }
                
                DrawVariableProperties(properties, ref position, label);
            }

            EditorGUI.EndProperty();
        }
    }
}