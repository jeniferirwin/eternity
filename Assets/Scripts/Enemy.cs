using UnityEngine;

namespace Eternity
{
    public class Enemy : MonoBehaviour
    {
        public float maxSpeed;
        public int pauseTimer;
        public Modifiers modifiers;
        
        private float moveSpeed;

        private GameObject player;
        private float currentPauseTimer;

        private void Awake()
        {
            modifiers = GameObject.FindGameObjectWithTag("Modifiers").GetComponent<Modifiers>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void OnEnable()
        {
            if (modifiers.GameOver)
            {
                gameObject.SetActive(false);
            }

            currentPauseTimer = pauseTimer;
            moveSpeed = Random.Range(maxSpeed / 2, maxSpeed);
        }

        void Update()
        {
            if (modifiers.GameOver)
            {
                return;
            }

            if (currentPauseTimer >= 0)
            {
                currentPauseTimer -= Time.deltaTime;
                return;
            }

            Vector3 moveDir = (player.transform.position - transform.position).normalized;
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        }

        public void DieWater()
        {
            gameObject.SetActive(false);
        }
        
        public void DieSafeZone()
        {
            modifiers.IncrementEnemiesKilled();
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject == player && currentPauseTimer <= 0 && modifiers.gracePeriodTicker <= 0)
            {
                modifiers.DecrementHitPoints();
                currentPauseTimer = pauseTimer;
            }

            if (collider.gameObject.CompareTag("Water"))
            {
                DieWater();
            }
        }
        
        private void OnTriggerStay(Collider collider)
        {
            if (collider.gameObject == player && currentPauseTimer <= 0 && modifiers.gracePeriodTicker <= 0)
            {
                modifiers.DecrementHitPoints();
                currentPauseTimer = pauseTimer;
            }
        }
    }
}