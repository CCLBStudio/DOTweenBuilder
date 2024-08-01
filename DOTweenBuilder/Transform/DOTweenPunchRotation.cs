using System;
using CCLBStudio.DOTweenBuilder;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenPunchRotation : DOTweenPunchElement<Transform>
    {
        public override Tween Generate()
        {
            return Target.DOPunchRotation(Value, Duration, Vibrato, Elasticity);
        }
    }
}
