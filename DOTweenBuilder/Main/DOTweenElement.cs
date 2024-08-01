using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public abstract class DOTweenElement
    {
        #region Editor

#if UNITY_EDITOR

        public static string PrependIntervalProperty => nameof(prependInterval);
        public static string PrependIntervalValueProperty => nameof(prependIntervalValue);
        
        public static string PrependCallbackProperty => nameof(prependCallback);
        public static string PrependCallbackValueProperty => nameof(prependCallbackValue);
        
        public static string AppendIntervalProperty => nameof(appendInterval);
        public static string AppendIntervalValueProperty => nameof(appendIntervalValue);
        
        public static string AppendCallbackProperty => nameof(appendCallback);
        public static string AppendCallbackValueProperty => nameof(appendCallbackValue);

        public static string OnPlayCallbackProperty => nameof(onPlayCallback);
        public static string OnUpdateCallbackProperty => nameof(onUpdateCallback);
        public static string OnRewindCallbackProperty => nameof(onRewindCallback);
        public static string OnCompleteCallbackProperty => nameof(onCompleteCallback);
        public static string OnStepCompleteCallbackProperty => nameof(onStepCompleteCallback);
        public static string CallbacksExpandedProperty => nameof(callbacksExpanded);

        public static string LoopProperty => nameof(loop);
        public static string LoopCountProperty => nameof(loopCount);
        public static string LoopTypeProperty => nameof(loopType);
        public static string DelayProperty => nameof(delay);

        [SerializeField] private bool callbacksExpanded;

#endif


        #endregion

        public bool Enabled => enabled.Value;
        public AbsorbType AbsorbBehaviour => absorbBehaviour.Value;
        public float Duration => Mathf.Max(0f, duration.Value);
        public DOTweenEase Easing => easing;
        public bool SnapToInteger => snapToInteger.Value;
        public bool PrependInterval => prependInterval;
        public float PrependIntervalValue => prependIntervalValue;
        
        public bool AppendInterval => appendInterval;
        public float AppendIntervalValue => appendIntervalValue;
        
        public bool PrependCallback => prependCallback;
        public UnityEvent PrependCallbackValue => prependCallbackValue;
        
        public bool AppendCallback => appendCallback;
        public UnityEvent AppendCallbackValue => appendCallbackValue;
        public bool Loop => loop;
        public int LoopCount => loopCount;
        public LoopType LoopType => loopType;
        public float Delay => Mathf.Max(0f, delay.Value);

        public UnityEvent OnPlayCallback => onPlayCallback;
        public UnityEvent OnUpdateCallback => onUpdateCallback;
        public UnityEvent OnRewindCallback => onRewindCallback;
        public UnityEvent OnCompleteCallback => onCompleteCallback;
        public UnityEvent OnStepCompleteCallback => onStepCompleteCallback;
        
        [Tooltip("If FALSE, this tween will be ignored")]
        [SerializeField] private DOTweenBoolVariable enabled = new(true);
        [Tooltip("Determines how this Tween will be inserted in the sequence : Prepend = at the beginning, Append = at the end, Join = at the same time of the previous Tween.")]
        [SerializeField] private DOTweenAbsorbTypeVariable absorbBehaviour = new(AbsorbType.Append);
        [Tooltip("The duration of this Tween.")]
        [SerializeField] private DOTweenFloatVariable duration = new(1f);
        [Tooltip("The ease function to use for this Tween.")]
        [SerializeField] private DOTweenEase easing;
        [Tooltip("If TRUE, the tween will smoothly snap to integer.")]
        [SerializeField] protected DOTweenBoolVariable snapToInteger = new (false);
        [Tooltip("If TRUE, the tween will loop the given amount of times.")]
        [SerializeField] private bool loop;
        [Tooltip("Any value > 0 will add a delay of the given amount to this Tween.")]
        [SerializeField] private DOTweenFloatVariable delay =new(0f);
        [Tooltip("How many time this Tween will loop ?")]
        [Min(2)] [SerializeField] private int loopCount = 2;
        [Tooltip("Determines the loop behaviour. LoopType.Restart : When a loop ends it will restart from the beginning - LoopType.Yoyo : When a loop ends it will play backwards until it completes another loop, then forward again - LoopType.Incremental : Each time a loop ends the difference between its endValue and its startValue will be added to the endValue, thus creating tweens that increase their values with each loop cycle.")]
        [SerializeField] private LoopType loopType;
        [Tooltip("Add a delay at the beginning of the Sequence.")]
        [SerializeField] private bool prependInterval;
        [Tooltip("The delay to add.")]
        [SerializeField] private float prependIntervalValue;
        [Tooltip("Add a delay at the end of this Tween, delaying the next ones.")]
        [SerializeField] private bool appendInterval;
        [Tooltip("The delay to add.")]
        [SerializeField] private float appendIntervalValue;
        [Tooltip("Invoke an event at the beginning of the Sequence.")]
        [SerializeField] private bool prependCallback;
        [Tooltip("The event to invoke at the beginning of the Sequence.")]
        [SerializeField] private UnityEvent prependCallbackValue;
        [Tooltip("Invoke an event after this Tween has completed.")]
        [SerializeField] private bool appendCallback;
        [Tooltip("The event to invoke when this Tween ends.")]
        [SerializeField] private UnityEvent appendCallbackValue;
        [Tooltip("Event raised when this Tween starts.")]
        [SerializeField] private UnityEvent onPlayCallback;
        [Tooltip("Event raised every time this Tween updates.")]
        [SerializeField] private UnityEvent onUpdateCallback;
        [Tooltip("Event raised when this Tween rewinds.")]
        [SerializeField] private UnityEvent onRewindCallback;
        [Tooltip("Event raised when this Tween completes.")]
        [SerializeField] private UnityEvent onCompleteCallback;
        [Tooltip("Event raised when this Tween completes a loop step.")]
        [SerializeField] private UnityEvent onStepCompleteCallback;
        
        protected DOTweenElement()
        {
            easing.SetDefaultValues();
        }

        public virtual void Init()
        {

        }

        public virtual void OnDestroy()
        {

        }
        public abstract Tween Generate();
        public virtual bool IsProperlySetup() => true;
    }
}
