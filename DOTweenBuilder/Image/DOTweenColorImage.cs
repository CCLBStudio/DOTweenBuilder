using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenColorImage : DOTweenGenericElement<Image, Color>
    {
        public override Tween Generate()
        {
            return Target.DOColor(Value, Duration);
        }
    }
}
