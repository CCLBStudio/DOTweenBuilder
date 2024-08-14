using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenShakePosition : DOTweenShakeElement<Transform>
    {
        public override Tween Generate()
        {
            return Target.DOShakePosition(Duration, Value, Vibrato, Randomness, SnapToInteger, FadeOut, RandomnessMode);
        }
    }
}
