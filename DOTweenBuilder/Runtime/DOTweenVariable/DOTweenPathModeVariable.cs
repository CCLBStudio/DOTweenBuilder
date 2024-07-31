using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenPathModeVariable : DOTweenVariable<PathMode, DOTweenPathModeValue>
    {
        public DOTweenPathModeVariable(PathMode defaultValue) : base(defaultValue)
        {
        }
    }
}
