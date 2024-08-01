 using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
#if UNITY_EDITOR
    [DOTweenEditorModifier(typeof(DOTweenPathEditorModifier))]
#endif
    public class DOTweenPath : DOTweenGenericElement<Transform, Transform[]>
    {
        #if UNITY_EDITOR

        public static string LookAtOptionProperty => nameof(lookAtOption);
        public static string LookAtPositionProperty => nameof(lookAtPosition);
        public static string LookAtTargetProperty => nameof(lookAtTarget);
        public static string LookAtAheadProperty => nameof(lookAtAhead);
        protected override string GetDesiredValueName() => "Waypoints";

#endif
        
        [Tooltip("Determines which space to use, between world and local.")]
        [SerializeField] private DOTweenSpaceVariable space = new(Space.World);
        [Tooltip("The type of path : Linear (straight path), CatmullRom (curved CatmullRom path) or CubicBezier (curved path with 2 control points per each waypoint).")]
        [SerializeField] private DOTweenPathTypeVariable pathType = new(PathType.Linear);
        [Tooltip("The path mode, used to determine correct LookAt options: Ignore (ignores any lookAt option passed), 3D, side-scroller 2D, top-down 2D.")]
        [SerializeField] private DOTweenPathModeVariable pathMode = new(PathMode.Full3D);
        [Tooltip("The resolution of the path (useless in case of Linear paths) : higher resolutions make for more detailed curved paths but are more expensive. Defaults to 10, but a value of 5 is usually enough if you don't have dramatic long curves between waypoints.")]
        /*[Min(5)]*/ [SerializeField] private DOTweenIntVariable resolution = new(10);
        [Tooltip("If TRUE the path will be automatically closed.")]
        [SerializeField] private DOTweenBoolVariable closePath = new(false);
        [Tooltip("The eventual movement axis to lock. You can input multiple axis if you separate them like this : AxisConstrain.X | AxisConstraint.Y.")]
        [SerializeField] private DOTweenAxisConstraintVariable lockPosition = new(AxisConstraint.None);
        [Tooltip("The eventual rotation axis to lock. You can input multiple axis if you separate them like this : AxisConstrain.X | AxisConstraint.Y.")]
        [SerializeField] private DOTweenAxisConstraintVariable lockRotation = new(AxisConstraint.None);
        [Tooltip("The option to determine how the target will orient itself when moving along the path. Look At Ahead : will look at the next point in path - Look At Position : will look at a fixed Vector3 - Look At Target : will look at a Transform.")]
        [SerializeField] private DOTweenPathLookAtOptionVariable lookAtOption = new(DOTweenPathLookAtOption.LookAtAhead);
        [Tooltip("The fixed point in space to look at.")]
        [SerializeField] private DOTweenVector3Variable lookAtPosition = new(Vector3.zero);
        [SerializeField] private DOTweenTransformVariable lookAtTarget = new(null);
        [Tooltip("The lookAhead percentage to use when orienting to the path (0 to 1).")]
        /*[Range(0f, 1f)]*/ [SerializeField] private DOTweenFloatVariable lookAtAhead = new(0.01f);

        public override Tween Generate()
        {
            Vector3[] array = Value.Select(x => space.Value == Space.Self ? x.localPosition : x.position).ToArray();

            return lookAtOption.Value switch
            {
                DOTweenPathLookAtOption.LookAtPosition => space.Value == Space.Self ? Target.DOLocalPath(array, Duration, pathType.Value, pathMode.Value, resolution.Value).SetOptions(closePath.Value, lockPosition.Value, lockRotation.Value).SetLookAt(lookAtPosition.Value) : Target.DOPath(array, Duration, pathType.Value, pathMode.Value, resolution.Value).SetOptions(closePath.Value, lockPosition.Value, lockRotation.Value).SetLookAt(lookAtPosition.Value),
                DOTweenPathLookAtOption.LookAtTarget => space.Value == Space.Self ? Target.DOLocalPath(array, Duration, pathType.Value, pathMode.Value, resolution.Value).SetOptions(closePath.Value, lockPosition.Value, lockRotation.Value).SetLookAt(lookAtTarget.Value) : Target.DOPath(array, Duration, pathType.Value, pathMode.Value, resolution.Value).SetOptions(closePath.Value, lockPosition.Value, lockRotation.Value).SetLookAt(lookAtTarget.Value),
                DOTweenPathLookAtOption.LookAtAhead => space.Value == Space.Self ? Target.DOLocalPath(array, Duration, pathType.Value, pathMode.Value, resolution.Value).SetOptions(closePath.Value, lockPosition.Value, lockRotation.Value).SetLookAt(lookAtAhead.Value) : Target.DOPath(array, Duration, pathType.Value, pathMode.Value, resolution.Value).SetOptions(closePath.Value, lockPosition.Value, lockRotation.Value).SetLookAt(lookAtAhead.Value),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
