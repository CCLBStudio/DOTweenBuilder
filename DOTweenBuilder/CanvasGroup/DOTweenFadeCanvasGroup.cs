using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenFadeCanvasGroup : DOTweenGenericElement<CanvasGroup, float>
    {
        public override Tween Generate()
        {
            return Target.DOFade(Value, Duration);
        }
    }
}
