using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenShakePosition : DOTweenShakeElement<Transform>
    {
        public override Tween Generate()
        {
            return Target.DOShakePosition(Duration, Value, vibrato, randomness, SnapToInteger, fadeOut, randomnessMode);
        }
    }
}
