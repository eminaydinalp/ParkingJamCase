using System;
using Abstract.Collision;
using Cinemachine;
using Managers;
using UnityEngine;

namespace Spline
{
    public class SplineTrigger : MonoBehaviour, ICollide
    {
        [SerializeField] private CinemachinePathBase _path;
        BoxCollider boxCollider;
        public CinemachinePathBase Path => _path;
        
        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        public void HandleCollide()
        {
            EventManager.InvokeOnCollideSpline(this);
        }

        public Vector3 ClosestPoint(Transform carTransform)
        {
            return boxCollider.bounds.ClosestPoint(carTransform.position);
        }
    }
}