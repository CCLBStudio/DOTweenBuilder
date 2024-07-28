using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenVectorRenderer : DOTweenRendererElement<Vector4>
    {
        public override Tween Generate()
        {
            AssignPropertyId();
            var sq = DOTween.Sequence();

            foreach (var m in useSharedMaterials ? Target.sharedMaterials : Target.materials)
            {
                sq.Join(m.DOVector(Value, propertyId, Duration));
            }

            return sq;
        }
    }
}
