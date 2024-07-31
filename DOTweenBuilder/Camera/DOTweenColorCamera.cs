using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenColorCamera : DOTweenGenericElement<Camera, Color>
    {
        public override Tween Generate()
        {
            return Target.DOColor(Value, Duration);
        }
    }
}
