using System;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenVariable<T, SV> where SV : DOTweenScriptableValue<T>
    {
        #region Editor
#if UNITY_EDITOR

        public static string UseScriptableProperty => nameof(useScriptableValue);
        public static string ValueProperty => nameof(value);
        public static string ScriptableValueProperty => nameof(scriptableValue);
    
#endif
        #endregion
        
        public DOTweenVariable(T defaultValue)
        {
            value = defaultValue;
        }

        public T Value
        {
            get => GetValue();
            set => this.value = value;
        }

        [SerializeField] private bool useScriptableValue;
        [SerializeField] private T value;
        [SerializeField] private SV scriptableValue;

        private T GetValue()
        {
            if (!useScriptableValue)
            {
                return value;
            }
            
            return scriptableValue ? scriptableValue.Value : value;
        }

        private void SetValue(T newValue)
        {
            value = newValue;
            if (scriptableValue)
            {
                scriptableValue.Value = newValue;
            }
        }
    }
}
