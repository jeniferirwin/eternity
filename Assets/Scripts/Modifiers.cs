using UnityEngine;

namespace Eternity
{
    public class Modifiers : MonoBehaviour
    {
        public UI ui;

        public int enemiesKilled = 0; // affects the player's digging and movement speed
        public int roundsCompleted = 0; // affects the number of digsites and safe zones that spawn
        public int fragmentsGathered = 0; // affects the number of enemies that spawn
        public int hitPoints = 3; // player health
        public bool GameOver = false;
        public float maxGracePeriod = 1.5f;
        public float gracePeriodTicker;
        public bool willHeal;
        
        private void Awake()
        {
            ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        }
        
        public void SetGameOver()
        {
            GameOver = true;
        }

        public void StartGracePeriod()
        {
            gracePeriodTicker = maxGracePeriod;
        }

        public void Update()
        {
            if (gracePeriodTicker > 0)
                gracePeriodTicker -= Time.deltaTime;
        }
        
        public void IncrementEnemiesKilled()
        {
            enemiesKilled++;
            ui.UpdateEnemiesKilled(enemiesKilled);
        }
        
        public void IncrementFragmentsGathered()
        {
            fragmentsGathered++;
            ui.UpdateFragments(fragmentsGathered);
        }
        
        public void IncrementRoundsCompleted()
        {
            roundsCompleted++;
        }
        
        public void IncrementHitPoints()
        {
            hitPoints++;
            if (hitPoints > 3)
            {
                hitPoints = 3;
            }
            willHeal = false;
            ui.UpdateHitPoints(hitPoints);
        }
        
        public void DecrementHitPoints()
        {
            hitPoints--;
            if (hitPoints < 0)
            {
                hitPoints = 0;
            }
            ui.UpdateHitPoints(hitPoints);
            StartGracePeriod();
        }

        public float DigSpeedMulitplier
        {
            get
            {
                return Mathf.Min(6, 1 + ((float)enemiesKilled * 0.05f));
            }
        }

        public float MoveSpeedMultiplier
        {
            get
            {
                return (Mathf.Min(5, 1 + ((float)enemiesKilled * 0.05f)));
            }
        }

        public int NumDigSites
        {
            get
            {
                return (10 + (roundsCompleted * 2));
            }
        }

        public int NumSafeZones
        {
            get
            {
                return (5 + (roundsCompleted));
            }
        }

        public int NumHealingSafeZones
        {
            get
            {
                return (2 + (roundsCompleted));
            }
        }

        public int NumEnemies
        {
            get
            {
                return (1 + (fragmentsGathered * 2));
            }
        }
    }
}