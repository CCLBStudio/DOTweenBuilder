using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenAxis2DVariable : DOTweenVariable<DOTweenAxis2D, DOTweenAxis2DValue>
    {
        public DOTweenAxis2DVariable(DOTweenAxis2D defaultValue) : base(defaultValue)
        {
        }
    }
}
