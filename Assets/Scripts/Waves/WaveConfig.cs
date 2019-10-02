﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName="Enemy Wave Configuration")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawn = 2f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 10;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab(){return enemyPrefab;}

    public List<Transform> GetWaypoints(){

        var waveWaypoints = new List<Transform>();

        foreach(Transform child in pathPrefab.transform){
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }
    public float GetTimeBetweenSpawn(){return timeBetweenSpawn;}
    public float GetSpawnRandomFactor(){return spawnRandomFactor;}
    public int GetNumberOfEnemies(){return numberOfEnemies;}
    public float GetMoveSpeed(){return moveSpeed;}

}
