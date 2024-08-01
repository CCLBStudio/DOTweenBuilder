using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenRotate : DOTweenGenericElement<Transform, Vector3>
    {
        [SerializeField] private RotateMode rotateMode = RotateMode.Fast;
        [SerializeField] private Space space = Space.World;
        public override Tween Generate()
        {
            return space == Space.Self ? Target.DOLocalRotate(Value, Duration, rotateMode) : Target.DORotate(Value, Duration, rotateMode);
        }
    }
}
