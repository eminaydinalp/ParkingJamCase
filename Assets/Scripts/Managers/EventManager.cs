using System;
using UnityEngine;

namespace DefaultNamespace.Managers
{
    public static class EventManager
    {
        public static event Action<Touch> FingerPressed;

        public static void InvokeFingerPressed(Touch touch)
        {
            FingerPressed?.Invoke(touch);
        }

    }
}