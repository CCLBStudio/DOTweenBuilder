using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenRectTransformVariable : DOTweenVariable<RectTransform, DOTweenRectTransformValue>
    {
        public DOTweenRectTransformVariable(RectTransform defaultValue) : base(defaultValue)
        {
        }
    }
}