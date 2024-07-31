using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenShakeRotationCamera : DOTweenShakeElement<Camera>
    {
        public override Tween Generate()
        {
            return Target.DOShakeRotation(Duration, Value, vibrato, randomness, fadeOut, randomnessMode);
        }
    }
}