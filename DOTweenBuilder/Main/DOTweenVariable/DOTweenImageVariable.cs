using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenImageVariable : DOTweenVariable<Image, DOTweenImageValue>
    {
        public DOTweenImageVariable(Image defaultValue) : base(defaultValue)
        {
        }
    }
}
