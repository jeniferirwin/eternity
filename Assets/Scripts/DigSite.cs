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
            progress = 10;
        }

        // Update is called once per frame
        void Update()
        {
            float distance = Vector3.Distance(player.gameObject.transform.position, transform.position);
            if (Input.GetKey(KeyCode.E) && distance < 1f)
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
    }
}