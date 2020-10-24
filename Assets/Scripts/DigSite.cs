using UnityEngine;

namespace Eternity
{
    public class DigSite : MonoBehaviour
    {
        public Modifiers modifiers;
        public float speedMult;
        public Vector3 originalScale;
        
        private GameObject player;

        private float progress;

        private void OnEnable()
        {
            modifiers = GameObject.FindGameObjectWithTag("Modifiers").GetComponent<Modifiers>();
            if (modifiers.GameOver)
            {
                gameObject.SetActive(false);
            }
            player = GameObject.FindGameObjectWithTag("Player");
            progress = 10f;
            originalScale = new Vector3(3,1,3);
            transform.localScale = originalScale;
        }

        private void Update()
        {
            if (modifiers.GameOver)
            {
                return;
            }

            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < 1.5f)
            {
                float chunk = Time.deltaTime * speedMult * modifiers.DigSpeedMulitplier;
                progress -= chunk;
                float scalechunk = chunk / 10;
                if (transform.localScale.y > 0.1)
                {
                    transform.localScale -= originalScale * scalechunk;
                }
                if (progress <= 0)
                {
                    modifiers.IncrementFragmentsGathered();
                    gameObject.SetActive(false);
                }
            }
        }
        
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Water"))
            {
                progress = 10;
                gameObject.SetActive(false);
            }
        }
    }
}