using UnityEngine;
using Eternity;

namespace Eternity
{
    public class Water : MonoBehaviour
    {
        public int initTimer;
        public int timerStep;
        public float moveSpeed;
        public ObjectPool enemyPool;
        public ObjectPool digSitePool;
        public Player player;
        public int digSites = 4;

        private int currentTimerMax;
        private int currentTimerCountdown;
        private float frameTick = 1;
        private Vector3 startPosition;
        private Vector3 endPosition = new Vector3(315, 0, 0);
        private bool crashing;
        private Vector3 movement = new Vector3(1, 0, 0);

        public int TimerDisplay
        {
            get
            {
                return currentTimerCountdown;
            }
        }

        void Start()
        {
            startPosition = transform.position;
            currentTimerMax = initTimer;
            currentTimerCountdown = currentTimerMax;
        }

        void Update()
        {
            if (!crashing)
            {
                frameTick -= Time.deltaTime;
                if (frameTick <= 0)
                {
                    frameTick = 1;
                    currentTimerCountdown -= 1;
                    Debug.Log(currentTimerCountdown);
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
                return;
            }

            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }

        public void ResetSpawns()
        {
            int numEnemies = player.fragments * 2;
            digSites++;

            for (int i = 0; i < numEnemies; i++)
            {
                Vector3 spawnPos = EternityUtils.PickRandomLocation();
                GameObject enemy = enemyPool.GetRandomInactiveObject();
                enemy.transform.position = spawnPos;
                enemy.SetActive(true);
            }

            for (int i = 0; i < digSites; i++)
            {
                Vector3 spawnPos = EternityUtils.PickRandomLocation();
                GameObject digSite = digSitePool.GetRandomInactiveObject();
                digSite.transform.position = spawnPos;
                digSite.SetActive(true);
            }
        }
    }
}