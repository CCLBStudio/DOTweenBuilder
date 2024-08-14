using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public abstract class DOTweenShakeElement<T> : DOTweenGenericElement<T, Vector3>
    {
        #region Editor
        #if UNITY_EDITOR

        protected override string GetDesiredValueName() => "Strength";

        #endif
        #endregion
        
        protected int Vibrato => vibrato.Value;
        protected float Randomness => randomness.Value;
        protected bool FadeOut => fadeOut.Value;
        protected ShakeRandomnessMode RandomnessMode => randomnessMode.Value;
        
        [Tooltip("How much will the shake vibrate.")]
        [SerializeField] private DOTweenIntVariable vibrato = new(10);
        [Tooltip("How much the shake will be random (0 to 180 - values higher than 90 kind of suck, so beware). Setting it to 0 will shake along a single direction.")]
        [SerializeField] private DOTweenFloatVariable randomness = new(90f);
        [Tooltip("If TRUE the shake will automatically fadeOut smoothly within the tween's duration, otherwise it will not.")]
        [SerializeField] private DOTweenBoolVariable fadeOut = new(true);
        [Tooltip("The type of randomness to apply, Full (fully random) or Harmonic (more balanced and visually more pleasant).")]
        [SerializeField] private DOTweenShakeRandomnessModeVariable randomnessMode = new(ShakeRandomnessMode.Full);
    }
}
