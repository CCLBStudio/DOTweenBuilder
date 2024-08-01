using System;
using System.Collections.Generic;
using CCLBStudio;
using CCLBStudioEditor;
using UnityEditor;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [CustomPropertyDrawer(typeof(DOTweenGenericElement<,>), true)]
    public class DOTweenGenericElementDrawer : PropertyDrawer
    {
        public void ClearInternalDrawers() => _initializedDrawers.Clear();
        protected static readonly string[] ToExcludeDefault = new[]
        {
            DOTweenElement.PrependIntervalProperty, DOTweenElement.PrependIntervalValueProperty, DOTweenElement.PrependCallbackProperty, DOTweenElement.PrependCallbackValueProperty,
            DOTweenElement.AppendIntervalProperty, DOTweenElement.AppendIntervalValueProperty, DOTweenElement.AppendCallbackProperty, DOTweenElement.AppendCallbackValueProperty,
            DOTweenElement.OnPlayCallbackProperty, DOTweenElement.OnUpdateCallbackProperty, DOTweenElement.OnCompleteCallbackProperty, DOTweenElement.OnStepCompleteCallbackProperty, DOTweenElement.OnRewindCallbackProperty,
            DOTweenElement.CallbacksExpandedProperty, DOTweenElement.LoopProperty, DOTweenElement.LoopCountProperty, DOTweenElement.LoopTypeProperty,
            DOTweenElement.DelayProperty
        };

        protected static readonly DOTweenBuilderEditorSettings EditorSettings = EditorExtender.LoadScriptableAsset<DOTweenBuilderEditorSettings>();
        protected readonly string[] toExclude = ToExcludeDefault;
        private readonly Dictionary<string, DrawerProperties> _initializedDrawers = new ();
        private Rect _testRect;

        protected DrawerProperties Initialize(SerializedProperty property)
        {
            string drawerKey = property.propertyPath;
            if (_initializedDrawers.TryGetValue(drawerKey, out var properties))
            {
                if (properties.isDefinedAsCustom)
                {
                    foreach (var classInfo in properties.infos)
                    {
                        properties.editorModifier.ModifySerializedProperties(classInfo.drawableProperties);
                    }
                }
                return properties;
            }

            var type = EditorExtender.GetPropertyType(property);
            bool isDefinedAsCustom = type.IsDefined(typeof(DOTweenEditorModifierAttribute), true);
            Type attrType = null;
            if (isDefinedAsCustom)
            {
                var attr = (DOTweenEditorModifierAttribute)Attribute.GetCustomAttribute(type, typeof(DOTweenEditorModifierAttribute));
                attrType = attr.Type;
            }
            var drawerProperties = new DrawerProperties
            {
                targetVariable = property.FindPropertyRelative(DOTweenGenericElement<dynamic, dynamic>.TargetVariableProperty),
                valueVariable = property.FindPropertyRelative(DOTweenGenericElement<dynamic, dynamic>.ValueVariableProperty),
                valueName = property.FindPropertyRelative(DOTweenGenericElement<dynamic, dynamic>.ValueNameProperty),
                prependCallback = property.FindPropertyRelative(DOTweenElement.PrependCallbackProperty),
                prependCallbackValue = property.FindPropertyRelative(DOTweenElement.PrependCallbackValueProperty),
                appendInterval = property.FindPropertyRelative(DOTweenElement.AppendIntervalProperty),
                appendIntervalValue = property.FindPropertyRelative(DOTweenElement.AppendIntervalValueProperty),
                appendCallback = property.FindPropertyRelative(DOTweenElement.AppendCallbackProperty),
                appendCallbackValue = property.FindPropertyRelative(DOTweenElement.AppendCallbackValueProperty),
                callbacksExpanded = property.FindPropertyRelative(DOTweenElement.CallbacksExpandedProperty),
                onPlay = property.FindPropertyRelative(DOTweenElement.OnPlayCallbackProperty),
                onUpdate = property.FindPropertyRelative(DOTweenElement.OnUpdateCallbackProperty),
                onRewind = property.FindPropertyRelative(DOTweenElement.OnRewindCallbackProperty),
                onComplete = property.FindPropertyRelative(DOTweenElement.OnCompleteCallbackProperty),
                onStepComplete = property.FindPropertyRelative(DOTweenElement.OnStepCompleteCallbackProperty),
                loop = property.FindPropertyRelative(DOTweenElement.LoopProperty),
                loopCount = property.FindPropertyRelative(DOTweenElement.LoopCountProperty),
                loopType = property.FindPropertyRelative(DOTweenElement.LoopTypeProperty),
                delay = property.FindPropertyRelative(DOTweenElement.DelayProperty),

                infos = GetClassInfos(property),
                isDefinedAsCustom = isDefinedAsCustom,
                editorModifier = isDefinedAsCustom ? (DOTweenEditorModifier)Activator.CreateInstance(attrType) : null,
                foldoutStyle = new GUIStyle(EditorStyles.foldoutHeader) { normal = { textColor = EditorSettings.foldoutColor }, onNormal = { textColor = EditorSettings.foldoutColor } }
            };

            drawerProperties.targetVariableValue = drawerProperties.targetVariable.FindPropertyRelative(DOTweenVariable<dynamic, DOTweenScriptableValue<dynamic>>.ValueProperty);
            drawerProperties.targetVariableUseScriptable = drawerProperties.targetVariable.FindPropertyRelative(DOTweenVariable<dynamic, DOTweenScriptableValue<dynamic>>.UseScriptableProperty);
            drawerProperties.targetVariableScriptableValue = drawerProperties.targetVariable.FindPropertyRelative(DOTweenVariable<dynamic, DOTweenScriptableValue<dynamic>>.ScriptableValueProperty);
            
            drawerProperties.genericIndex = drawerProperties.infos.FindIndex(x => x.type.IsGenericType && x.type.GetGenericTypeDefinition() == typeof(DOTweenGenericElement<,>));
            _initializedDrawers[drawerKey] = drawerProperties;
            DOTweenBuilderEditor.Hello(this);

            if (drawerProperties.isDefinedAsCustom)
            {
                foreach (var classInfo in drawerProperties.infos)
                {
                    drawerProperties.editorModifier.ModifySerializedProperties(classInfo.drawableProperties);
                }
            }

            return drawerProperties;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var propertiesInfo = Initialize(property);

            propertiesInfo.foldoutStyle.fixedWidth = position.width;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded,  new GUIContent(ObjectNames.NicifyVariableName(propertiesInfo.infos[0].type.Name)), true, propertiesInfo.foldoutStyle);

            if (!property.isExpanded)
            {
                return;
            }

            float width = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 185f;
            position.y += EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;

            bool showError = propertiesInfo.targetVariableUseScriptable.boolValue ? 
                propertiesInfo.targetVariableScriptableValue.objectReferenceValue == null : propertiesInfo.targetVariableValue.objectReferenceValue == null;
            if (showError)
            {
                Rect r = new Rect(position)
                {
                    height = EditorGUIUtility.singleLineHeight * 2
                };

                r.y += EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.HelpBox(r, "You need to specify the target !", MessageType.Error);
                position.y += EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
            }
            
            if (propertiesInfo.genericIndex >= 0)
            {
                position.y += EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(position, propertiesInfo.targetVariable);
                position.y += EditorGUI.GetPropertyHeight(propertiesInfo.targetVariable) + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(position, propertiesInfo.valueVariable, new GUIContent(ObjectNames.NicifyVariableName(propertiesInfo.valueName.stringValue)));
                position.y += EditorGUI.GetPropertyHeight(propertiesInfo.valueVariable) + EditorGUIUtility.standardVerticalSpacing;
            }
            
            for(int i = 0; i < propertiesInfo.infos.Count; i++) 
            {
                if (i == propertiesInfo.genericIndex)
                {
                    continue;
                }

                var info = propertiesInfo.infos[i];
                position.y += info.drawableProperties.Count > 0 ? EditorGUIUtility.singleLineHeight : 0f;

                foreach (var pair in info.drawableProperties)
                {
                    if (pair.Value == null)
                    {
                        continue;
                    }
                    EditorGUI.PropertyField(position, pair.Value);
                    position.y += EditorGUI.GetPropertyHeight(pair.Value) + EditorGUIUtility.standardVerticalSpacing;
                }
            }

            DrawExcludedProperties(ref propertiesInfo, ref position);
            EditorGUIUtility.labelWidth = width;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            string drawerKey = property.propertyPath;
            if (!_initializedDrawers.ContainsKey(drawerKey))
            {
                return 0f;
            }

            var propertiesInfo = _initializedDrawers[drawerKey];

            float totalHeight = EditorGUIUtility.singleLineHeight;

            if (!property.isExpanded)
            {
                return totalHeight;
            }
            
            foreach (var info in propertiesInfo.infos)
            {
                totalHeight += info.drawableProperties.Count > 0 ? EditorGUIUtility.singleLineHeight : 0f;
                foreach (var p in info.drawableProperties.Values)
                {
                    if (p == null)
                    {
                        continue;
                    }
                    totalHeight += EditorGUI.GetPropertyHeight(p) + EditorGUIUtility.standardVerticalSpacing;
                }
            }

            bool showError = propertiesInfo.targetVariableUseScriptable.boolValue ? 
                propertiesInfo.targetVariableScriptableValue.objectReferenceValue == null : propertiesInfo.targetVariableValue.objectReferenceValue == null;
            if (showError)
            {
                totalHeight += EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
            }

            totalHeight += 5 * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);

            if (propertiesInfo.prependCallback.boolValue)
            {
                totalHeight += EditorGUI.GetPropertyHeight(propertiesInfo.prependCallbackValue) + EditorGUIUtility.standardVerticalSpacing;
            }
            
            if (propertiesInfo.appendCallback.boolValue)
            {
                totalHeight += EditorGUI.GetPropertyHeight(propertiesInfo.appendCallbackValue) + EditorGUIUtility.standardVerticalSpacing;
            }
            
            totalHeight += 2 * EditorGUIUtility.singleLineHeight;
            if (propertiesInfo.callbacksExpanded.boolValue)
            {
                totalHeight += EditorGUI.GetPropertyHeight(propertiesInfo.onPlay) + EditorGUIUtility.standardVerticalSpacing;
                totalHeight += EditorGUI.GetPropertyHeight(propertiesInfo.onUpdate) + EditorGUIUtility.standardVerticalSpacing;
                totalHeight += EditorGUI.GetPropertyHeight(propertiesInfo.onRewind) + EditorGUIUtility.standardVerticalSpacing;
                totalHeight += EditorGUI.GetPropertyHeight(propertiesInfo.onComplete) + EditorGUIUtility.standardVerticalSpacing;
                totalHeight += EditorGUI.GetPropertyHeight(propertiesInfo.onStepComplete) + EditorGUIUtility.standardVerticalSpacing;
            }
            
            return totalHeight;
        }

        protected virtual List<EditorExtender.SerializedClassInfo> GetClassInfos(SerializedProperty property)
        {
            return EditorExtender.GetPropertiesUntilExcluding(property, typeof(DOTweenElement), toExclude);
        }

        protected virtual void DrawExcludedProperties(ref DrawerProperties propertiesInfo, ref Rect position)
        {
            float labelWidth = EditorGUIUtility.labelWidth;

            var rectWidth = Mathf.Min(400, position.width / 3f);
            propertiesInfo.loopRect = propertiesInfo.loopRect == null || Event.current.type == EventType.Repaint ? 
                new Rect(position.x, position.y, rectWidth, EditorGUIUtility.singleLineHeight) : propertiesInfo.loopRect;

            EditorGUI.PropertyField(propertiesInfo.loopRect.Value, propertiesInfo.loop);
            position.y += EditorGUI.GetPropertyHeight(propertiesInfo.loop) + EditorGUIUtility.standardVerticalSpacing;
            if (propertiesInfo.loop.boolValue)
            {
                var loopRect = new Rect(propertiesInfo.loopRect.Value);
                EditorGUIUtility.labelWidth = 90f;
                loopRect.x += rectWidth;
                EditorGUI.PropertyField(loopRect, propertiesInfo.loopCount);
                loopRect.x += rectWidth;
                EditorGUI.PropertyField(loopRect, propertiesInfo.loopType);
                EditorGUIUtility.labelWidth = labelWidth;
            }

            EditorGUI.PropertyField(position, propertiesInfo.delay);
            position.y += EditorGUI.GetPropertyHeight(propertiesInfo.delay) + EditorGUIUtility.standardVerticalSpacing;

            float halfWidth = position.width / 2f;
            propertiesInfo.singleFieldRect = propertiesInfo.singleFieldRect == null || Event.current.type == EventType.Repaint ? new Rect(position.x, position.y, Mathf.Min(halfWidth, 250f), position.height) : propertiesInfo.singleFieldRect;
            EditorGUI.PropertyField(propertiesInfo.singleFieldRect.Value, propertiesInfo.appendInterval);

            Rect temp = new Rect(propertiesInfo.singleFieldRect.Value);
            temp.x += propertiesInfo.singleFieldRect.Value.width;
            position.y += EditorGUI.GetPropertyHeight(propertiesInfo.appendInterval) + EditorGUIUtility.standardVerticalSpacing;
            if (propertiesInfo.appendInterval.boolValue)
            {
                EditorGUIUtility.labelWidth = 80f;
                EditorGUI.PropertyField(temp, propertiesInfo.appendIntervalValue, new GUIContent("Value"));
                EditorGUIUtility.labelWidth = labelWidth;
            }

            EditorGUI.PropertyField(position, propertiesInfo.prependCallback);
            position.y += EditorGUI.GetPropertyHeight(propertiesInfo.prependCallback) + EditorGUIUtility.standardVerticalSpacing;
            if (propertiesInfo.prependCallback.boolValue)
            {
                EditorGUI.PropertyField(position, propertiesInfo.prependCallbackValue, new GUIContent("Callback"));
                position.y += EditorGUI.GetPropertyHeight(propertiesInfo.prependCallbackValue) + EditorGUIUtility.standardVerticalSpacing;
            }

            EditorGUI.PropertyField(position, propertiesInfo.appendCallback);
            position.y += EditorGUI.GetPropertyHeight(propertiesInfo.appendCallback) + EditorGUIUtility.standardVerticalSpacing;
            if (propertiesInfo.appendCallback.boolValue)
            {
                EditorGUI.PropertyField(position, propertiesInfo.appendCallbackValue, new GUIContent("Callback"));
                position.y += EditorGUI.GetPropertyHeight(propertiesInfo.appendCallbackValue) + EditorGUIUtility.standardVerticalSpacing;
            }

            propertiesInfo.callbacksExpanded.boolValue = EditorGUI.Foldout(position, propertiesInfo.callbacksExpanded.boolValue,  new GUIContent("Tween Callbacks"), true);
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            
            if (propertiesInfo.callbacksExpanded.boolValue)
            {
                EditorGUI.PropertyField(position, propertiesInfo.onPlay, new GUIContent("On Play"));
                position.y += EditorGUI.GetPropertyHeight(propertiesInfo.onPlay) + EditorGUIUtility.standardVerticalSpacing;
                
                EditorGUI.PropertyField(position, propertiesInfo.onUpdate, new GUIContent("On Update"));
                position.y += EditorGUI.GetPropertyHeight(propertiesInfo.onUpdate) + EditorGUIUtility.standardVerticalSpacing;
                
                EditorGUI.PropertyField(position, propertiesInfo.onRewind, new GUIContent("On Rewind"));
                position.y += EditorGUI.GetPropertyHeight(propertiesInfo.onRewind) + EditorGUIUtility.standardVerticalSpacing;
                
                EditorGUI.PropertyField(position, propertiesInfo.onComplete, new GUIContent("On Complete"));
                position.y += EditorGUI.GetPropertyHeight(propertiesInfo.onComplete) + EditorGUIUtility.standardVerticalSpacing;
                
                EditorGUI.PropertyField(position, propertiesInfo.onStepComplete, new GUIContent("On Step Complete"));
                position.y += EditorGUI.GetPropertyHeight(propertiesInfo.onStepComplete) + EditorGUIUtility.standardVerticalSpacing;
            }
        }
        
        protected struct DrawerProperties
        {
            public SerializedProperty targetVariable;
            public SerializedProperty targetVariableValue;
            public SerializedProperty targetVariableUseScriptable;
            public SerializedProperty targetVariableScriptableValue;
            public SerializedProperty valueVariable;
            public SerializedProperty valueName;
            public SerializedProperty prependCallback;
            public SerializedProperty prependCallbackValue;
            public SerializedProperty appendInterval;
            public SerializedProperty appendIntervalValue;
            public SerializedProperty appendCallback;
            public SerializedProperty appendCallbackValue;
            public SerializedProperty callbacksExpanded;
            public SerializedProperty onPlay;
            public SerializedProperty onUpdate;
            public SerializedProperty onRewind;
            public SerializedProperty onComplete;
            public SerializedProperty onStepComplete;
            public SerializedProperty loop;
            public SerializedProperty loopCount;
            public SerializedProperty loopType;
            public SerializedProperty delay;

            public List<EditorExtender.SerializedClassInfo> infos;
            public int genericIndex;
            public GUIStyle foldoutStyle;
            public bool isDefinedAsCustom;
            public DOTweenEditorModifier editorModifier;
            
            public Rect? loopRect;
            public Rect? singleFieldRect;
        }
    }
}
