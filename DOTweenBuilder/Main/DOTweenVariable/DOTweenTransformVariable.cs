using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenTransformVariable : DOTweenVariable<Transform, DOTweenTransformValue>
    {
        public DOTweenTransformVariable(Transform defaultValue) : base(defaultValue)
        {
        }
    }
}
