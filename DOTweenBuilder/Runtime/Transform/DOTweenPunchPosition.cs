using System;
using CCLBStudio.DTB;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
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
