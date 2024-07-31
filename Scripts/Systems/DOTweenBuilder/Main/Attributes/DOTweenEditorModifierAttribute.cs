using System;

namespace CCLBStudio.DTB
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
