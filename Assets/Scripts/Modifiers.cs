using UnityEngine;

namespace Eternity
{
    public class Modifiers : MonoBehaviour
    {
        public UI ui;
        public GameObject hud;
        public GameObject gameOverScreen;
        public GameObject winScreen;

        public AudioManager audioManager;

        public int enemiesKilled = 0; // affects the player's digging and movement speed
        public int roundsCompleted = 0; // affects the number of digsites and safe zones that spawn
        public int fragmentsGathered = 0; // affects the number of enemies that spawn
        public int hitPoints = 3; // player health
        public bool gameOver = false;
        public bool gameWon = false;
        public float maxGracePeriod = 1.5f;
        public float gracePeriodTicker;
        public bool willHeal;
        
        public void SetGameOver()
        {
            gameOver = true;
            hud.SetActive(false);
            gameOverScreen.SetActive(true);
        }
        
        public void SetWin()
        {
            hud.SetActive(false);
            winScreen.SetActive(true);
            gameWon = true;
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
            audioManager.PlayZap();
            ui.UpdateEnemiesKilled(enemiesKilled);
        }
        
        public void IncrementFragmentsGathered()
        {
            fragmentsGathered++;
            audioManager.PlayCollect();
            ui.UpdateFragments(fragmentsGathered);
            if (fragmentsGathered >= 50)
            {
                Time.timeScale = 0;
                SetWin();
            }
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
            audioManager.PlayHit();
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
                return (3 + (roundsCompleted));
            }
        }

        public int NumHealingSafeZones
        {
            get
            {
                return (1 + (roundsCompleted));
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