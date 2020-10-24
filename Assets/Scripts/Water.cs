using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace Eternity
{
    public class Water : MonoBehaviour
    {
        public Modifiers modifiers;
        public int initTimer;
        public int timerStep;
        public float moveSpeed;
        public ObjectPool enemyPool;
        public ObjectPool digSitePool;
        public ObjectPool healingSafeZonePool;
        public ObjectPool safeZonePool;
        public Player player;
        public UI ui;

        private int currentTimerMax;
        private int currentTimerCountdown;
        private float frameTick = 1;
        private Vector3 startPosition;
        private Vector3 endPosition = new Vector3(170, 0, 0);
        private bool crashing;
        private Vector3 movement = new Vector3(1, 0, 0);
        private bool zonesActive;
        private bool firstRound;

        public int TimerDisplay
        {
            get
            {
                return currentTimerCountdown;
            }
        }

        void Start()
        {
            firstRound = true;
            modifiers = GameObject.FindGameObjectWithTag("Modifiers").GetComponent<Modifiers>();
            ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
            ResetSafeZones();
            GetAllSafeZones()[0].transform.position = new Vector3(0,0,0);  // DIS-GUSS-TAAANG
            ActivateSafeZones();
            startPosition = transform.position;
            currentTimerMax = initTimer;
            currentTimerCountdown = currentTimerMax;
            ResetSpawns();
        }

        void Update()
        {
            if (modifiers.GameOver)
            {
                return;
            }
            if (firstRound)
            {
                crashing = true;
                firstRound = false;
                return;
            }

            if (!crashing)
            {
                frameTick -= Time.deltaTime;
                if (frameTick <= 0)
                {
                    frameTick = 1;
                    currentTimerCountdown -= 1;
                    ui.UpdateTimer(currentTimerCountdown);
                }
                
                if (currentTimerCountdown < (currentTimerMax * 0.75) && !zonesActive)
                {
                    ActivateSafeZones();
                }

                if (currentTimerCountdown <= 0)
                {
                    crashing = true;
                }
                return;
            }

            if (crashing && transform.position.x >= Mathf.Abs(startPosition.x))
            {
                crashing = false;
                transform.position = startPosition;
                currentTimerMax += timerStep;
                currentTimerCountdown = currentTimerMax;
                ResetSpawns();
                ResetSafeZones();
                modifiers.IncrementRoundsCompleted();
                return;
            }

            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }

        public void ResetSpawns()
        {
            for (int i = 0; i < modifiers.NumEnemies; i++)
            {
                Vector3 spawnPos = EternityUtils.PickRandomLocation();
                GameObject enemy = enemyPool.GetRandomInactiveObject();
                enemy.transform.position = spawnPos;
                enemy.SetActive(true);
            }

            for (int i = 0; i < modifiers.NumDigSites; i++)
            {
                Vector3 spawnPos = EternityUtils.PickRandomLocation();
                GameObject digSite = digSitePool.GetRandomInactiveObject();
                digSite.transform.position = spawnPos;
                digSite.SetActive(true);
            }
        }
        
        public void ResetSafeZones()
        {
            List<GameObject> safeZones = GetAllSafeZones();
            foreach (GameObject safeZone in safeZones)
            {
                Vector3 newPos = EternityUtils.PickRandomLocation();
                safeZone.SetActive(false);
                safeZone.transform.position = newPos;
            }
            zonesActive = false;
            Debug.Log("Safe zones reset.");
        }
        
        public void ActivateSafeZones()
        {
            for (int i = 0; i < modifiers.NumSafeZones; i++)
            {
                GameObject safeZone = safeZonePool.GetRandomInactiveObject();
                safeZone.SetActive(true);
            }
            for (int i = 0; i < modifiers.NumHealingSafeZones; i++)
            {
                GameObject healingSafeZone = healingSafeZonePool.GetRandomInactiveObject();
                healingSafeZone.SetActive(true);
            }
            zonesActive = true;
            Debug.Log("Safe zones activated.");
        }
        
        public List<GameObject> GetAllSafeZones()
        {
            List<GameObject> healingSafeZones = healingSafeZonePool.GetAllInactiveObjects();
            healingSafeZones.AddRange(healingSafeZonePool.GetAllActiveObjects());

            List<GameObject> safeZones = safeZonePool.GetAllInactiveObjects();
            safeZones.AddRange(safeZonePool.GetAllActiveObjects());
            
            List<GameObject> allSafeZones = new List<GameObject>();
            allSafeZones.AddRange(safeZones);
            allSafeZones.AddRange(healingSafeZones);
            
            return allSafeZones;
        }
    }
}