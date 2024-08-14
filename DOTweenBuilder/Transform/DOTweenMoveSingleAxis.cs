using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenMoveSingleAxis : DOTweenGenericElement<Transform, float>
    {
        [SerializeField] private DOTweenSpaceVariable space = new(Space.World);
        [SerializeField] private DOTweenAxisVariable axis = new(DOTweenAxis.Y);
        public override Tween Generate()
        {
            return axis.Value switch
            {
                DOTweenAxis.X => space.Value == Space.Self ? Target.DOLocalMoveX(Value, Duration, SnapToInteger) : Target.DOMoveX(Value, Duration, SnapToInteger),
                DOTweenAxis.Y => space.Value == Space.Self ? Target.DOLocalMoveY(Value, Duration, SnapToInteger) : Target.DOMoveY(Value, Duration, SnapToInteger),
                DOTweenAxis.Z => space.Value == Space.Self ? Target.DOLocalMoveZ(Value, Duration, SnapToInteger) : Target.DOMoveZ(Value, Duration, SnapToInteger),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
