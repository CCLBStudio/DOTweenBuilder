using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenJump : DOTweenGenericElement<Transform, Vector3>
    {
        [SerializeField] private DOTweenSpaceVariable space = new(Space.World);
        [SerializeField] private DOTweenFloatVariable jumpPower = new(1f);
        [SerializeField] private DOTweenIntVariable jumpCount = new(1);
        public override Tween Generate()
        {
            return space.Value == Space.Self ? Target.DOLocalJump(Value, jumpPower.Value, jumpCount.Value, Duration, SnapToInteger) : Target.DOJump(Value, jumpPower.Value, jumpCount.Value, Duration, SnapToInteger);
        }
    }
}
