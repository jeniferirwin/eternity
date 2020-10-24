using UnityEngine;

namespace Eternity
{
    public class EternityUtils
    {
        public static Vector3 PickRandomLocation()
        {
            Physics.queriesHitTriggers = true;
            float xbounds = 73;
            float zbounds = 73;

            Vector3 potentialPos = new Vector3(0, 0, 0);
            Vector3 halfExtents = new Vector3(1, 1, 1);
            bool posFound = false;
            int tries = 3000;
            while (!posFound)
            {
                float xpos = Random.Range(-xbounds, xbounds);
                float zpos = Random.Range(-zbounds, zbounds);
                potentialPos = new Vector3(xpos, 0, zpos);

                RaycastHit[] hits = Physics.BoxCastAll(potentialPos + new Vector3(0, 25, 0), halfExtents, Vector3.down, Quaternion.identity, 50);

                if (hits.Length > 1)
                {
                    tries--;
                    if (tries <= 0)
                    {
                        Debug.Log("Could not find a suitable position for a spawn.");
                        return new Vector3(170,0,0);
                    }
                }
            }
            return potentialPos;
        }
    }
}