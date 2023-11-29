using Abstract.Collision;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour, ICollide
    {
        [SerializeField] private float shakeRotateForce;
        [SerializeField] private float shakeRotateDuration;
        [SerializeField] private int shakeRotateVibrato;
        [SerializeField] private float shakeRotateElasticity;
        private void ShakeObstacle()
        {
            transform.DOKill(true);
            transform.DOPunchRotation(Vector3.right * shakeRotateForce, shakeRotateDuration, shakeRotateVibrato, shakeRotateElasticity);
        }

        public void HandleCollide()
        {
            EventManager.InvokeOnCollideObstacle();
            EventManager.InvokeOnCollideObstacleWithTransform(transform);
            ShakeObstacle();
        }
    }
}
