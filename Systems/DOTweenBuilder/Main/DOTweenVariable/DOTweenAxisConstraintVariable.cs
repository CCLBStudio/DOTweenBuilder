using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenAxisConstraintVariable : DOTweenVariable<AxisConstraint, DOTweenAxisConstraintValue>
    {
        public DOTweenAxisConstraintVariable(AxisConstraint defaultValue) : base(defaultValue)
        {
        }
    }
}
