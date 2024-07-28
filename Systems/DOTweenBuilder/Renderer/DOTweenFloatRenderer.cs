using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenFloatRenderer : DOTweenRendererElement<float>
    {
        public override Tween Generate()
        {
            AssignPropertyId();
            var sq = DOTween.Sequence();

            foreach (var m in useSharedMaterials ? Target.sharedMaterials : Target.materials)
            {
                sq.Join(m.DOFloat(Value, propertyId, Duration));
            }

            return sq;
        }
    }
}
