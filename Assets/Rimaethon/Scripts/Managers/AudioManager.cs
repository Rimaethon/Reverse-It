using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Utility;
using UnityEngine;

namespace Rimaethon.Scripts.Managers
{ 
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        public AudioSource musicSource;
        public AudioSource sfxSource;
        public AudioClip[] musicClips;  
        public AudioClip[] sfxClips;
        public bool musicOn;
        public bool sfxOn;
        
        private void Start()
        {
            EventManager.Instance.AddHandler(GameEvents.OnPlayingAudio, Play);
            PlayMusic(0);
        }
        public void PlayMusic(int trackNumber)
        {
            if (!musicOn) return;
            if (musicSource.isPlaying) musicSource.Stop();
            musicSource.clip = musicClips[trackNumber];
            musicSource.Play();
        }
        public void PlaySFX(int clip)
        {
            if (!sfxOn) return;
            sfxSource.PlayOneShot(sfxClips[clip]);
        }

        public void Play()
        {
            if (!musicOn) return;
            if (musicSource.isPlaying) musicSource.Stop();
            musicSource.Play();
        }
    }
}
