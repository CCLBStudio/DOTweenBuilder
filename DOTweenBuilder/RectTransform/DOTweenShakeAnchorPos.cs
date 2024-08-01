using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenShakeAnchorPos : DOTweenShakeElement<RectTransform>
    {
        public override Tween Generate()
        {
            return Target.DOShakeAnchorPos(Duration, Value, vibrato, randomness, SnapToInteger, fadeOut, randomnessMode);
        }
    }
}
