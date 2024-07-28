using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenSpaceVariable : DOTweenVariable<Space, DOTweenSpaceValue>
    {
        public DOTweenSpaceVariable(Space defaultValue) : base(defaultValue)
        {
        }
    }
}
