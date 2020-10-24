using UnityEngine;

namespace Eternity
{
    public class DigSite : MonoBehaviour
    {
        public float speedMult;

        private GameObject player;
        private Player playerStats;
        private float progress;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerStats = player.GetComponent<Player>();
        }

        private void OnEnable()
        {
            progress = 10;
        }

        private void Update()
        {
            float distance = Vector3.Distance(player.gameObject.transform.position, transform.position);
            if (distance < 1f)
            {
                progress -= Time.deltaTime * speedMult;
                Debug.Log("Progress: " + progress);
                if (progress <= 0)
                {
                    playerStats.fragments++;
                    Debug.Log("Fragment collected. Fragments: " + playerStats.fragments);
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