using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenCanvasGroupVariable : DOTweenVariable<CanvasGroup, DOTweenCanvasGroupValue>
    {
        public DOTweenCanvasGroupVariable(CanvasGroup defaultValue) : base(defaultValue)
        {
        }
    }
}