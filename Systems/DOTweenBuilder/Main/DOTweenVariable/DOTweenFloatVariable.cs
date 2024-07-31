using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenFloatVariable : DOTweenVariable<float, DOTweenFloatValue>
    {
        public DOTweenFloatVariable(float defaultValue) : base(defaultValue)
        {
        }
    }
}
