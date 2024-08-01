using System;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public abstract class DOTweenRendererElement<T> : DOTweenGenericElement<Renderer, T>
    {
        [Tooltip("The property name to animate. Leave empty to use the default property (when applicable).")]
        [SerializeField] protected string property;
        [Tooltip("If TRUE, this Tween will use the shared materials. This will cause any renderer with the same material to be animated, and the asset will save the value. Not recommended.")]
        [SerializeField] protected bool useSharedMaterials;
        
        [NonSerialized] private bool hasCachedProperty = false;
        protected int propertyId;

        protected virtual void AssignPropertyId()
        {
            if (!string.IsNullOrEmpty(property) && !hasCachedProperty)
            {
                propertyId = Shader.PropertyToID(property);
                hasCachedProperty = true;
            }
        }
    }
}
