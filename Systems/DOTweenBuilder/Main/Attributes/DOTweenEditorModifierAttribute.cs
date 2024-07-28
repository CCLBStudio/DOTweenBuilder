using System;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DOTweenEditorModifierAttribute : Attribute
    {
        public Type Type { get; }

        public DOTweenEditorModifierAttribute(Type type)
        {
            Type = type;
        }
    }
}
