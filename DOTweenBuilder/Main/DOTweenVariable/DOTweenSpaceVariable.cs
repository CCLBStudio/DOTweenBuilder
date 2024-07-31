using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenSpaceVariable : DOTweenVariable<Space, DOTweenSpaceValue>
    {
        public DOTweenSpaceVariable(Space defaultValue) : base(defaultValue)
        {
        }
    }
}
