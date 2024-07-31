using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenAxisVariable : DOTweenVariable<DOTweenAxis, DOTweenAxisValue>
    {
        public DOTweenAxisVariable(DOTweenAxis defaultValue) : base(defaultValue)
        {
        }
    }
}
