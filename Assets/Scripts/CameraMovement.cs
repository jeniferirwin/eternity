using UnityEngine;

namespace Eternity
{
    public class CameraMovement : MonoBehaviour
    {
        public GameObject player;
        public Vector3 hoverPos;

        private Vector3 lastFramePos;
        private float xbound = 50;
        private float zbound = 60;

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