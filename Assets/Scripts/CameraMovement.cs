using UnityEngine;

namespace Eternity
{
    public class CameraMovement : MonoBehaviour
    {
        public GameObject player;
        public Vector3 hoverPos;

        private Vector3 lastFramePos;
        private float xbound = 109;
        private float zbound = 115;

        // left boundary is -109
        // right boundary is 109?
        // upper boundary is 115
        // lower boundary is -115?

        void LateUpdate()
        {
            if (player.transform.position != transform.position + hoverPos)
            {
                Reposition();
            }
        }

        private void Reposition()
        {
            Vector3 newPos = player.transform.position;
            if (newPos.x < -xbound || newPos.x > xbound)
            {
                newPos.x = lastFramePos.x;
            }
            if (newPos.z < -zbound || newPos.z > zbound)
            {
                newPos.z = lastFramePos.z;
            }
            transform.position = newPos + hoverPos;
            lastFramePos = transform.position;
        }
    }
}