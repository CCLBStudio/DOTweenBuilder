using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenCameraClipPlaneVariable : DOTweenVariable<DOTweenCameraClipPlane, DOTweenCameraClipPlaneValue>
    {
        public DOTweenCameraClipPlaneVariable(DOTweenCameraClipPlane defaultValue) : base(defaultValue)
        {
        }
    }
}