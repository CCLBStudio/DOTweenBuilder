using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenAnchorPos3D : DOTweenGenericElement<RectTransform, Vector3>
    {
        public override Tween Generate()
        {
            return Target.DOAnchorPos3D(Value, Duration, SnapToInteger);
        }
    }
}
