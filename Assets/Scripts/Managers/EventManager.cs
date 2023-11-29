using Spline;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public static class EventManager
    {
        public static event UnityAction OnCollideObstacle;

        public static void InvokeOnCollideObstacle()
        {
            OnCollideObstacle?.Invoke();
        }
        
        public static event UnityAction<Transform> OnCollideObstacleWithTransform;

        public static void InvokeOnCollideObstacleWithTransform(Transform obstacle)
        {
            OnCollideObstacleWithTransform?.Invoke(obstacle);
        }
        
        public static event UnityAction<SplineTrigger> OnCollideSpline;

        public static void InvokeOnCollideSpline(SplineTrigger splineTrigger)
        {
            OnCollideSpline?.Invoke(splineTrigger);
        }

    }
}