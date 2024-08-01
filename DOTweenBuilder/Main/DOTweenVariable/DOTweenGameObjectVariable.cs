using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenGameObjectVariable : DOTweenVariable<GameObject, DOTweenGameObjectValue>
    {
        public DOTweenGameObjectVariable(GameObject defaultValue) : base(defaultValue)
        {
        }
    }
}
