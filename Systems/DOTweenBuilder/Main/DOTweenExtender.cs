using System;
using DG.Tweening;
using TMPro;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    public static class DOTweenExtender
    {
        public static void Absorb(this Sequence sq, DOTweenElement elem)
        {
            if (!elem.Enabled)
            {
                return;
            }
            
            if (elem.PrependInterval)
            {
                sq.PrependInterval(elem.PrependIntervalValue);
            }

            if (elem.PrependCallback)
            {
                sq.PrependCallback(elem.PrependCallbackValue.Invoke);
                
            }
            
            switch (elem.AbsorbBehaviour)
            {
                case AbsorbType.Prepend:
                    sq.Prepend(BuildTween(elem));
                    break;
                
                case AbsorbType.Append:
                    sq.Append(BuildTween(elem));
                    break;
                
                case AbsorbType.Join:
                    sq.Join(BuildTween(elem));
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (elem.AppendCallback)
            {
                sq.AppendCallback(elem.AppendCallbackValue.Invoke);
            }

            if (elem.AppendInterval)
            {
                sq.AppendInterval(elem.AppendIntervalValue);
            }
        }

        private static Tween BuildTween(DOTweenElement elem)
        {
            Tween tween = elem.Generate();
            if (elem.Easing.useCustomCurve)
            {
                tween.SetEase(elem.Easing.curve);
            }
            else
            {
                tween.SetEase(elem.Easing.ease);
            }

            if (elem.Delay > 0f)
            {
                tween.SetDelay(elem.Delay);
            }

            if (elem.Loop)
            {
                tween.SetLoops(elem.LoopCount, elem.LoopType);
            }
            
            if (elem.OnPlayCallback.GetPersistentEventCount() > 0)
            {
                tween.onPlay += elem.OnPlayCallback.Invoke;
            }
            
            if (elem.OnUpdateCallback.GetPersistentEventCount() > 0)
            {
                tween.onUpdate += elem.OnUpdateCallback.Invoke;
            }
            
            if (elem.OnRewindCallback.GetPersistentEventCount() > 0)
            {
                tween.onRewind += elem.OnRewindCallback.Invoke;
            }
            
            if (elem.OnCompleteCallback.GetPersistentEventCount() > 0)
            {
                tween.onComplete += elem.OnCompleteCallback.Invoke;
            }
            
            if (elem.OnStepCompleteCallback.GetPersistentEventCount() > 0)
            {
                tween.onStepComplete += elem.OnStepCompleteCallback.Invoke;
            }
            
            return tween;
        }

        public static Tweener DORatioCounter(this TextMeshProUGUI txt, int startValue, int endValue, int totalValue, float duration)
        {
            return DOTween.To(() => startValue, x => txt.text = $"{x} / {totalValue.ToString()}", endValue, duration);
        }
    }
}
