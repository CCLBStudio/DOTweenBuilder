using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    public class DOTRangeAttribute : PropertyAttribute
    {
        public readonly int min;
        public readonly int max;
        public readonly float minF;
        public readonly float maxF;

        public DOTRangeAttribute(float min, float max)
        {
            minF = min;
            maxF = max;
            this.min = (int)min;
            this.max = (int)max;
        }
        
        public DOTRangeAttribute(int min, int max)
        {
            minF = min;
            maxF = max;
            this.min = min;
            this.max = max;
        }
    }
}