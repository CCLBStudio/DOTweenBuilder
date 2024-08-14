using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenIntVariable : DOTweenVariable<int, DOTweenIntValue>
    {
        public DOTweenIntVariable(int defaultValue) : base(defaultValue)
        {
        }
    }
}