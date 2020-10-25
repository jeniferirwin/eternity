using UnityEngine;

namespace Eternity
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed;
        public Animator playerAnimator;
        public GameObject modelContainer;

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
            if (haxis != 0 || vaxis != 0)
            {
                playerAnimator.SetBool("isWalking", true);
            }
            else
            {
                playerAnimator.SetBool("isWalking", false);
            }
            
            Vector3 lookTarget = new Vector3(0,0,0);
            if (haxis > 0)
            {
                lookTarget.x = 1;
            }
            else if (haxis < 0)
            {
                lookTarget.x = -1;
            }

            if (vaxis > 0)
            {
                lookTarget.z = 1;
            }
            else if (vaxis < 0)
            {
                lookTarget.z = -1;
            }


            transform.Translate(new Vector3(haxis, 0, vaxis) * moveSpeed * Time.deltaTime * (1 + modifiers.fragmentsGathered / 50));
            modelContainer.transform.LookAt(transform.position + lookTarget);
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