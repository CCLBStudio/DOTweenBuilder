using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenRotateModeVariable : DOTweenVariable<RotateMode, DOTweenRotateModeValue>
    {
        public DOTweenRotateModeVariable(RotateMode defaultValue) : base(defaultValue)
        {
        }
    }
}