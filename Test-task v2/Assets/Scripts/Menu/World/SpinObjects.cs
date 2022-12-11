using UnityEngine;

namespace Game
{
    namespace Menu
    {
        [RequireComponent(typeof(Rigidbody))]
        public class SpinObjects : MonoBehaviour
        {
            [Range(0, 10f)]
            [SerializeField]
            private float rotateSpeed;

            [SerializeField]
            private Vector3 directionRotate;

            private Rigidbody body;

            private void Start()
            {
                body = GetComponent<Rigidbody>();
            }

            private void Update()
            {
                body.AddTorque(directionRotate * rotateSpeed * Time.deltaTime);
            }
        }
    }
}