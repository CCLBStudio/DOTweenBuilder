using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenLoopTypeVariable : DOTweenVariable<LoopType, DOTweenLoopTypeValue>
    {
        public DOTweenLoopTypeVariable(LoopType defaultValue) : base(defaultValue)
        {
        }
    }
}
