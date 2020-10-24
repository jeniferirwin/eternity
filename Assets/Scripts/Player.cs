using UnityEngine;

namespace Eternity
{
    public class Player : MonoBehaviour
    {
        public int hitPoints;
        public int hitGracePeriod;
        public float moveSpeed;
        public int fragments;
        public bool willHeal;

        private bool isSafe;
        private bool isGrace;
        private float gracePeriodTicker;
        private Modifiers modifiers;

        void Start()
        {
            modifiers = GameObject.FindGameObjectWithTag("Modifiers").GetComponent<Modifiers>();
        }

        void Update()
        {
            if (gracePeriodTicker >= 0f)
            {
                gracePeriodTicker -= Time.deltaTime;
            }

            if (gracePeriodTicker <= 0f && isGrace)
            {
                isGrace = false;
            }

            ProcessMovement();
        }

        private void ProcessMovement()
        {
            float haxis = Input.GetAxis("Horizontal");
            float vaxis = Input.GetAxis("Vertical");

            transform.Translate(new Vector3(haxis, 0, vaxis) * moveSpeed * Time.deltaTime * (1 + fragments / 50));
        }

        private void GetHit()
        {
            modifiers.DecrementHitPoints();
            if (modifiers.hitPoints <= 0)
            {
                Die();
            }
            else
            {
                gracePeriodTicker = hitGracePeriod;
                isGrace = true;
            }
        }

        private void Die()
        {
            modifiers.SetGameOver();
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("SafeZone"))
            {
                isSafe = true;
                willHeal = false;
            }

            if (collider.gameObject.CompareTag("HealingSafeZone"))
            {
                isSafe = true;
                willHeal = true;
            }

            if (collider.gameObject.CompareTag("Enemy") && !isGrace)
            {
                GetHit();
            }

            if (collider.gameObject.CompareTag("Water") && !isSafe)
            {
                Die();
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("SafeZone"))
            {
                isSafe = false;
                willHeal = false;
            }

            if (collider.gameObject.CompareTag("SafeZone"))
            {
                isSafe = false;
                willHeal = false;
            }
            
            if (collider.gameObject.CompareTag("Water"))
            {
                if (willHeal && modifiers.hitPoints < 3)
                {
                    modifiers.IncrementHitPoints();
                    isSafe = false;
                    willHeal = false;
                }
            }
        }
    }
}