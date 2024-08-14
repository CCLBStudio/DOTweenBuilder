using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    public class DOTMaxAttribute : PropertyAttribute
    {
        public float maxF;
        public int max;

        public DOTMaxAttribute(float max)
        {
            maxF = max;
            this.max = (int)max;
        }
    
        public DOTMaxAttribute(int max)
        {
            maxF = max;
            this.max = max;
        }
    }
}