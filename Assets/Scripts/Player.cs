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

        void Start()
        {
            if (hitPoints == 0)
            {
                hitPoints = 3;
            }
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

            transform.Translate(new Vector3(haxis, 0, vaxis) * moveSpeed * Time.deltaTime);
        }

        private void GetHit()
        {
            hitPoints -= 1;
            if (hitPoints == 0)
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
            Debug.Log("Dying.");
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
                if (willHeal && hitPoints < 3)
                {
                    Debug.Log("Healing.");
                    hitPoints++;
                    isSafe = false;
                    willHeal = false;
                }
            }
        }
    }
}