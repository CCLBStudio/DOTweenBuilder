using System;
using CCLBStudio.DOTweenBuilder;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenPunchAnchorPos : DOTweenPunchElement<RectTransform>
    {
        public override Tween Generate()
        {
            return Target.DOPunchAnchorPos(Value, Duration, Vibrato, Elasticity, SnapToInteger);
        }
    }
}
