using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenAnchorTypeVariable : DOTweenVariable<DOTweenAnchorType, DOTweenAnchorTypeValue>
    {
        public DOTweenAnchorTypeVariable(DOTweenAnchorType defaultValue) : base(defaultValue)
        {
        }
    }
}
