using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenShapeCircle : DOTweenGenericElement<RectTransform, Vector2>
    {
#if UNITY_EDITOR

        protected override string GetDesiredValueName() => "Center";

#endif
        
        [Tooltip("The end value degrees to reach (to rotate counter-clockwise pass a negative value).")]
        [SerializeField] private DOTweenFloatVariable endValueDegrees = new(360f);

        [Tooltip("If TRUE the coordinates will be considered as relative to the target's current anchoredPosition.")]
        [SerializeField] private DOTweenBoolVariable relativeCenter = new(false);
        
        public override Tween Generate()
        {
            return Target.DOShapeCircle(Value, endValueDegrees.Value, Duration, relativeCenter.Value, SnapToInteger);
        }
    }
}
