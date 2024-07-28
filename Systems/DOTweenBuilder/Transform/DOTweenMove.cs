using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenMove : DOTweenGenericElement<Transform, Vector3>
    {
        [SerializeField] private Space space = Space.World;
        
        public override Tween Generate()
        {
            return space == Space.Self ? Target.DOLocalMove(Value, Duration, SnapToInteger) : Target.DOMove(Value, Duration, SnapToInteger);
        }
    }
}
