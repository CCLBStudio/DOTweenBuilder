using System;
using CCLBStudio.DOTweenBuilder;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenPunchScale : DOTweenPunchElement<Transform>
    {
        public override Tween Generate()
        {
            return Target.DOPunchScale(Value, Duration, Vibrato, Elasticity);
        }
    }
}
