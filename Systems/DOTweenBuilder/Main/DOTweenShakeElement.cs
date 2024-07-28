using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public abstract class DOTweenShakeElement<T> : DOTweenGenericElement<T, Vector3>
    {
        #region Editor
        #if UNITY_EDITOR

        protected override string GetDesiredValueName() => "Strength";

        #endif
        #endregion
        
        [SerializeField] protected int vibrato = 10;
        [SerializeField] protected float randomness = 90f;
        [SerializeField] protected bool fadeOut = true;
        [SerializeField] protected ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full;
    }
}
