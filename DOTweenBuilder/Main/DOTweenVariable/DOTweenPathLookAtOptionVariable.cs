using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenPathLookAtOptionVariable : DOTweenVariable<DOTweenPathLookAtOption, DOTweenPathLookAtOptionValue>
    {
        public DOTweenPathLookAtOptionVariable(DOTweenPathLookAtOption defaultValue) : base(defaultValue)
        {
        }
    }
}
