using System;
using CCLBStudio.DTB;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
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
