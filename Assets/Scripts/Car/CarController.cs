using Abstract.Collision;
using Cinemachine;
using Managers;
using Spline;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        public CarSo carSo;
        
        private CarMovement _carMovement;
        private CarBackFire _carBackFire;
        private CarSplineFollow _carSplineFollow;
        private CarInput _carInput;
        
        public bool isMove;
        public bool isSwipe;
                
        private bool _isSplineFollow;

        [SerializeField] private Camera camera;
        [SerializeField] private CinemachineDollyCart cinemachineDollyCart;
        [SerializeField] private LayerMask layerMask;

        private void Awake()
        {
            _carMovement = new CarMovement(this);
            _carBackFire = new CarBackFire(this);
            _carSplineFollow = new CarSplineFollow(this, cinemachineDollyCart);
            _carInput = new CarInput(this, camera, layerMask);
        }

        private void OnEnable()
        {
            EventManager.OnCollideObstacle += MoveStop;
            EventManager.OnCollideObstacleWithTransform += CarBackFire;
            EventManager.OnCollideSpline += CarFollowSpline;
        }
        
        private void OnDisable()
        {
            EventManager.OnCollideObstacle -= MoveStop;
            EventManager.OnCollideObstacleWithTransform -= CarBackFire;
            EventManager.OnCollideSpline -= CarFollowSpline;
        }

        private void Update()
        {
            _carInput.TouchControl();

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

        private void MoveStop()
        {
            isMove = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollide collide))
            {
                collide.HandleCollide();
            }
        }

        private void CarBackFire(Transform obstacle)
        {
            _carBackFire.BackFire(obstacle);
        }

        private void CarFollowSpline(SplineTrigger splineTrigger)
        {
            if(_isSplineFollow) return;
            _isSplineFollow = true;
            _carSplineFollow.FollowSpline(splineTrigger);
        }
    }
}
