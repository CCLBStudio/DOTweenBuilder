using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenCanvasGroupVariable : DOTweenVariable<CanvasGroup, DOTweenCanvasGroupValue>
    {
        public DOTweenCanvasGroupVariable(CanvasGroup defaultValue) : base(defaultValue)
        {
        }
    }
}
