using System;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [Serializable]
    public abstract class DOTweenPunchElement<T> : DOTweenGenericElement<T, Vector3>
    {
        #region Editor
#if UNITY_EDITOR

        protected override string GetDesiredValueName() => "Punch";

#endif
        #endregion
    
        protected int Vibrato => vibrato.Value;
        protected float Elasticity => Mathf.Clamp(elasticity.Value, 0f, 1f);

        [SerializeField] private DOTweenIntVariable vibrato = new(10);
        [SerializeField] private DOTweenFloatVariable elasticity = new(1f);
    }
}
