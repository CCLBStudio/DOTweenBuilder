using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenCameraVariable : DOTweenVariable<Camera, DOTweenCameraValue>
    {
        public DOTweenCameraVariable(Camera defaultValue) : base(defaultValue)
        {
        }
    }
}
