using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenRectTransformVariable : DOTweenVariable<RectTransform, DOTweenRectTransformValue>
    {
        public DOTweenRectTransformVariable(RectTransform defaultValue) : base(defaultValue)
        {
        }
    }
}
