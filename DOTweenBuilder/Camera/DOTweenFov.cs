using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenFov : DOTweenGenericElement<Camera, float>
    {
        public override Tween Generate()
        {
            return Target.DOFieldOfView(Value, Duration);
        }
    }
}
