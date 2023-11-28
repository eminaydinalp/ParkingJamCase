using System;
using DG.Tweening;
using Obstacles;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        private CarMovement _carMovement;

        public bool isMove;
        public float moveSpeed;
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
