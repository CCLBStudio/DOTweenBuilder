using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenOrthoSize : DOTweenGenericElement<Camera, float>
    {
        public override Tween Generate()
        {
            return Target.DOOrthoSize(Value, Duration);
        }
    }
}
