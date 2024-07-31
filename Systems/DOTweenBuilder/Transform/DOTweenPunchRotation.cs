using System;
using CCLBStudio.DTB;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
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
