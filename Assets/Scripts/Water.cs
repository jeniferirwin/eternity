using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace Eternity
{
    public class Water : MonoBehaviour
    {
        public AudioManager audioManager;
        public AudioClip waveCrash;
        public Modifiers modifiers;
        public GameObject firstSafeZone;
        public SpawnManager spawns;
        public UI ui;
        public Player player;
        public float moveSpeed;
        public bool firstCrash = true;

        // TIME

        public int gamePreludeTimer;
        public int firstRoundTimer;
        public int currentTimerMax;
        public int currentTimerTicker;
        public int timerStep;
        private float frameTick = 1;

        private Vector3 startPosition;
        private Vector3 endPosition = new Vector3(170, 0, 0);
        private Vector3 movement = new Vector3(1, 0, 0);
        private bool zonesActive;
        private bool crashing;

        void Awake()
        {
            startPosition = transform.position;
            currentTimerMax = gamePreludeTimer;
            currentTimerTicker = currentTimerMax;
            firstCrash = true;
            zonesActive = true;
        }

        void Update()
        {

            if (modifiers.gameOver)
            {
                return;
            }

            if (!crashing)
            {
                frameTick -= Time.deltaTime;
                if (frameTick <= 0)
                {
                    frameTick = 1;
                    currentTimerTicker -= 1;
                    ui.UpdateTimer(currentTimerTicker);
                }

                if (currentTimerTicker < (currentTimerMax * 0.75) && !zonesActive)
                {
                    zonesActive = true;
                    spawns.SpawnSafeZones();
                    spawns.SpawnHealingSafeZones();
                }

                if (currentTimerTicker <= 0)
                {
                    crashing = true;
                    audioManager.PlayWave();
                }
                return;
            }

            if (crashing && transform.position.x >= Mathf.Abs(startPosition.x))
            {
                if (firstCrash)
                {
                    firstCrash = false;
                    currentTimerMax = firstRoundTimer;
                }
                else
                {
                    currentTimerMax += timerStep;
                }

                crashing = false;
                transform.position = startPosition;
                currentTimerTicker = currentTimerMax;
                zonesActive = false;
                player.isSafe = false;
                modifiers.IncrementRoundsCompleted();
                spawns.ResetAllSpawns();
                spawns.SpawnDigSites();
                spawns.SpawnEnemies();
                return;
            }

            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }
}