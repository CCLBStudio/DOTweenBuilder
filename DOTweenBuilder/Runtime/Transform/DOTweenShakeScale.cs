using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
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
