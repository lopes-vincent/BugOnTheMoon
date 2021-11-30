using UnityEngine;
using System.Collections;

namespace StellarSkyDemo
{

    public class CameraController : MonoBehaviour
    {

        public float Sensitivity;
        public bool YInvert;

        private void Update()
        {
            float rotationY = Input.GetAxis("Mouse X") * Sensitivity;
            float rotationX = 0;
            if (YInvert)
            {
                rotationX = -Input.GetAxis("Mouse Y") * Sensitivity;
            }
            else
            {
                rotationX = Input.GetAxis("Mouse Y") * Sensitivity;
            }

            transform.Rotate(new Vector3(rotationX, rotationY));
            Vector3 ea = transform.eulerAngles;
            transform.eulerAngles = new Vector3(ea.x, ea.y, 0);
        }

    }
}