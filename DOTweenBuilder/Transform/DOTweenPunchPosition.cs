using System;
using CCLBStudio.DOTweenBuilder;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenPunchPosition : DOTweenPunchElement<Transform>
    {
        public override Tween Generate()
        {
            return Target.DOPunchPosition(Value, Duration, Vibrato, Elasticity, SnapToInteger);
        }
    }
}
