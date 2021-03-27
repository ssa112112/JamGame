using UnityEngine;

namespace alpha_2.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform targetPlayer = null;
        private Vector3 _offset;
        private Camera _camera;

        public float cameraSpeed;

        void Start()
        {
            _offset = targetPlayer.position - transform.position;
            _camera = GetComponent<Camera>();
        }

        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                targetPlayer.position - _offset, cameraSpeed * Time.deltaTime);
        }
    }
}