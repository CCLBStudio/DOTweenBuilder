using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenFadeImage : DOTweenGenericElement<Image, float>
    {
        public override Tween Generate()
        {
            return Target.DOFade(Value, Duration);
        }
    }
}