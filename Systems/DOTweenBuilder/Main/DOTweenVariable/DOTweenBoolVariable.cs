using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenBoolVariable : DOTweenVariable<bool, DOTweenBoolValue>
    {
        public DOTweenBoolVariable(bool defaultValue) : base(defaultValue)
        {
        }
    }
}
