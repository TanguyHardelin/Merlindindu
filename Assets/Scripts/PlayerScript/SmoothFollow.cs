using UnityEngine;

namespace UnityStandardAssets.Utility
{
    public class SmoothFollow : MonoBehaviour
    {

        // The target we are following
        [SerializeField] Transform target;

        public float distance = 5.0f;
        public float xSpeed = 120.0f;
        public float ySpeed = 120.0f;

        public float yMinLimit = -20f;
        public float yMaxLimit = 80f;

        public float distanceMin = .5f;
        public float distanceMax = 15f;
        private Rigidbody rb;

        float x = 50f;
        float y = 0.0f;

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            // Make the rigid body not change rotation
            if (rb != null)
            {
                rb.freezeRotation = true;
            }
        }
        
        void LateUpdate()
        {
            if (target)
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                Quaternion rotation = Quaternion.Euler(y, x, 0);

                distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

                // Pour faire que la camera traverse un objet lorsqu'il est entre le joueur et la camera
                /* RaycastHit hit;
                if (Physics.Linecast(target.position, transform.position, out hit))
                {
                    distance -= hit.distance;
                }*/
                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = rotation * negDistance + target.position;
                position.y += 1f;

                transform.rotation = rotation;
                transform.position = position;
            }
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }

    }
}