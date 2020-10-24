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


        private bool vanishing;
        private bool dimming;
        private float maxIntensity;

        private void Start()
        {
            maxIntensity = lightStats.intensity;
            vanishing = false;
        }

        private void OnEnable()
        {
            playerTouching = false;
        }

        public void ExpendCharge()
        {
            charges--;
            if (charges <= 0)
            {
                vanishing = true;
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
                Debug.Log("Player is touching a safe zone.");
                playerTouching = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player stopped touching a safe zone.");
                playerTouching = false;
            }
            
            if (collider.gameObject.CompareTag("Water"))
            {
                vanishing = true;
                Debug.Log("Safe zone is going away.");
            }
        }
    }
}