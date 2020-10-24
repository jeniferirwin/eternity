using UnityEngine;

namespace Eternity
{
    public class EternityUtils
    {
        // if this returns 0,0,0, we need to ignore the attempt to spawn
        public static Vector3 PickRandomLocation()
        {
            float xbounds = 73;
            float zbounds = 73;

            Vector3 potentialPos = new Vector3(0,0,0);
            Vector3 halfExtents = new Vector3(1,1,1);
            bool posFound = false;
            int tries = 1000;
            while (!posFound)
            {
                float xpos = Random.Range(-xbounds, xbounds);
                float zpos = Random.Range(-zbounds, zbounds);
                potentialPos = new Vector3(xpos, 0, zpos);

                Debug.Log("Testing location: " + potentialPos);
                
                RaycastHit[] hits = Physics.BoxCastAll(potentialPos + new Vector3(0,5,0), halfExtents, Vector3.down, Quaternion.identity, 10);

                foreach (RaycastHit hit in hits)
                {
                    Debug.Log("Found object: " + hit.collider.gameObject.name);
                }

                if (hits.Length == 1)
                {
                    posFound = true;
                    break;
                }

                tries--;

                if (tries <= 0)
                {
                    break;
                }
            }

            return potentialPos;
        }
    }
}