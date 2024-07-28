using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenCanvasGroupVariable : DOTweenVariable<CanvasGroup, DOTweenCanvasGroupValue>
    {
        public DOTweenCanvasGroupVariable(CanvasGroup defaultValue) : base(defaultValue)
        {
        }
    }
}
