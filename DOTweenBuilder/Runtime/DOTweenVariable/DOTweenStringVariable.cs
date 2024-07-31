using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenStringVariable : DOTweenVariable<string, DOTweenStringValue>
    {
        public DOTweenStringVariable(string defaultValue) : base(defaultValue)
        {
        }
    }
}
