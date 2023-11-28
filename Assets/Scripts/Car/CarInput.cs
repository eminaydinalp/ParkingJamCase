using UnityEngine;

namespace Car
{
    public class CarInput : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _carMask;
        [SerializeField] private float threshold;
        
        private bool isSwiped;

        private void Update()
        {
            TouchControl();
        }

        private void TouchControl()
        {
            if (Input.touchCount <= 0)
            {
                return;
            }

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    if (touch.deltaPosition.magnitude > threshold && !isSwiped)
                    {
                        isSwiped = true;
                        OnFingerPressed(touch);
                    }
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    isSwiped = false;
                    break;
                case TouchPhase.Canceled:
                    isSwiped = false;
                    break;
            }
        }

        
        private void OnFingerPressed(Touch touch)
        {
            var ray = _camera.ScreenPointToRay(touch.position);
            var isHit = Physics.Raycast(ray, out var hitInfo, 1000, _carMask.value);

            if (!isHit)
            {
                return;
            }

            if (!hitInfo.transform.TryGetComponent<CarController>(out var carController))
            {
                return;
            }
            
            if(carController.isMove) return;
            
            var delta = touch.deltaPosition.normalized;
            var convertedDirection = new Vector3(delta.x, 0, delta.y);
            
            carController.MoveStart(convertedDirection);
        }
    }
}