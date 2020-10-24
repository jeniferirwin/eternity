using UnityEngine;

namespace Eternity
{
    public class Enemy : MonoBehaviour
    {
        public float moveSpeed;

        private GameObject player;
        private bool touchingPlayer;
        private float pauseTimer;
        private Vector3 lastPos;
        private bool touchingSafeZone;

        void OnEnable()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            if (pauseTimer >= 0)
            {
                pauseTimer -= Time.deltaTime;
                return;
            }

            if (touchingPlayer)
            {
                return;
            }

            if (!player.activeSelf)
            {
                return;
            }

            Vector3 moveDir = (player.transform.position - transform.position).normalized;
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);
            if (touchingSafeZone)
            {
                transform.position = lastPos;
            }
            lastPos = transform.position;
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                touchingPlayer = true;
            }

            if (collider.gameObject.CompareTag("Water"))
            {
                gameObject.SetActive(false);
            }
            
            if (collider.gameObject.CompareTag("SafeZone") || collider.gameObject.CompareTag("HealingSafeZone"))
            {
                GameObject safeZone = collider.gameObject;
                SafeZone safeZoneStats = safeZone.GetComponent<SafeZone>();
                if (safeZoneStats.playerTouching)
                {
                    safeZoneStats.ExpendCharge();
                    Die();
                }
            }
        }


        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                touchingPlayer = false;
                pauseTimer = 1.5f;
            }

            if (collider.gameObject.CompareTag("SafeZone") || collider.gameObject.CompareTag("HealingSafeZone"))
            {
                touchingSafeZone = false;
            }
        }
    }
}