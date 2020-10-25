using UnityEngine;

namespace Eternity
{
    public class SafeZoneBox : MonoBehaviour
    {
        public bool isHealing;
        public int maxCharges;

        private int currentCharges;
        private bool playerTouching;
        private Modifiers modifiers;

        private void Awake()
        {
            currentCharges = maxCharges;
            modifiers = GameObject.FindGameObjectWithTag("Modifiers").GetComponent<Modifiers>();
        }

        private void OnEnable()
        {
            currentCharges = maxCharges;
        }
        
        private void Update()
        {
            if (transform.parent.position.y > 10)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
            if (currentCharges <= 0)
            {
                if (isHealing && playerTouching)
                {
                    modifiers.IncrementHitPoints();
                }
                transform.parent.Translate(Vector3.up * 50);
            }
        }

        private void KillEnemy(GameObject enemy)
        {
            Enemy enemyInfo = enemy.GetComponent<Enemy>();
            enemyInfo.DieSafeZone();
            currentCharges--;
        }

        private void OnTriggerStay(Collider collider)
        {
            if (!playerTouching && collider.gameObject.CompareTag("Player"))
            {
                playerTouching = true;
            }

            if (collider.gameObject.CompareTag("Enemy") && playerTouching && currentCharges > 0)
            {
                KillEnemy(collider.gameObject);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!playerTouching && collider.gameObject.CompareTag("Player"))
            {
                playerTouching = true;
            }

            if (collider.gameObject.CompareTag("Enemy") && playerTouching && currentCharges > 0)
            {
                KillEnemy(collider.gameObject);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Water"))
            {
                currentCharges = 0;
            }

            if (collider.gameObject.CompareTag("Player"))
            {
                playerTouching = false;
            }
        }
    }
}