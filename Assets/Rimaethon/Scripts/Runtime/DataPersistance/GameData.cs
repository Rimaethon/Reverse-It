using System;
using Rimaethon.Runtime.Checkpoint;
using UnityEngine;

[Serializable]
public class GameData
{
    public long lastUpdated;
    public int deathCount;
    public Vector3 playerPosition;
    public Checkpoint checkpoint;

    // the values defined in this constructor will be the default values
    // the game starts from beginning when there's no data to load
}
