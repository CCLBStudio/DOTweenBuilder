using System;
using System.Collections.Generic;
using CCLBStudio;
using UnityEditor;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [CustomPropertyDrawer(typeof(DOTweenVariable<,>), true)]
    public class DOTweenVariableDrawer : PropertyDrawer
    {
        private readonly Dictionary<string, DOTweenVariableProperties> _knownProperties = new ();
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!_knownProperties.ContainsKey(property.propertyPath))
            {
                _knownProperties[property.propertyPath] = new DOTweenVariableProperties
                {
                    useScriptable = property.FindPropertyRelative(DOTweenVariable<dynamic, DOTweenScriptableValue<dynamic>>.UseScriptableProperty),
                    value = property.FindPropertyRelative(DOTweenVariable<dynamic, DOTweenScriptableValue<dynamic>>.ValueProperty),
                    scriptableValue = property.FindPropertyRelative(DOTweenVariable<dynamic, DOTweenScriptableValue<dynamic>>.ScriptableValueProperty),
                    swapButtonStyle = new GUIStyle(GUI.skin.button)
                    {
                        imagePosition = ImagePosition.TextOnly,
                        fixedHeight = 17,
                        fixedWidth = 22
                    }
                };

                _knownProperties[property.propertyPath].valuePositionOffset = _knownProperties[property.propertyPath].value.isArray ? 15f : 0f;
            }
            
            var properties = _knownProperties[property.propertyPath];

            DrawSwapButton(properties, ref position);

            if (!properties.useScriptable.boolValue)
            {
                position.x += properties.valuePositionOffset;
                position.width -= properties.valuePositionOffset;
                EditorGUI.PropertyField(position, properties.value, label);
                return;
            }

            float availableWidth = position.width;
            position.width = Mathf.Min(EditorGUIUtility.labelWidth + 400f, availableWidth / 1.3f);
            EditorGUI.PropertyField(position, properties.scriptableValue, label);
            
            float delta = position.width + 15;
            position.x += delta;
            availableWidth -= delta;
            position.width = Mathf.Min(100f, availableWidth);
            if (GUI.Button(position, "Create New"))
            {
                Type parameterType = GetBaseGenericType(fieldInfo.FieldType).GetGenericArguments()[1];

                if (parameterType.IsGenericType)
                {
                    Type genericType = parameterType.GetGenericArguments()[0];
                    int index = DOTweenBuilderEditorSettings.TrackedScriptableVariableTypes.FindIndex(x => x.valueType == genericType);
                    if (index < 0)
                    {
                        Debug.LogWarning($"No tracked type correspond to {genericType.Name}. Unable to create the Scriptable variable.");
                        return;
                    }

                    parameterType = DOTweenBuilderEditorSettings.TrackedScriptableVariableTypes[index].type;
                }
                
                var instance = EditorExtender.CreateScriptableAsset(parameterType, "RENAME-ME--REPLACE-ME");
                
                if (!properties.scriptableValue.objectReferenceValue)
                {
                    properties.scriptableValue.objectReferenceValue = instance;
                }
                
                SerializedObject instanceSerializedObj = new SerializedObject(instance);
                AssignDefaultValue(instanceSerializedObj, properties.value);
                EditorUtility.SetDirty(instance);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!_knownProperties.ContainsKey(property.propertyPath))
            {
                return base.GetPropertyHeight(property, label);
            }
            
            var properties = _knownProperties[property.propertyPath];
            if (properties.useScriptable.boolValue == false && properties.value.isArray)
            {
                return EditorGUI.GetPropertyHeight(properties.value);
            }

            return base.GetPropertyHeight(property, label);
        }

        private void AssignDefaultValue(SerializedObject serializedObject, SerializedProperty defaultProperty)
        {
            switch (defaultProperty.propertyType)
            {
                case SerializedPropertyType.Generic:
                    break;
                
                case SerializedPropertyType.Integer:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).intValue = defaultProperty.intValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).intValue = defaultProperty.intValue;
                    break;
                
                case SerializedPropertyType.Boolean:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).boolValue = defaultProperty.boolValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).boolValue = defaultProperty.boolValue;
                    break;
                
                case SerializedPropertyType.Float:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).floatValue = defaultProperty.floatValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).floatValue = defaultProperty.floatValue;
                    break;
                
                case SerializedPropertyType.String:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).stringValue = defaultProperty.stringValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).stringValue = defaultProperty.stringValue;
                    break;
                
                case SerializedPropertyType.Color:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).colorValue = defaultProperty.colorValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).colorValue = defaultProperty.colorValue;
                    break;
                
                case SerializedPropertyType.ObjectReference:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).objectReferenceValue = defaultProperty.objectReferenceValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).objectReferenceValue = defaultProperty.objectReferenceValue;
                    break;
                
                case SerializedPropertyType.LayerMask:
                    break;
                
                case SerializedPropertyType.Enum:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).enumValueIndex = defaultProperty.enumValueIndex;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).enumValueIndex = defaultProperty.enumValueIndex;
                    break;
                
                case SerializedPropertyType.Vector2:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).vector2Value = defaultProperty.vector2Value;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).vector2Value = defaultProperty.vector2Value;
                    break;
                
                case SerializedPropertyType.Vector3:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).vector3Value = defaultProperty.vector3Value;
                    var p = serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty);
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).vector3Value = defaultProperty.vector3Value;
                    break;
                
                case SerializedPropertyType.Vector4:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).vector4Value = defaultProperty.vector4Value;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).vector4Value = defaultProperty.vector4Value;
                    break;
                
                case SerializedPropertyType.Rect:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).rectValue = defaultProperty.rectValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).rectValue = defaultProperty.rectValue;
                    break;
                
                case SerializedPropertyType.ArraySize:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).arraySize = defaultProperty.arraySize;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).arraySize = defaultProperty.arraySize;
                    break;
                
                case SerializedPropertyType.Character:
                    break;
                
                case SerializedPropertyType.AnimationCurve:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).animationCurveValue = defaultProperty.animationCurveValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).animationCurveValue = defaultProperty.animationCurveValue;
                    break;
                
                case SerializedPropertyType.Bounds:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).boundsValue = defaultProperty.boundsValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).boundsValue = defaultProperty.boundsValue;
                    break;
                
                case SerializedPropertyType.Gradient:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).gradientValue = defaultProperty.gradientValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).gradientValue = defaultProperty.gradientValue;
                    break;
                
                case SerializedPropertyType.Quaternion:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).quaternionValue = defaultProperty.quaternionValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).quaternionValue = defaultProperty.quaternionValue;
                    break;
                
                case SerializedPropertyType.ExposedReference:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).exposedReferenceValue = defaultProperty.exposedReferenceValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).exposedReferenceValue = defaultProperty.exposedReferenceValue;
                    break;
                
                case SerializedPropertyType.FixedBufferSize:
                    break;
                
                case SerializedPropertyType.Vector2Int:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).vector2IntValue = defaultProperty.vector2IntValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).vector2IntValue = defaultProperty.vector2IntValue;
                    break;
                
                case SerializedPropertyType.Vector3Int:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).vector3IntValue = defaultProperty.vector3IntValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).vector3IntValue = defaultProperty.vector3IntValue;
                    break;
                
                case SerializedPropertyType.RectInt:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).rectIntValue = defaultProperty.rectIntValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).rectIntValue = defaultProperty.rectIntValue;
                    break;
                
                case SerializedPropertyType.BoundsInt:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).boundsIntValue = defaultProperty.boundsIntValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).boundsIntValue = defaultProperty.boundsIntValue;
                    break;
                
                case SerializedPropertyType.ManagedReference:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).managedReferenceValue = defaultProperty.managedReferenceValue;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).managedReferenceValue = defaultProperty.managedReferenceValue;
                    break;
                
                case SerializedPropertyType.Hash128:
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty).hash128Value = defaultProperty.hash128Value;
                    serializedObject.FindProperty(DOTweenScriptableValue<dynamic>.BaseValueProperty).hash128Value = defaultProperty.hash128Value;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawSwapButton(DOTweenVariableProperties properties, ref Rect position)
        {
            float width = position.width;
            float diffX = 14;
            float diffY = 1;
            position.x -= diffX;
            position.y += diffY;
            position.height = properties.swapButtonStyle.fixedHeight;
            position.width = properties.swapButtonStyle.fixedWidth;
            if (GUI.Button(position, "", properties.swapButtonStyle))
            {
                properties.useScriptable.boolValue = !properties.useScriptable.boolValue;
            }

            position.width = width;
            position.height = EditorGUIUtility.singleLineHeight;

            float textureSize = properties.swapButtonStyle.fixedHeight - 2;
            if (DOTweenBuilderEditor.Settings)
            {
                GUI.DrawTexture(new Rect(position.x + (properties.swapButtonStyle.fixedWidth - textureSize) / 2f, position.y + 1f, textureSize, textureSize),
                    properties.useScriptable.boolValue ? DOTweenBuilderEditor.Settings.iconScriptableValue : DOTweenBuilderEditor.Settings.iconNormalValue, ScaleMode.ScaleAndCrop);
            }
            
            position.x += diffX;
            position.y -= diffY;
            position.height = EditorGUIUtility.singleLineHeight;
        }

        private Type GetBaseGenericType(Type startType)
        {
            Type result = startType;
            bool found = startType.IsGenericType && startType.GetGenericTypeDefinition() == typeof(DOTweenVariable<,>);

            while (!found)
            {
                var baseType = result.BaseType;
                if (baseType == null)
                {
                    break;
                }
                result = baseType;
                found = result.IsGenericType && result.GetGenericTypeDefinition() == typeof(DOTweenVariable<,>);
            }

            return result;
        }

        private class DOTweenVariableProperties
        {
            public SerializedProperty useScriptable;
            public SerializedProperty value;
            public SerializedProperty scriptableValue;
            public float valuePositionOffset;
            public GUIStyle swapButtonStyle;
        }
    }
}
