using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenMove : DOTweenGenericElement<Transform, Vector3>
    {
        [SerializeField] private DOTweenSpaceVariable space = new(Space.World);
        
        public override Tween Generate()
        {
            return space.Value == Space.Self ? Target.DOLocalMove(Value, Duration, SnapToInteger) : Target.DOMove(Value, Duration, SnapToInteger);
        }
    }
}
