using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rimaethon.Runtime.DataPersistance;
using Rimaethon.Scripts.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : PersistentSingleton<DataPersistenceManager>
{
    [Header("Debugging")] [SerializeField] private bool disableDataPersistence;

    [SerializeField] private bool initializeDataIfNull;
    [SerializeField] private bool overrideSelectedProfileId;
    [SerializeField] private string testSelectedProfileId = "test";

    [Header("File Storage Config")] [SerializeField]
    private string fileName;

    [SerializeField] private bool useEncryption;
    [Header("Auto Saving Configuration")] [SerializeField]
    private float autoSaveTimeSeconds = 60f;

    private Coroutine autoSaveCoroutine;
    private FileDataHandler dataHandler;
    private List<IDataPersistence> dataPersistenceObjects;

    private GameSettingsData gameData;

    private string selectedProfileId = "";


    protected override void Awake()
    {
        base.Awake();

        if (disableDataPersistence)
            Debug.LogWarning("Data Persistence is currently disabled!");

        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        InitializeSelectedProfileId();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected override void OnApplicationQuit()
    {
        SaveGame();
        base.OnApplicationQuit();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
        if (autoSaveCoroutine != null)
            StopCoroutine(autoSaveCoroutine);
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        selectedProfileId = newProfileId;
        LoadGame();
    }

    public void DeleteProfileData(string profileId)
    {
        dataHandler.Delete(profileId);
        InitializeSelectedProfileId();
        LoadGame();
    }

    private void InitializeSelectedProfileId()
    {
       selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();
        if (overrideSelectedProfileId)
        {
            selectedProfileId = testSelectedProfileId;
        }
    }

    public void NewGame()
    {
        gameData = new GameSettingsData();
    }

    public void LoadGame()
    {
        if (disableDataPersistence)
            return;

        gameData = dataHandler.Load(selectedProfileId);

        if (gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        if (gameData == null)
        {
            return;
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // return right away if data persistence is disabled
        if (disableDataPersistence) return;

        // if we don't have any data to save, log a warning here
        if (gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        // pass the data to other scripts so they can update it
        foreach (var dataPersistenceObj in dataPersistenceObjects) dataPersistenceObj.SaveData(gameData);

        // timestamp the data so we know when it was last saved
        gameData.lastUpdated = DateTime.Now.ToBinary();

        // save that data to a file using the data handler
           dataHandler.Save(gameData, selectedProfileId);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameSettingsData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
            Debug.Log("Auto Saved Game");
        }
    }
}
