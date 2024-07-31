using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenEaseVariable : DOTweenVariable<DOTweenEase, DOTweenEaseValue>
    {
        public DOTweenEaseVariable(DOTweenEase defaultValue) : base(defaultValue)
        {
        }
    }
}
