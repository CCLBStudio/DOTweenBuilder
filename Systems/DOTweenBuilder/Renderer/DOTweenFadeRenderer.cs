using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenFadeRenderer : DOTweenRendererElement<float>
    {
        public override Tween Generate()
        {
            AssignPropertyId();
            
            var sq = DOTween.Sequence();
            
            if (string.IsNullOrEmpty(property))
            {
                foreach (var m in useSharedMaterials ? Target.sharedMaterials : Target.materials)
                {
                    sq.Join(m.DOFade(Value, Duration));
                }

                return sq;
            }

            foreach (var m in useSharedMaterials ? Target.sharedMaterials : Target.materials)
            {
                sq.Join(m.DOFade(Value, propertyId, Duration));
            }

            return sq;
        }
    }
}
