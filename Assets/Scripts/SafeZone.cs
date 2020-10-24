using UnityEngine;

namespace Eternity
{
    public class SafeZone : MonoBehaviour
    {
        public Light lightStats;
        public Player playerStats;
        public float flashSpeed;
        public int charges;
        public bool playerTouching;
        public Modifiers modifiers;


        private bool vanishing;
        private int currentCharges;
        private bool dimming;
        private float maxIntensity;

        private void Start()
        {
            maxIntensity = lightStats.intensity;
            vanishing = false;
        }

        private void OnEnable()
        {
            modifiers = GameObject.FindGameObjectWithTag("Modifiers").GetComponent<Modifiers>();
            playerTouching = false;
            currentCharges = charges;
        }

        public void ExpendCharge()
        {
            currentCharges--;
            if (currentCharges == 0)
            {
                vanishing = true;
                if (gameObject.CompareTag("HealingSafeZone"))
                {
                    modifiers.IncrementHitPoints();
                }
            }
        }

        void Update()
        {
            if (vanishing)
            {
                transform.Translate(Vector3.up * 10);
                if (transform.position.y > 30)
                {
                    vanishing = false;
                    gameObject.SetActive(false);
                }
            }
            if (dimming)
            {
                lightStats.intensity -= Time.deltaTime * flashSpeed;
                if (lightStats.intensity <= 0)
                {
                    dimming = false;
                }
            }
            else
            {
                lightStats.intensity += Time.deltaTime * flashSpeed;
                if (lightStats.intensity >= maxIntensity)
                {
                    dimming = true;
                }
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                playerTouching = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                playerTouching = false;
            }
            
            if (collider.gameObject.CompareTag("Water"))
            {
                vanishing = true;
            }
        }
    }
}