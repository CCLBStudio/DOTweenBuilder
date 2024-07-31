using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenMoveSingleAxis : DOTweenGenericElement<Transform, float>
    {
        [SerializeField] private Space space = Space.World;
        [SerializeField] private DOTweenAxis axis;
        public override Tween Generate()
        {
            return axis switch
            {
                DOTweenAxis.X => space == Space.Self ? Target.DOLocalMoveX(Value, Duration, SnapToInteger) : Target.DOMoveX(Value, Duration, SnapToInteger),
                DOTweenAxis.Y => space == Space.Self ? Target.DOLocalMoveY(Value, Duration, SnapToInteger) : Target.DOMoveY(Value, Duration, SnapToInteger),
                DOTweenAxis.Z => space == Space.Self ? Target.DOLocalMoveZ(Value, Duration, SnapToInteger) : Target.DOMoveZ(Value, Duration, SnapToInteger),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
