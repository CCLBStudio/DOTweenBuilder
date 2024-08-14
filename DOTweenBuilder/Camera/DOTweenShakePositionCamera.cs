using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenShakePositionCamera : DOTweenShakeElement<Camera>
    {
        public override Tween Generate()
        {
            return Target.DOShakePosition(Duration, Value, Vibrato, Randomness, FadeOut, RandomnessMode);
        }
    }
}
