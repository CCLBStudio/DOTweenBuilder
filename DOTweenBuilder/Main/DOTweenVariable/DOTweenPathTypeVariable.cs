using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenPathTypeVariable : DOTweenVariable<PathType, DOTweenPathTypeValue>
    {
        public DOTweenPathTypeVariable(PathType defaultValue) : base(defaultValue)
        {
        }
    }
}
