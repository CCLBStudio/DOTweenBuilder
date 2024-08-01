using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CCLBStudio.DOTweenBuilder
{
    public class DOTweenBuilder : MonoBehaviour
    {
        #region Editor

#if UNITY_EDITOR

        public static string TweenElementsProperty => nameof(tweenElements);
        public static string LoopProperty => nameof(loop);
        public static string LoopTypeProperty => nameof(loopType);
        public static string OnCompletedProperty => nameof(onCompleted);

#endif
        #endregion
        public List<DOTweenElement> TweenElements => tweenElements;

        [Tooltip("Add this amount of time at the beginning of the Sequence.")]
        [Min(0f)] [SerializeField] private float initialDelay;
        [Tooltip("Will automatically play on Start if TRUE")]
        [SerializeField] private bool autoPlay;
        [Tooltip("If true, the sequence will infinitely loop.")]
        [SerializeField] private bool loop;
        [Tooltip("Which loop type to use.")]
        [SerializeField] private LoopType loopType = LoopType.Restart;
        
        [Tooltip("How to handle a play conflict, i.e. calling the Play() method when the Sequence is still running : Kill And Go Next : the currently running Sequence is stopped and a new one is started " +
                 "- Keep Playing Current : the currently running sequence keeps playing and the new one will not start - Rewinds And Go Next : rewinds the tween and launch the next one, firing relevant callback " +
                 "- Complete And Go Next : completes the tween and launch the next one, firing relevant callback")]
        [SerializeField] private StartConflictBehaviour playConflictStrategy = StartConflictBehaviour.KeepPlayingCurrent;
        [SerializeField] private UnityEvent onCompleted;
        
        [SerializeReference] private List<DOTweenElement> tweenElements = new List<DOTweenElement>();

        private enum StartConflictBehaviour {KeepPlayingCurrent, KillAndGoNext, RewindAndGoNext, CompleteAndGoNext}
        [NonSerialized] private Sequence _currentSequence;
        [NonSerialized] private bool _init;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            if (autoPlay)
            {
                Play();
            }
        }

        private void OnDestroy()
        {
            foreach (var elem in tweenElements)
            {
                elem.OnDestroy();
            }
        }

        public void Initialize()
        {
            if (_init)
            {
                return;
            }
            
            _init = true;
            foreach (var elem in tweenElements)
            {
                elem.Init();
            }
        }

        public void Play()
        {
            if (_currentSequence != null && _currentSequence.IsPlaying())
            {
                switch (playConflictStrategy)
                {
                    case StartConflictBehaviour.KeepPlayingCurrent:
                        return;
                    
                    case StartConflictBehaviour.KillAndGoNext:
                        _currentSequence.Kill();
                        break;

                    case StartConflictBehaviour.RewindAndGoNext:
                        _currentSequence.Rewind();
                        _currentSequence.Kill();
                        break;
                    
                    case StartConflictBehaviour.CompleteAndGoNext:
                        _currentSequence.Complete();
                        _currentSequence.Kill();
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            _currentSequence = DOTween.Sequence();
            if (initialDelay > 0f)
            {
                _currentSequence.SetDelay(initialDelay);
            }

            foreach (var elem in tweenElements.Where(x => x.IsProperlySetup()))
            {
                _currentSequence.Absorb(elem);
            }

            _currentSequence.onComplete += () => onCompleted?.Invoke();
            _currentSequence.SetLoops(loop ? -1 : 0, loopType).Play();
        }
    }
}
