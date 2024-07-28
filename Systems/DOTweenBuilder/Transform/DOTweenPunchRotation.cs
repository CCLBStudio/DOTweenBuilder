using System;
using CCLBStudio.Systems.DOTweenBuilder;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
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
