using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenLoopTypeVariable : DOTweenVariable<LoopType, DOTweenLoopTypeValue>
    {
        public DOTweenLoopTypeVariable(LoopType defaultValue) : base(defaultValue)
        {
        }
    }
}