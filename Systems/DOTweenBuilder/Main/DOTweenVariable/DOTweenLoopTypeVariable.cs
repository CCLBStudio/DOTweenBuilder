using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenLoopTypeVariable : DOTweenVariable<LoopType, DOTweenLoopTypeValue>
    {
        public DOTweenLoopTypeVariable(LoopType defaultValue) : base(defaultValue)
        {
        }
    }
}
