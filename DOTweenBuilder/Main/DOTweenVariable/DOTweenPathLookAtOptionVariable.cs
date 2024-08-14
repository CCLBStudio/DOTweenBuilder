using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenPathLookAtOptionVariable : DOTweenVariable<DOTweenPathLookAtOption, DOTweenPathLookAtOptionValue>
    {
        public DOTweenPathLookAtOptionVariable(DOTweenPathLookAtOption defaultValue) : base(defaultValue)
        {
        }
    }
}