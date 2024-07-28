using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenAnchorPosSingleAxis : DOTweenGenericElement<RectTransform, float>
    {
        [SerializeField] private DOTweenAxis2DVariable axis;
        
        public override Tween Generate()
        {
            switch (axis.Value)
            {
                case DOTweenAxis2D.X:
                    return Target.DOAnchorPosX(Value, Duration, SnapToInteger);
                
                case DOTweenAxis2D.Y:
                    return Target.DOAnchorPosY(Value, Duration, SnapToInteger);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
