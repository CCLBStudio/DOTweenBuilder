using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenShakeRotation : DOTweenShakeElement<Transform>
    {
        public override Tween Generate()
        {
            return Target.DOShakeRotation(Duration, Value, vibrato, randomness, fadeOut, randomnessMode);
        }
    }
}
