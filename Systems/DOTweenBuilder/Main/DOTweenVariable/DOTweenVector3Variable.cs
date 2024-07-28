using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenVector3Variable : DOTweenVariable<Vector3, DOTweenVector3Value>
    {
        public DOTweenVector3Variable(Vector3 defaultValue) : base(defaultValue)
        {
        }
    }
}
