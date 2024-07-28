#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    public abstract class DOTweenScriptableValue<T> : ScriptableObject
    {
        #region Editor
#if UNITY_EDITOR

        public static string ValueProperty => nameof(value);
        public static string BaseValueProperty => nameof(initialValue);

#endif
        #endregion
        public T Value
        {
            get => value;
            set => SetValue(value);
        }

        [SerializeField] protected T value;
        
        [DisableGUI]
        [SerializeField] protected T initialValue;

#if UNITY_EDITOR
        private void OnEnable()
        {
            initialValue = value;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                value = initialValue;
            }
        }
#endif

        private void SetValue(T newValue)
        {
            value = newValue;

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                initialValue = newValue;
            }
#endif
        }
    }
}
