using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
namespace CCLBStudio.Systems.DOTweenBuilder
{
    [Serializable]
    public class DOTweenTextMeshProUGUIVariable : DOTweenVariable<TextMeshProUGUI, DOTweenTextMeshProUGUIValue>
    {
        public DOTweenTextMeshProUGUIVariable(TextMeshProUGUI defaultValue) : base(defaultValue)
        {
        }
    }
}
