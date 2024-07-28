using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenAxisVariable : DOTweenVariable<DOTweenAxis, DOTweenAxisValue>
    {
        public DOTweenAxisVariable(DOTweenAxis defaultValue) : base(defaultValue)
        {
        }
    }
}
