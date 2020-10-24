using UnityEngine;

namespace Eternity
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed;

        private bool isSafe;
        private Modifiers modifiers;

        void Awake()
        {
            modifiers = GameObject.FindGameObjectWithTag("Modifiers").GetComponent<Modifiers>();
        }

        void Update()
        {
            if (modifiers.hitPoints <= 0)
            {
                Die();
            }

            ProcessMovement();
        }

        private void ProcessMovement()
        {
            float haxis = Input.GetAxis("Horizontal");
            float vaxis = Input.GetAxis("Vertical");

            transform.Translate(new Vector3(haxis, 0, vaxis) * moveSpeed * Time.deltaTime * (1 + modifiers.fragmentsGathered / 50));
        }

        private void Die()
        {
            modifiers.SetGameOver();
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("SafeZone") || collider.gameObject.CompareTag("HealingSafeZone"))
            {
                isSafe = true;
            }

            if (collider.gameObject.CompareTag("Water") && !isSafe)
            {
                Die();
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("SafeZone") || collider.gameObject.CompareTag("HealingSafeZone"))
            {
                isSafe = false;
            }

            if (collider.gameObject.CompareTag("SafeZone") || collider.gameObject.CompareTag("HealingSafeZone"))
            {
                isSafe = false;
            }
        }
    }
}