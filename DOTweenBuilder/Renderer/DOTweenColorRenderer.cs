using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenColorRenderer : DOTweenRendererElement<Color>
    {
        public override Tween Generate()
        {
            AssignPropertyId();
            
            var sq = DOTween.Sequence();
            
            if (string.IsNullOrEmpty(Property))
            {
                foreach (var m in UseSharedMaterials ? Target.sharedMaterials : Target.materials)
                {
                    sq.Join(m.DOColor(Value, Duration));
                }

                return sq;
            }

            foreach (var m in UseSharedMaterials ? Target.sharedMaterials : Target.materials)
            {
                sq.Join(m.DOColor(Value, propertyId, Duration));
            }

            return sq;
        }
    }
}
