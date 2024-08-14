using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenFloatVariable : DOTweenVariable<float, DOTweenFloatValue>
    {
        public DOTweenFloatVariable(float defaultValue) : base(defaultValue)
        {
        }
    }
}