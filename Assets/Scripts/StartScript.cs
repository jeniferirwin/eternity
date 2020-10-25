using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eternity
{
    public class StartScript : MonoBehaviour
    {
        public GameObject titleScreen;
        public GameObject hudOverlay;
        public AudioManager audioManager;
        public GameObject tutorial;

        void Start()
        {
            Time.timeScale = 0;
        }
        
        public void BeginGame(bool showTutorial)
        {
            titleScreen.SetActive(false);
            hudOverlay.SetActive(true);
            audioManager.PlayMusic();
            Time.timeScale = 1;
            if (showTutorial)
            {
                tutorial.SetActive(true);
            }
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene("main");
        }
    }
}