using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenScaleSingleAxis : DOTweenGenericElement<Transform, float>
    {
        [SerializeField] private DOTweenAxisVariable axis = new(DOTweenAxis.Y);
        public override Tween Generate()
        {
            return axis.Value switch
            {
                DOTweenAxis.X => Target.DOScaleX(Value, Duration),
                DOTweenAxis.Y => Target.DOScaleY(Value, Duration),
                DOTweenAxis.Z => Target.DOScaleZ(Value, Duration),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
