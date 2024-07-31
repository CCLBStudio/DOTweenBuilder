using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenJump : DOTweenGenericElement<Transform, Vector3>
    {
        [SerializeField] private Space space = Space.World;
        [SerializeField] private float jumpPower = 1f;
        [SerializeField] private int jumpCount = 1;
        public override Tween Generate()
        {
            return space == Space.Self ? Target.DOLocalJump(Value, jumpPower, jumpCount, Duration, SnapToInteger) : Target.DOJump(Value, jumpPower, jumpCount, Duration, SnapToInteger);
        }
    }
}
