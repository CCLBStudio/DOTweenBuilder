using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenTransformVariable : DOTweenVariable<Transform, DOTweenTransformValue>
    {
        public DOTweenTransformVariable(Transform defaultValue) : base(defaultValue)
        {
        }
    }
}
