using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenTilingRenderer : DOTweenRendererElement<Vector2>
    {
        public override Tween Generate()
        {
            AssignPropertyId();
            
            var sq = DOTween.Sequence();
            
            if (string.IsNullOrEmpty(property))
            {
                foreach (var m in useSharedMaterials ? Target.sharedMaterials : Target.materials)
                {
                    sq.Join(m.DOTiling(Value, Duration));
                }

                return sq;
            }

            foreach (var m in useSharedMaterials ? Target.sharedMaterials : Target.materials)
            {
                sq.Join(m.DOTiling(Value, propertyId, Duration));
            }

            return sq;
        }
    }
}
