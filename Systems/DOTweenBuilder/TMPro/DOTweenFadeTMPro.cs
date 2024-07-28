using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenFadeTMPro : DOTweenGenericElement<TextMeshProUGUI, float>
    {
        public override Tween Generate()
        {
            return Target.DOFade(Value, Duration);
        }
    }
}
