using System;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [Serializable]
    public abstract class DOTweenGenericElement<T, TV> : DOTweenElement
    {
        #region Editor
        #if UNITY_EDITOR

        public static string TargetVariableProperty => nameof(target);
        public static string ValueVariableProperty => nameof(value);
        public static string ValueNameProperty => nameof(valueName);

        protected T Target => target.Value;
        protected TV Value => value.Value;

        protected virtual string GetDesiredValueName() => "End Value";
        [SerializeField] protected string valueName;

#endif
        #endregion
        
        [SerializeField] private DOTweenVariable<T, DOTweenScriptableValue<T>> target;
        [SerializeField] private DOTweenVariable<TV, DOTweenScriptableValue<TV>> value;

        public DOTweenGenericElement()
        {
            #if UNITY_EDITOR
            valueName = GetDesiredValueName();
            #endif
        }

        public override bool IsProperlySetup()
        {
            return Target != null;
        }
    }
}
