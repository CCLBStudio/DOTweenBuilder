using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenAnchorPos3DSingleAxis : DOTweenGenericElement<RectTransform, float>
    {
        [SerializeField] private DOTweenAxisVariable axis = new(DOTweenAxis.Y);
        public override Tween Generate()
        {
            return axis.Value switch
            {
                DOTweenAxis.X => Target.DOAnchorPos3DX(Value, Duration, SnapToInteger),
                DOTweenAxis.Y => Target.DOAnchorPos3DY(Value, Duration, SnapToInteger),
                DOTweenAxis.Z => Target.DOAnchorPos3DZ(Value, Duration, SnapToInteger),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
