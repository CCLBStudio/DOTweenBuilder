using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenGradientColorRenderer : DOTweenRendererElement<Gradient>
    {
        public override Tween Generate()
        {
            AssignPropertyId();
            
            var sq = DOTween.Sequence();
            
            if (string.IsNullOrEmpty(property))
            {
                foreach (var m in useSharedMaterials ? Target.sharedMaterials : Target.materials)
                {
                    sq.Join(m.DOGradientColor(Value, Duration));
                }

                return sq;
            }

            foreach (var m in useSharedMaterials ? Target.sharedMaterials : Target.materials)
            {
                sq.Join(m.DOGradientColor(Value, property, Duration));
            }

            return sq;
        }
    }
}
