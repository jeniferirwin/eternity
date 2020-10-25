using UnityEngine;

namespace Eternity
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed;
        public Animator playerAnimator;
        public GameObject modelContainer;

        public bool isSafe;
        private Modifiers modifiers;
        private float xbounds = 67;
        private float zbounds = 70;

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
            
            Vector3 lookTarget = new Vector3(0,1,0);
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

            Vector3 lastPos = transform.position;
            transform.Translate(new Vector3(haxis, 0, vaxis) * moveSpeed * Time.deltaTime * (1 + modifiers.fragmentsGathered / 50));
            if (transform.position.x > xbounds || transform.position.x < -xbounds)
            {
                transform.position = new Vector3(lastPos.x, transform.position.y, transform.position.z);
            }
            if (transform.position.z > zbounds || transform.position.z < -zbounds)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, lastPos.z);
            }
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
            
            if (collider.gameObject.CompareTag("Water"))
            {
                isSafe = false;
            }
        }
    }
}