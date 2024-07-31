using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CCLBStudio.DTB
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
