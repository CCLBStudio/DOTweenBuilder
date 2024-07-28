using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenScaleSingleAxis : DOTweenGenericElement<Transform, float>
    {
        [SerializeField] private DOTweenAxis axis;
        public override Tween Generate()
        {
            switch (axis)
            {
                case DOTweenAxis.X:
                    return Target.DOScaleX(Value, Duration);
                
                case DOTweenAxis.Y:
                    return Target.DOScaleY(Value, Duration);
                
                case DOTweenAxis.Z:
                    return Target.DOScaleZ(Value, Duration);
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
