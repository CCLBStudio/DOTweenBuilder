using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenJumpAnchorPos : DOTweenGenericElement<RectTransform, Vector2>
    {
        [SerializeField] private DOTweenFloatVariable jumpPower = new(1f);
        [SerializeField] private DOTweenIntVariable jumpCount = new(1);
        public override Tween Generate()
        {
            return Target.DOJumpAnchorPos(Value, jumpPower.Value, jumpCount.Value, Duration, SnapToInteger);
        }
    }
}
