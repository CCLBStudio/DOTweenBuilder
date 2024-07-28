using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public struct DOTweenEase
    {
        #region Editor
#if UNITY_EDITOR
        
        public static string UseCustomProperty => nameof(useCustomCurve);
        public static string EaseProperty => nameof(ease);
        public static string CurveProperty => nameof(curve);

#endif
        #endregion

        public bool useCustomCurve;
        public Ease ease;
        public AnimationCurve curve;

        public void SetDefaultValues()
        {
            ease = Ease.InOutSine;
            curve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
        }
    }
}
