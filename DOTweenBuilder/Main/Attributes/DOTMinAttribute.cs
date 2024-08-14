using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    public class DOTMinAttribute : PropertyAttribute
    {
        public float minF;
        public int min;

        public DOTMinAttribute(float min)
        {
            minF = min;
            this.min = (int)min;
        }
    
        public DOTMinAttribute(int min)
        {
            minF = min;
            this.min = min;
        }
    }
}