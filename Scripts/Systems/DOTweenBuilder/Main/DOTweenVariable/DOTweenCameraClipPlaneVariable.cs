using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenCameraClipPlaneVariable : DOTweenVariable<DOTweenCameraClipPlane, DOTweenCameraClipPlaneValue>
    {
        public DOTweenCameraClipPlaneVariable(DOTweenCameraClipPlane defaultValue) : base(defaultValue)
        {
        }
    }
}
