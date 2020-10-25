using UnityEngine;
using System.Collections.Generic;

namespace Eternity
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject player;

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
        public List<ObjectPool> allPools = new List<ObjectPool>();

        public Modifiers modifiers;

        public float minDistance;

        public void Awake()
        {
            allSpawns.Add(digSites);
            allSpawns.Add(enemies);
            allSpawns.Add(safeZones);
            allSpawns.Add(healingSafeZones);

            allPools.Add(digSitePool);
            allPools.Add(safeZonePool);
            allPools.Add(healingSafeZonePool);
            allPools.Add(enemyPool);
        }

        public void SpawnDigSites()
        {
            for (int i = 0; i < modifiers.NumDigSites; i++)
            {
                GameObject digSite = digSitePool.GetRandomInactiveObject();
                digSite.transform.position = digSites[i];
                digSite.SetActive(true);
            }
        }

        public void SpawnEnemies()
        {
            for (int i = 0; i < modifiers.NumEnemies; i++)
            {
                GameObject enemy = enemyPool.GetRandomInactiveObject();
                enemy.transform.position = enemies[i];
                enemy.SetActive(true);
            }
        }

        public void SpawnSafeZones()
        {
            Debug.Log("Spawning number of safe zones: " + modifiers.NumSafeZones);
            for (int i = 0; i < modifiers.NumSafeZones; i++)
            {
                GameObject safeZone = safeZonePool.GetRandomInactiveObject();
                safeZone.transform.position = safeZones[i];
                safeZone.SetActive(true);
            }
        }

        public void SpawnHealingSafeZones()
        {
            Debug.Log("Spawning number of healing safe zones: " + modifiers.NumHealingSafeZones);
            for (int i = 0; i < modifiers.NumHealingSafeZones; i++)
            {
                GameObject healingSafeZone = healingSafeZonePool.GetRandomInactiveObject();
                healingSafeZone.transform.position = healingSafeZones[i];
                healingSafeZone.SetActive(true);
            }
        }

        public void ResetAllSpawns()
        {
            DeactivateAllPooledObjects();
            safeZones = BuildSafeZoneSpawnList(modifiers.NumSafeZones);
            healingSafeZones = BuildHealingSafeZoneSpawnList(modifiers.NumHealingSafeZones);
            digSites = BuildDigsiteSpawnList(modifiers.NumDigSites);
            enemies = BuildEnemySpawnList(modifiers.NumEnemies);
        }

        public void DeactivateAllPooledObjects()
        {
            foreach (ObjectPool pool in allPools)
            {
                pool.DeactivateAllObjects();
            }
        }

        public List<Vector3> BuildSafeZoneSpawnList(int numSpawns)
        {
            List<Vector3> newSafeZones = new List<Vector3>();
            for (int i = 0; i < numSpawns; i++)
            {
                Vector3 newPos = RandomPosition();
                while (!IsPosDistancedFromList(newPos, newSafeZones, 25))
                {
                    newPos = RandomPosition();
                }
                newSafeZones.Add(newPos);
            }
            return newSafeZones;
        }

    public List<Vector3> BuildHealingSafeZoneSpawnList(int numSpawns)
    {
        List<Vector3> newHealingSafeZones = new List<Vector3>();
        for (int i = 0; i < numSpawns; i++)
        {
            Vector3 newPos = RandomPosition();
            while (!IsPosDistancedFromList(newPos, newHealingSafeZones, 40) && !IsPosDistancedFromList(newPos, healingSafeZones, 25))
            {
                newPos = RandomPosition();
            }
            newHealingSafeZones.Add(newPos);
        }
        return newHealingSafeZones;
    }

    public List<Vector3> BuildDigsiteSpawnList(int numSpawns)
    {
        List<Vector3> newDigSites = new List<Vector3>();
        for (int i = 0; i < numSpawns; i++)
        {
            Vector3 newPos = RandomPosition();
            while (!IsPosDistancedFromList(newPos, newDigSites, 15) && !IsPosDistancedFromList(newPos, healingSafeZones, 5) && !IsPosDistancedFromList(newPos, safeZones, 5))
            {
                newPos = RandomPosition();
            }
            newDigSites.Add(newPos);
        }
        return newDigSites;
    }

    public List<Vector3> BuildEnemySpawnList(int numSpawns)
    {
        List<Vector3> newEnemies = new List<Vector3>();
        for (int i = 0; i < numSpawns; i++)
        {
            Vector3 newPos = RandomPosition();
            while (!IsPosDistancedFromList(newPos, newEnemies, 8) && Vector3.Distance(newPos, player.transform.position) < 15)
            {
                newPos = RandomPosition();
            }
            newEnemies.Add(newPos);
        }
        return newEnemies;
    }

    public bool IsPosDistancedFromList(Vector3 pos, List<Vector3> list, float distance)
    {
        if (list.Count < 1)
            return true;

        foreach (Vector3 value in list)
        {
            if (Vector3.Distance(value, pos) < distance)
            {
                return false;
            }
        }
        return true;
    }

    public Vector3 RandomPosition()
    {
        float xpos = Random.Range(-bounds, bounds);
        float zpos = Random.Range(-bounds, bounds);
        return new Vector3(xpos, 0, zpos);
    }
}
}