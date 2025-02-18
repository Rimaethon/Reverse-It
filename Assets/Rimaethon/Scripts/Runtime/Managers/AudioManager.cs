using Rimaethon.Runtime.Core;
using Rimaethon.Scripts.Utility;
using UnityEngine;

namespace Rimaethon.Scripts.Managers
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [SerializeField] private AudioLibrary audioLibrary;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        private int _currentMusicIndex;
        private int _currentSFXIndex;
        private AudioClip[] _musicClips;
        private AudioClip[] _sfxClips;
        private bool musicOn=true;
        private bool sfxOn=true;

        protected override void Awake()
        {
            base.Awake();
            _musicClips = audioLibrary.MusicClips;
            _sfxClips = audioLibrary.SFXClips;
        }

        public void PlayMusic(MusicClips clipEnum)
        {
            if (!musicOn) return;
            if (musicSource.isPlaying) musicSource.Stop();
            _currentMusicIndex = (int)clipEnum;
            musicSource.clip = _musicClips[_currentMusicIndex];
            musicSource.Play();
        }

        public void PlaySFX(SFXClips clipEnum)
        {
            if (!sfxOn) return;
            _currentSFXIndex = (int)clipEnum;
            sfxSource.PlayOneShot(_sfxClips[_currentSFXIndex]);
        }
    }
}
