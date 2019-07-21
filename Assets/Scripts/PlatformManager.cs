﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformManager : MonoBehaviour
{
    public Tilemap tileMap;
    public RuleTile platformTile;
    public Transform deletionPoint;
    public float distanceBetween;
    public float minStartSpawnTime = 1;
    public float maxStartSpawnTime = 3;
    public int minGaps = 5;
    public int maxGaps = 8;

    private float elapsedSpawnWaitTime;
    private float spawnWaitTime;
    private int numOfPlatformTiles = 0;
    private const int platformWidth = 1;

    void Start()
    {
        spawnWaitTime = Random.Range(minStartSpawnTime, maxStartSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedSpawnWaitTime > spawnWaitTime)
        {
            spawnWaitTime = Random.Range(minStartSpawnTime, maxStartSpawnTime);
            elapsedSpawnWaitTime = 0f;
            numOfPlatformTiles = Random.Range(minGaps, maxGaps);
        }

        Vector3 translation = new Vector3(distanceBetween + platformWidth, 0f);
        transform.position += translation;

        if (numOfPlatformTiles > 0)
        {
            Vector3Int currentCell = tileMap.WorldToCell(transform.position);
            tileMap.SetTile(currentCell, platformTile);
            currentCell = tileMap.WorldToCell(transform.position - new Vector3(1f, 0f));
            tileMap.SetTile(currentCell, platformTile);
            numOfPlatformTiles--;
        }
        /*
        //Delete any ground tiles that have exited the screen.
        Vector3Int deletionCell = tileMap.WorldToCell(deletionPoint.position);
        tileMap.SetTile(deletionCell, null);
        */
        elapsedSpawnWaitTime += Time.deltaTime;
    }
}