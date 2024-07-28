using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenShakePositionCamera : DOTweenShakeElement<Camera>
    {
        public override Tween Generate()
        {
            return Target.DOShakePosition(Duration, Value, vibrato, randomness, fadeOut, randomnessMode);
        }
    }
}
