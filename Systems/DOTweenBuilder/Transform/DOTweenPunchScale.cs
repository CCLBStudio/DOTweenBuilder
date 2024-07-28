using System;
using CCLBStudio.Systems.DOTweenBuilder;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
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
