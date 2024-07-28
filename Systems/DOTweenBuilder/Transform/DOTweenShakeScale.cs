using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenShakeScale : DOTweenShakeElement<Transform>
    {
        public override Tween Generate()
        {
            return Target.DOShakeScale(Duration, Value, vibrato, randomness, fadeOut, randomnessMode);
        }
    }
}
