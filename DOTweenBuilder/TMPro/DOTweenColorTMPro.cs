using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenColorTMPro : DOTweenGenericElement<TextMeshProUGUI, Color>
    {
        public override Tween Generate()
        {
            return Target.DOColor(Value, Duration);
        }
    }
}
