using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenVector4Variable : DOTweenVariable<Vector4, DOTweenVector4Value>
    {
        public DOTweenVector4Variable(Vector4 defaultValue) : base(defaultValue)
        {
        }
    }
}