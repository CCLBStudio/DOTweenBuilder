using System;
using UnityEngine.UI;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenImageVariable : DOTweenVariable<Image, DOTweenImageValue>
    {
        public DOTweenImageVariable(Image defaultValue) : base(defaultValue)
        {
        }
    }
}
