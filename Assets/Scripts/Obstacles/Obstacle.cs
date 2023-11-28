using DG.Tweening;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        public void HandleCollision()
        {
            transform.DOKill(true);
            transform.DOPunchRotation(Vector3.right * 10f, 0.5f, 5, 0.5f);
        }
    }
}
