using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenRendererVariable : DOTweenVariable<Renderer, DOTweenRendererValue>
    {
        public DOTweenRendererVariable(Renderer defaultValue) : base(defaultValue)
        {
        }
    }
}
