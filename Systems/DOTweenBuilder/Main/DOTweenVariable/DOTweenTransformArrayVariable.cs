using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenTransformArrayVariable : DOTweenVariable<Transform[], DOTweenTransformArrayValue>
    {
        public DOTweenTransformArrayVariable(Transform[] defaultValue) : base(defaultValue)
        {
        }
    }
}
