using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CCLBStudioEditor
{
    public static class EditorExtender
    {
        #region Custom Styles

        public static GUIStyle TypeStyle => new GUIStyle(EditorStyles.boldLabel) { fontSize = 15, normal = { textColor = Color.white } };
        public static GUIStyle URLButtonStyle => new GUIStyle(GUI.skin.button) {fontStyle = FontStyle.BoldAndItalic, normal = {textColor = Color.cyan}, hover = {textColor = Color.cyan}};

        #endregion

        #region Unity UI Properties

        public static string SelectableNavigationProperty => "m_Navigation";
        public static string SelectableTransitionProperty => "m_Transition";
        public static string SelectableColorsProperty => "m_Colors";
        public static string SelectableSpritesProperty => "m_SpriteState";
        public static string SelectableAnimationTriggersProperty => "m_AnimationTriggers";
        public static string SelectableInteractableProperty => "m_Interactable";
        public static string SelectableTargetGraphicProperty => "m_TargetGraphic";

        #endregion

        public static void DrawScriptField(SerializedObject serializedObject)
        {
            GUI.enabled = false;
            if (serializedObject.targetObject is MonoBehaviour behaviour)
            {
                EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(behaviour), typeof(MonoBehaviour), false);
            }
            else
            {
                EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((ScriptableObject)serializedObject.targetObject), typeof(ScriptableObject), false);
            }
            GUI.enabled = true;
            GUILayout.Space(5);
        }
        
        public static void DrawSelfProperties(SerializedObject serializedObject)
        {
            var fields = serializedObject.targetObject.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var f in fields)
            {
                var property = serializedObject.FindProperty(f.Name);
                if (property != null)
                {
                    EditorGUILayout.PropertyField(property);
                }
            }
        }

        public static List<SerializedProperty> GetSelfPropertiesExcluding(SerializedObject serializedObject, params string[] toExclude)
        {
            var fields = serializedObject.targetObject.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            List<SerializedProperty> properties = new List<SerializedProperty>();
            foreach (var f in fields)
            {
                if (toExclude != null && toExclude.Any(f.Name.Contains))
                {
                    continue;
                }

                var property = serializedObject.FindProperty(f.Name);
                if (property != null)
                {
                    properties.Add(property);
                }
            }

            return properties;
        }

        public static void DrawSelfPropertiesExcluding(SerializedObject serializedObject, bool drawTypeAsHeader, params string[] toExclude)
        {
            var fields = serializedObject.targetObject.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            List<SerializedProperty> properties = new List<SerializedProperty>();

            foreach (var f in fields)
            {
                if (toExclude != null && toExclude.Any(f.Name.Contains))
                {
                    continue;
                }

                var property = serializedObject.FindProperty(f.Name);
                if (property != null)
                {
                    properties.Add(property);
                }
            }

            if (properties.Count <= 0)
            {
                return;
            }

            if (drawTypeAsHeader)
            {
                DrawTypeHeader(serializedObject);
            }

            foreach (var p in properties)
            {
                EditorGUILayout.PropertyField(p);
            }
        }

        public static void  DrawHierarchyPropertiesUntilExcluding(SerializedProperty property, Type highestType, bool drawTypesAsHeader, bool reverse = false, params string[] toExclude)
        {
            List<SerializedClassDescriptor> allFields = new List<SerializedClassDescriptor>();
            Type currentType = GetPropertyType(property);
            int security = 50;
            
            while (currentType != null)
            {
                security--;
                if (security <= 0)
                {
                    Debug.LogError("Warning ! You exited with security due to infinite loop.");
                    return;
                }
                
                if (highestType.IsAssignableFrom(currentType))
                {
                    var desc = new SerializedClassDescriptor
                    {
                        type = currentType,
                        drawableProperties = new List<SerializedProperty>()
                    };
                    
                    var array = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                    
                    foreach (var fi in array)
                    {
                        if (toExclude != null && toExclude.Any(fi.Name.Contains))
                        {
                            continue;
                        }
                        var relativeProperty = property.FindPropertyRelative(fi.Name);
                        if (relativeProperty == null)
                        {
                            continue;
                        }

                        desc.drawableProperties.Add(relativeProperty);
                    }
                    
                    allFields.Add(desc);
                }
                
                currentType = currentType.BaseType;
            }

            if (reverse)
            {
                allFields.Reverse();
            }
            
            foreach (var classData in allFields)
            {
                if (drawTypesAsHeader && classData.drawableProperties.Count > 0)
                {
                    EditorGUILayout.Space(5);
                    DrawTypeHeader(classData.type);
                }
                
                foreach (var p in classData.drawableProperties)
                {
                    EditorGUILayout.PropertyField(p);
                }
            }
        }

        public static List<SerializedClassInfo> GetPropertiesUntilExcluding(SerializedProperty property, Type highestType, params string[] toExclude)
        {
            List<SerializedClassInfo> result = new List<SerializedClassInfo>();
            Type currentType = GetPropertyType(property);

            while (currentType != null)
            {
                if (!highestType.IsAssignableFrom(currentType))
                {
                    currentType = null;
                    continue;
                }
                
                var array = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static);

                SerializedClassInfo info = new SerializedClassInfo
                {
                    type = currentType,
                    drawableProperties = new Dictionary<string, SerializedProperty>()
                };

                if (toExclude == null)
                {
                    foreach (var fi in array)
                    {
                        var relativeProperty = property.FindPropertyRelative(fi.Name);
                        if (relativeProperty == null)
                        {
                            continue;
                        }

                        info.drawableProperties.Add(fi.Name, relativeProperty);
                    }
                    
                    result.Add(info);
                    currentType = currentType.BaseType;
                    continue;
                }


                HashSet<string> toExcludeSet = new HashSet<string>(toExclude);
                var filtered = array.Where(x => !toExcludeSet.Contains(x.Name));
                foreach (var fi in filtered)
                {
                    var relativeProperty = property.FindPropertyRelative(fi.Name);
                    if (relativeProperty == null)
                    {
                        continue;
                    }

                    info.drawableProperties.Add(fi.Name, relativeProperty);
                }

                result.Add(info);
                currentType = currentType.BaseType;
            }

            return result;
        }

        public static void DrawHierarchyPropertiesUntilExcluding(SerializedObject serializedObject, Type lowestType, bool drawTypesAsHeader, params string[] toExclude)
        {
            List<SerializedClassDescriptor> allFields = new List<SerializedClassDescriptor>();
            Type currentType = serializedObject.targetObject.GetType();
            while (currentType != null)
            {
                if (lowestType.IsAssignableFrom(currentType) && currentType != lowestType)
                {
                    currentType = currentType.BaseType;
                    continue;
                }
                
                var desc = new SerializedClassDescriptor
                {
                    type = currentType,
                    drawableProperties = new List<SerializedProperty>()
                };
                
                var array = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

                foreach (var fi in array)
                {
                    if (toExclude != null && toExclude.Any(fi.Name.Contains))
                    {
                        continue;
                    }
                    var property = serializedObject.FindProperty(fi.Name);
                    if (property == null)
                    {
                        continue;
                    }

                    desc.drawableProperties.Add(property);
                }
                
                allFields.Add(desc);
                currentType = currentType.BaseType;
            }
            
            for(int i = allFields.Count - 1; i >= 0; i--)
            {
                if (drawTypesAsHeader && allFields[i].drawableProperties.Count > 0)
                {
                    EditorGUILayout.Space(5);
                    DrawTypeHeader(allFields[i].type);
                }
                
                foreach (var property in allFields[i].drawableProperties)
                {
                    EditorGUILayout.PropertyField(property);
                }
            }
        }

        public static List<SerializedProperty> GetFullHierarchyPropertiesExcluding(SerializedObject serializedObject, params string[] toExclude)
        {
            Type currentType = serializedObject.targetObject.GetType();
            List<SerializedProperty> result = new List<SerializedProperty>();
            
            while (currentType != null)
            {
                var array = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                foreach (var fi in array)
                {
                    if (toExclude != null && toExclude.Any(fi.Name.Contains))
                    {
                        continue;
                    }
                    
                    var property = serializedObject.FindProperty(fi.Name);
                    if (property == null)
                    {
                        continue;
                    }

                    result.Add(property);
                }
                
                currentType = currentType.BaseType;
            }

            return result;
        }

        public static void DrawFullHierarchyPropertiesExcluding(SerializedObject serializedObject, bool drawTypesAsHeader, params string[] toExclude)
        {
            List<SerializedClassDescriptor> allFields = new List<SerializedClassDescriptor>();
            Type currentType = serializedObject.targetObject.GetType();
            while (currentType != null)
            {
                var desc = new SerializedClassDescriptor
                {
                    type = currentType,
                    drawableProperties = new List<SerializedProperty>()
                };
                
                var array = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

                foreach (var fi in array)
                {
                    if (toExclude != null && toExclude.Any(fi.Name.Contains))
                    {
                        continue;
                    }
                    var property = serializedObject.FindProperty(fi.Name);
                    if (property == null)
                    {
                        continue;
                    }

                    desc.drawableProperties.Add(property);
                }
                
                allFields.Add(desc);
                currentType = currentType.BaseType;
            }
            
            for(int i = allFields.Count - 1; i >= 0; i--)
            {
                if (drawTypesAsHeader && allFields[i].drawableProperties.Count > 0)
                {
                    EditorGUILayout.Space(5);
                    DrawTypeHeader(allFields[i].type.Name);
                }
                
                foreach (var property in allFields[i].drawableProperties)
                {
                    EditorGUILayout.PropertyField(property);
                }
            }
        }
        
        public static void DrawPropertyFieldsExcluding(SerializedProperty serializedProperty, bool indent = false, params string[] toExclude)
        {
            var enumerator = serializedProperty.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var property = enumerator.Current as SerializedProperty;
                if (property != null && toExclude != null && toExclude.Any(property.name.Contains))
                {
                    continue;
                }

                if (indent)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUILayout.PropertyField(property);
                    }
                    
                    continue;
                }
                
                EditorGUILayout.PropertyField(property);
            }
        }

        public static void DrawPropertyFieldsExcluding(SerializedProperty serializedProperty, ref Rect position, bool indent = false, params string[] toExclude)
        {
            var enumerator = serializedProperty.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var property = enumerator.Current as SerializedProperty;
                if (property != null && toExclude != null && toExclude.Any(property.name.Contains))
                {
                    continue;
                }

                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                if (indent)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUI.PropertyField(position, property);
                    }
                    
                    continue;
                }
                
                EditorGUI.PropertyField(position, property);
            }
        }

        public static void DrawProperties(List<SerializedProperty> properties)
        {
            foreach (var p in properties)
            {
                EditorGUILayout.PropertyField(p);
            }
        }

        public static bool IsArrayElement(this SerializedProperty property)
        {
            return property.propertyPath.Contains("Array");
        }

        public static void DrawTypeHeader(string value)
        {
            EditorGUILayout.LabelField(ObjectNames.NicifyVariableName(value), TypeStyle);
        }
        
        public static void DrawTypeHeader(Type value)
        {
            EditorGUILayout.LabelField(ObjectNames.NicifyVariableName(value.Name), TypeStyle);
        }
        
        public static void DrawTypeHeader(SerializedObject value)
        {
            EditorGUILayout.LabelField(ObjectNames.NicifyVariableName(value.targetObject.GetType().Name), TypeStyle);
        }

        public static List<T> GetAllScriptsDerivingFrom<T>() where T : class
        {
            string[] guids = AssetDatabase.FindAssets("t:script", new []{"Assets"});
            List<T> result = new List<T>();

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                MonoScript mono = AssetDatabase.LoadAssetAtPath<MonoScript>(path);

                if (!mono)
                {
                    continue;
                }

                var monoType = mono.GetClass();
                if (monoType == typeof(T) || !typeof(T).IsAssignableFrom(monoType))
                {
                    continue;
                }
                
                T instance = (T)Activator.CreateInstance(monoType);
                result.Add(instance);
            }

            return result;
        }

        public static T LoadScriptableAsset<T>() where T : ScriptableObject
        {
            string[] assetPath = AssetDatabase.FindAssets($"t:{typeof(T).Name}");

            if (assetPath.Length <= 0)
            {
                Debug.LogError($"There is no asset of type {typeof(T).Name} in the project.");
                return null;
            }

            T result = (T)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(assetPath[0]), typeof(T));

            return result;
        }
        
        public static T[] LoadScriptableAssets<T>() where T : ScriptableObject
        {
            string[] assetPath = AssetDatabase.FindAssets($"t:{typeof(T).Name}");

            if (assetPath.Length <= 0)
            {
                Debug.LogError($"There is no asset of type {typeof(T).Name} in the project.");
                return null;
            }

            T[] result = new T[assetPath.Length];

            for (int i = 0; i < assetPath.Length; i++)
            {
                result[i] = (T)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(assetPath[i]), typeof(T));
            }

            return result;
        }

        public static T CreateScriptableAsset<T>(string assetName, bool ping = true) where T : ScriptableObject
        {
            T newSo = ScriptableObject.CreateInstance<T>();
            assetName = $"/{(string.IsNullOrEmpty(assetName) ? $"New{typeof(T).Name}" : assetName)}.asset";
            
            string fullPath = AssetDatabase.GenerateUniqueAssetPath(GetActiveFolderPath() + assetName);
            AssetDatabase.CreateAsset(newSo, fullPath);
            AssetDatabase.SaveAssets();
            if (ping)
            {
                EditorGUIUtility.PingObject(newSo);
            }

            return newSo;
        }

        public static ScriptableObject CreateScriptableAsset(Type type, string assetName, bool ping = true)
        {
            if (!typeof(ScriptableObject).IsAssignableFrom(type))
            {
                throw new Exception($"Type {type.Name} is not a ScriptableObject");
            }
            
            var instance = ScriptableObject.CreateInstance(type);
            assetName = $"/{(string.IsNullOrEmpty(assetName) ? $"New{type.Name}" : assetName)}.asset";
            string fullPath = AssetDatabase.GenerateUniqueAssetPath(GetActiveFolderPath() + assetName);
            AssetDatabase.CreateAsset(instance, fullPath);
            AssetDatabase.SaveAssets();
            if (ping)
            {
                EditorGUIUtility.PingObject(instance);
            }

            return instance;
        }
        
        public static int DrawPopup(int current, string[] values, string label, bool nicifyValues = true,  string customMessage = "")
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent(label), GUILayout.Width(EditorGUIUtility.labelWidth - EditorGUI.indentLevel * 15));

            if (values.Length <= 0)
            {
                EditorGUILayout.HelpBox(string.IsNullOrEmpty(customMessage) ? $"You asked for a Popup for content {label} but you provided an empty values array." : customMessage, MessageType.Warning);
                GUILayout.EndHorizontal();
                return -1;
            }

            if (nicifyValues)
            {
                values = values.Select(ObjectNames.NicifyVariableName).ToArray();
            }
            current = Mathf.Clamp(current, 0, values.Length - 1);
            int result = EditorGUILayout.Popup(current, values);
            
            GUILayout.EndHorizontal();
            return result;
        }

        public static Type GetPropertyType(SerializedProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException();
            }
            
            string[] pathParts = property.propertyPath.Split('.');
            object targetObject = null;
            if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue != null)
            {
                targetObject = property.objectReferenceValue;
            }
            else if (property.propertyType == SerializedPropertyType.ManagedReference && property.managedReferenceValue != null)
            {
                targetObject = property.managedReferenceValue;
            }
            else if (property.propertyType == SerializedPropertyType.ExposedReference && property.exposedReferenceValue != null)
            {
                targetObject = property.exposedReferenceValue;
            }

            if (targetObject == null)
            {
                throw new NullReferenceException();
            }
            
            Type currentType = targetObject.GetType();
            
            foreach (string part in pathParts)
            {
                if (part == "Array")
                {
                    // Handle arrays
                    if (part.Contains("data["))
                    {
                        // We reached an array element
                        int startIndex = part.IndexOf("[") + 1;
                        int endIndex = part.IndexOf("]");
                        string indexString = part.Substring(startIndex, endIndex - startIndex);
                        int index = int.Parse(indexString);

                        if (currentType.IsArray)
                        {
                            currentType = currentType.GetElementType();
                        }
                        else if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            currentType = currentType.GetGenericArguments()[0];
                        }

                        // Get the array element at the specified index
                        if (targetObject is Array array)
                        {
                            targetObject = array.GetValue(index);
                        }
                        else if (targetObject is IList list)
                        {
                            targetObject = list[index];
                        }
                    }
                }
                else
                {
                    // Handle normal fields
                    FieldInfo fieldInfo = currentType.GetField(part, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (fieldInfo != null)
                    {
                        currentType = fieldInfo.FieldType;
                        targetObject = fieldInfo.GetValue(targetObject);
                    }
                    else
                    {
                        PropertyInfo propertyInfo = currentType.GetProperty(part, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (propertyInfo != null)
                        {
                            currentType = propertyInfo.PropertyType;
                            targetObject = propertyInfo.GetValue(targetObject);
                        }
                    }
                }
            }

            return currentType;
        }

        public static string GetActiveFolderPath()
        {
            Type projectWindowUtilType = typeof(ProjectWindowUtil);
            MethodInfo getActiveFolderPath = projectWindowUtilType.GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);

            if (getActiveFolderPath == null)
            {
                return "Assets";
            }
            
            object obj = getActiveFolderPath.Invoke(null, Array.Empty<object>());
            string pathToCurrentFolder = obj.ToString();
            return pathToCurrentFolder;
        }

        public static bool IsNullOrMissing(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                return !property.objectReferenceValue;
            }

            if (property.propertyType == SerializedPropertyType.ManagedReference)
            {
                return property.managedReferenceValue == null;
            }

            if (property.propertyType == SerializedPropertyType.ExposedReference)
            {
                return !property.exposedReferenceValue;
            }

            return false;
        }
        
        public static Canvas CreateCanvas()
        {
            GameObject canvasGameObject = new GameObject("Canvas")
            {
                transform =
                {
                    localPosition = Vector3.zero,
                    localRotation = Quaternion.identity,
                    localScale = Vector3.one
                }
            };

            Canvas canvas = canvasGameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler canvasScaler = canvasGameObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1080, 1920);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;

            canvasGameObject.AddComponent<GraphicRaycaster>();
            return canvas;
        }

        private class SerializedClassDescriptor
        {
            public Type type;
            public List<SerializedProperty> drawableProperties;
        }

        public struct SerializedClassInfo
        {
            public Type type;
            public Dictionary<string, SerializedProperty> drawableProperties;
        }
    }
}
