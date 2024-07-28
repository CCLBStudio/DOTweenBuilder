using System;
using CCLBStudio.Systems.DOTweenBuilder;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
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
