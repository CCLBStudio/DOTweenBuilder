using System;
using CCLBStudio.DTB;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
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
