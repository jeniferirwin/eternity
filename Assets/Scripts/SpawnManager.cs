using UnityEngine;
using System.Collections.Generic;

namespace Eternity
{
    public class SpawnManager : MonoBehaviour
    {
        public List<Vector3> digSites = new List<Vector3>();
        public List<Vector3> enemies = new List<Vector3>();
        public List<Vector3> safeZones = new List<Vector3>();
        public List<Vector3> healingSafeZones = new List<Vector3>();
        public List<List<Vector3>> allSpawns = new List<List<Vector3>>();

        public float bounds = 73;
        
        public ObjectPool digSitePool;
        public ObjectPool safeZonePool;
        public ObjectPool healingSafeZonePool;
        public ObjectPool enemyPool;
        public Modifiers modifiers;

        public void Awake()
        {
            allSpawns.Add(digSites);
            allSpawns.Add(enemies);
            allSpawns.Add(safeZones);
            allSpawns.Add(healingSafeZones);
        }
        
        public void SpawnDigSites()
        {
            for (int i = 0; i < modifiers.NumDigSites; i++)
            {

            }
        }
        
        public void SpawnEnemies()
        {
            for (int i = 0; i < modifiers.NumEnemies; i++)
            {

            }
        }
        
        public void SpawnSafeZones()
        {
            for (int i = 0; i < modifiers.NumSafeZones; i++)
            {

            }
        }
        
        public void SpawnHealingSafeZones()
        {
            for (int i = 0; i < modifiers.NumHealingSafeZones; i++)
            {

            }
        }

        public void ResetAllSpawns()
        {
            ClearSpawns();
            digSites = BuildSpawnList(modifiers.NumDigSites);
            enemies = BuildSpawnList(modifiers.NumEnemies);
            safeZones = BuildSpawnList(modifiers.NumSafeZones);
            healingSafeZones = BuildSpawnList(modifiers.NumHealingSafeZones);
        }

        public void ClearSpawns()
        {
            foreach (List<Vector3> list in allSpawns)
            {
                list.Clear();
            }
        }
        
        public bool IsClearSpawnArea(Vector3 position)
        {
            foreach (List<Vector3> list in allSpawns)
            {
                foreach (Vector3 pos in list)
                {
                    if (Vector3.Distance(position, pos) < 4)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<Vector3> BuildSpawnList(int numSpawns)
        {
            List<Vector3> spawnList = new List<Vector3>();
            for (int i = 0; i < numSpawns; i++)
            {
                float tries = 1000;

                for (int x = 0; x < tries; x++)
                {
                    Vector3 newPos = RandomPosition();
                    if (IsClearSpawnArea(newPos))
                    {
                        spawnList.Add(newPos);
                        break;
                    }
                    else
                    {
                        tries--;
                        if (tries <= 0)
                        {
                            spawnList.Add(new Vector3(170,0,0));
                        }
                    }
                }
            }
            return spawnList;
        }

        public Vector3 RandomPosition()
        {
            float xpos = Random.Range(-bounds, bounds);
            float zpos = Random.Range(-bounds, bounds);
            return new Vector3(xpos, 0, zpos);
        }
    }
}