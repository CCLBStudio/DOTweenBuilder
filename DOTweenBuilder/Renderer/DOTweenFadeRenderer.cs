using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenFadeRenderer : DOTweenRendererElement<float>
    {
        public override Tween Generate()
        {
            AssignPropertyId();
            
            var sq = DOTween.Sequence();
            
            if (string.IsNullOrEmpty(Property))
            {
                foreach (var m in UseSharedMaterials ? Target.sharedMaterials : Target.materials)
                {
                    sq.Join(m.DOFade(Value, Duration));
                }

                return sq;
            }

            foreach (var m in UseSharedMaterials ? Target.sharedMaterials : Target.materials)
            {
                sq.Join(m.DOFade(Value, propertyId, Duration));
            }

            return sq;
        }
    }
}
