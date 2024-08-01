using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenGradientColorImage : DOTweenGenericElement<Image, Gradient>
    {
        public override Tween Generate()
        {
            return Target.DOGradientColor(Value, Duration);
        }
    }
}
