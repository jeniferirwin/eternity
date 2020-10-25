using UnityEngine;

namespace Eternity
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource musicSource;
        public AudioSource sfxSource;
        public AudioSource diggingSfxSource;
        
        public AudioClip diggingClip;
        public AudioClip collectingClip;
        public AudioClip hitClip;
        public AudioClip waveClip;
        public AudioClip zapClip;
        public AudioClip musicClip;
        
        public void PlayDigging()
        {
            if (!diggingSfxSource.isPlaying)
                diggingSfxSource.PlayOneShot(diggingClip);
        }

        public void PlayWave()
        {
            sfxSource.PlayOneShot(waveClip);
        }

        public void PlayHit()
        {
            sfxSource.PlayOneShot(hitClip);
        }

        public void PlayZap()
        {
            sfxSource.PlayOneShot(zapClip);
        }

        public void PlayCollect()
        {
            sfxSource.PlayOneShot(collectingClip);
        }

        public void PlayMusic()
        {
            musicSource.Play();
        }
        
        public void StopMusic()
        {
            musicSource.Stop();
        }
    }
}