using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenRendererVariable : DOTweenVariable<Renderer, DOTweenRendererValue>
    {
        public DOTweenRendererVariable(Renderer defaultValue) : base(defaultValue)
        {
        }
    }
}