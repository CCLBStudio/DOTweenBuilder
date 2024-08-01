#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEditor;

namespace CCLBStudio.DOTweenBuilder
{
    public class DOTweenPathEditorModifier : DOTweenEditorModifier
    {
        private SerializedProperty _lookAtOptionValue;
        private SerializedProperty _lookAtOptionUseSo;
        private SerializedProperty _lookAtOptionSoValue;
        private SerializedProperty _lookAtOptionSoStoredValue;
        private SerializedObject _lookAtOptionSoSerializedObject;

        private SerializedProperty _lookAtAhead;
        private SerializedProperty _lookAtTarget;
        private SerializedProperty _lookAtPosition;

        public override void ModifySerializedProperties(Dictionary<string, SerializedProperty> properties)
        {
            if (!properties.ContainsKey(DOTweenPath.LookAtOptionProperty))
            {
                return;
            }

            if (_lookAtAhead == null)
            {
                _lookAtAhead = properties[DOTweenPath.LookAtAheadProperty];
                _lookAtTarget = properties[DOTweenPath.LookAtTargetProperty];
                _lookAtPosition = properties[DOTweenPath.LookAtPositionProperty];
                
                SerializedProperty lookAtOption = properties[DOTweenPath.LookAtOptionProperty];
                _lookAtOptionValue = lookAtOption.FindPropertyRelative(DOTweenVariable<dynamic, DOTweenScriptableValue<dynamic>>.ValueProperty);
                _lookAtOptionUseSo = lookAtOption.FindPropertyRelative(DOTweenVariable<dynamic, DOTweenScriptableValue<dynamic>>.UseScriptableProperty);
                _lookAtOptionSoValue = lookAtOption.FindPropertyRelative(DOTweenVariable<dynamic, DOTweenScriptableValue<dynamic>>.ScriptableValueProperty);
                 
                if (_lookAtOptionUseSo.boolValue && _lookAtOptionSoValue.objectReferenceValue != null)
                {
                    _lookAtOptionSoSerializedObject = new SerializedObject(_lookAtOptionSoValue.objectReferenceValue);
                    _lookAtOptionSoStoredValue = _lookAtOptionSoSerializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty);
                }
            }

            DOTweenPathLookAtOption option;
            
            if (_lookAtOptionUseSo.boolValue)
            {
                if (_lookAtOptionSoValue.objectReferenceValue == null)
                {
                    properties[DOTweenPath.LookAtPositionProperty] = null;
                    properties[DOTweenPath.LookAtAheadProperty] = null;
                    properties[DOTweenPath.LookAtTargetProperty] = null;
                    return;
                }

                if (_lookAtOptionSoSerializedObject == null)
                {
                    _lookAtOptionSoSerializedObject = new SerializedObject(_lookAtOptionSoValue.objectReferenceValue);
                    _lookAtOptionSoStoredValue = _lookAtOptionSoSerializedObject.FindProperty(DOTweenScriptableValue<dynamic>.ValueProperty);
                }
                
                _lookAtOptionSoSerializedObject.Update();
                option = (DOTweenPathLookAtOption)_lookAtOptionSoStoredValue.enumValueIndex;
            }
            else
            {
                option = (DOTweenPathLookAtOption) _lookAtOptionValue.enumValueIndex;
            }

            switch (option)
            {
                case DOTweenPathLookAtOption.LookAtPosition:
                    properties[DOTweenPath.LookAtPositionProperty] = _lookAtPosition;
                    properties[DOTweenPath.LookAtAheadProperty] = null;
                    properties[DOTweenPath.LookAtTargetProperty] = null;
                    break;
                
                case DOTweenPathLookAtOption.LookAtTarget:
                    properties[DOTweenPath.LookAtTargetProperty] = _lookAtTarget;
                    properties[DOTweenPath.LookAtAheadProperty] = null;
                    properties[DOTweenPath.LookAtPositionProperty] = null;
                    break;
                
                case DOTweenPathLookAtOption.LookAtAhead:
                    properties[DOTweenPath.LookAtAheadProperty] = _lookAtAhead;
                    properties[DOTweenPath.LookAtPositionProperty] = null;
                    properties[DOTweenPath.LookAtTargetProperty] = null;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
#endif
