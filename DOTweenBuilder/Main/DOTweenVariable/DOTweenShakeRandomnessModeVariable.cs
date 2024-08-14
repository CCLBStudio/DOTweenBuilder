using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenShakeRandomnessModeVariable : DOTweenVariable<ShakeRandomnessMode, DOTweenShakeRandomnessModeValue>
    {
        public DOTweenShakeRandomnessModeVariable(ShakeRandomnessMode defaultValue) : base(defaultValue)
        {
        }
    }
}