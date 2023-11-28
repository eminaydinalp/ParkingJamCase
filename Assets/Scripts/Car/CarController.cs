using System;
using Cinemachine;
using DG.Tweening;
using Obstacles;
using Spline;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        private CarMovement _carMovement;

        public bool isMove;
        public float moveSpeed;
        
        [SerializeField] private CinemachineDollyCart cinemachineDollyCart;
        [SerializeField] private bool isSpline;

        private void Awake()
        {
            _carMovement = new CarMovement(this);
        }

        private void Update()
        {
            if (isMove)
            {
                _carMovement.MoveStraight();
            }
        }

        public void MoveStart(Vector3 direction)
        {
            _carMovement.SetMoveDirection(direction);
            isMove = true;
        }

        public void MoveStop()
        {
            isMove = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Obstacle obstacle = other.GetComponent<Obstacle>();

            if (obstacle != null)
            {
                obstacle.HandleCollision();
                MoveStop();
                CollisionObstacle(other.transform);
            }

            SplineTrigger splineTrigger = other.GetComponent<SplineTrigger>();

            if (splineTrigger != null && !isSpline)
            {
                isSpline = true;

                Vector3 point = other.bounds.ClosestPoint(transform.position);

                var path = splineTrigger.Path;
                cinemachineDollyCart.m_Path = path;

                var closestPoint = path.FindClosestPoint(point, 0, -1, 10);
                closestPoint = path.FromPathNativeUnits(closestPoint, cinemachineDollyCart.m_PositionUnits);

                var startPosition = path.EvaluatePositionAtUnit(closestPoint, cinemachineDollyCart.m_PositionUnits);
                var startRotation = path.EvaluateOrientationAtUnit(closestPoint, cinemachineDollyCart.m_PositionUnits);

                transform.DOMove(startPosition, 0.2f);

                transform.DORotateQuaternion(startRotation, 0.2f)
                    .OnComplete(() =>
                    {
                        cinemachineDollyCart.m_Position = closestPoint;
                        cinemachineDollyCart.enabled = true;

                        DOVirtual.Float(cinemachineDollyCart.m_Speed / 3f, cinemachineDollyCart.m_Speed, 0.2f, value => cinemachineDollyCart.m_Speed = value);
                    });
            }
        }

        private void CollisionObstacle(Transform obstacle)
        {
            var direction =  obstacle.position - transform.position;
            var dot = Vector3.Dot(transform.forward.normalized , direction.normalized);
            
            var moveDireciton = transform.forward * MathF.Sign(-dot);

            
            transform.DOLocalMove(transform.localPosition + moveDireciton * 0.3f, 0.2f);
            transform.DOPunchRotation(direction * 7f, 0.3f, 8);

        }
    }
}
