using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenVectorRenderer : DOTweenRendererElement<Vector3>
    {
        public override Tween Generate()
        {
            AssignPropertyId();
            var sq = DOTween.Sequence();

            foreach (var m in UseSharedMaterials ? Target.sharedMaterials : Target.materials)
            {
                sq.Join(m.DOVector(Value, propertyId, Duration));
            }

            return sq;
        }
    }
}
