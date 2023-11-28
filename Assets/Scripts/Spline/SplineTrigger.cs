using Cinemachine;
using UnityEngine;

namespace Spline
{
    public class SplineTrigger : MonoBehaviour
    {
        [SerializeField] private CinemachinePathBase _path;

        public CinemachinePathBase Path => _path;
    }
}