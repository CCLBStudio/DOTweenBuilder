using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenOffsetRenderer : DOTweenRendererElement<Vector2>
    {
        public override Tween Generate()
        {
            AssignPropertyId();
            
            var sq = DOTween.Sequence();
            
            if (string.IsNullOrEmpty(property))
            {
                foreach (var m in useSharedMaterials ? Target.sharedMaterials : Target.materials)
                {
                    sq.Join(m.DOOffset(Value, Duration));
                }

                return sq;
            }

            foreach (var m in useSharedMaterials ? Target.sharedMaterials : Target.materials)
            {
                sq.Join(m.DOOffset(Value, propertyId, Duration));
            }

            return sq;
        }
    }
}
