using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenAbsorbTypeVariable : DOTweenVariable<AbsorbType, DOTweenAbsorbTypeValue>
    {
        public DOTweenAbsorbTypeVariable(AbsorbType defaultValue) : base(defaultValue)
        {
        }
    }
}
