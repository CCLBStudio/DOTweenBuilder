using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenTransformArrayVariable : DOTweenVariable<Transform[], DOTweenTransformArrayValue>
    {
        public DOTweenTransformArrayVariable(Transform[] defaultValue) : base(defaultValue)
        {
        }
    }
}
