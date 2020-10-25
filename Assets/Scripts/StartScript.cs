using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eternity
{
    public class StartScript : MonoBehaviour
    {
        public GameObject titleScreen;
        public GameObject hudOverlay;
        public AudioManager audioManager;

        void Start()
        {
            Time.timeScale = 0;
        }
        
        public void BeginGame()
        {
            titleScreen.SetActive(false);
            hudOverlay.SetActive(true);
            audioManager.PlayMusic();
            Time.timeScale = 1;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("main");
        }
    }
}