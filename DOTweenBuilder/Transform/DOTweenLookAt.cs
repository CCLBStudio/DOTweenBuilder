using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenLookAt : DOTweenGenericElement<Transform, Transform>
    {
#if UNITY_EDITOR

        protected override string GetDesiredValueName() => "Look At Target";

#endif

        [Tooltip("The eventual rotation axis to lock. You can input multiple axis if you separate them like this : AxisConstrain.X | AxisConstraint.Y.")]
        [SerializeField] private DOTweenAxisConstraintVariable axisConstraint = new(AxisConstraint.None);
        
        public override Tween Generate()
        {
            return Target.DOLookAt(Value.position, Duration, axisConstraint.Value);
        }
    }
}
