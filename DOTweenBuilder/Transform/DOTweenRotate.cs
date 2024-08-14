using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenRotate : DOTweenGenericElement<Transform, Vector3>
    {
        [SerializeField] private DOTweenRotateModeVariable rotateMode = new(RotateMode.Fast);
        [SerializeField] private DOTweenSpaceVariable space = new(Space.World);
        public override Tween Generate()
        {
            return space.Value == Space.Self ? Target.DOLocalRotate(Value, Duration, rotateMode.Value) : Target.DORotate(Value, Duration, rotateMode.Value);
        }
    }
}
