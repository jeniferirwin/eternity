﻿using UnityEngine;

namespace Eternity
{
    public class Utils
    {
        float xbounds = 124;
        float zbounds = 124;

        // if this returns 0,0,0, we need to ignore the attempt to spawn
        public Vector3 PickRandomLocation()
        {
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
                
                RaycastHit[] hits = Physics.BoxCastAll(potentialPos, halfExtents, Vector3.down, Quaternion.identity, 10);

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