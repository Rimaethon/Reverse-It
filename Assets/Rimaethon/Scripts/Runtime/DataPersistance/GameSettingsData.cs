using System;
using UnityEngine;

namespace Rimaethon.Runtime.DataPersistance
{
    [Serializable]
    public class GameSettingsData
    {
        public int ScreenWidth;
        public int ScreenHeight;
        public bool FullScreen;
        public float MasterVolume;
        public float MusicVolume;
        public float SFXVolume;
        public int FrameRate;
        public long lastUpdated;


        public GameSettingsData()
        {
            ScreenWidth = Screen.width;
            ScreenHeight = Screen.height;
            FullScreen = true;
            MasterVolume = 0.5f;
            MusicVolume = 0.5f;
            SFXVolume = 0.5f;
            FrameRate = -1;
            lastUpdated = DateTime.Now.Ticks;
        }
    }
}