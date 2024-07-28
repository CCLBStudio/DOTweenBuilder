using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenVector2Variable : DOTweenVariable<Vector2, DOTweenVector2Value>
    {
        public DOTweenVector2Variable(Vector2 defaultValue) : base(defaultValue)
        {
        }
    }
}
