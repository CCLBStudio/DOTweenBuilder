using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenClipPlane : DOTweenGenericElement<Camera, float>
    {
        [SerializeField] private DOTweenCameraClipPlaneVariable plane = new(DOTweenCameraClipPlane.FarClip);
        public override Tween Generate()
        {
            return plane.Value == DOTweenCameraClipPlane.NearClip ? Target.DONearClipPlane(Value, Duration) : Target.DOFarClipPlane(Value, Duration);
        }
    }
}
