using System;
using Rimaethon.Runtime.DataPersistance;
using Rimaethon.Scripts.Utility;
using UnityEngine;
using UnityEngine.Audio;
using Application = UnityEngine.Device.Application;
using Screen = UnityEngine.Device.Screen;

namespace Rimaethon.Runtime.Managers
{
    public class SettingsManager : PersistentSingleton<GameManager>, IDataPersistence
    {
        [SerializeField] private AudioMixer audioMixer;

        public void LoadData(GameSettingsData data)
        {
            Screen.fullScreen = data.FullScreen;
            Screen.SetResolution(data.ScreenWidth, data.ScreenHeight, data.FullScreen);
            Application.targetFrameRate = data.FrameRate;
        }

        public void SaveData(GameSettingsData data)
        {
        }
    }
}
