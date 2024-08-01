using System;

namespace CCLBStudio.DOTweenBuilder
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
