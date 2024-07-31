using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenAnchor : DOTweenGenericElement<RectTransform, Vector2>
    {
        [SerializeField] private DOTweenAnchorTypeVariable anchorToTween = new (DOTweenAnchorType.AnchorMax);
        public override Tween Generate()
        {
            return anchorToTween.Value == DOTweenAnchorType.AnchorMin ? Target.DOAnchorMin(Value, Duration, SnapToInteger) : Target.DOAnchorMax(Value, Duration, SnapToInteger);
        }
    }
}
