using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager2 : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;
    private float zSpawnPosition = 38;
    private float xSpawnRange = 18;
    private float startDelay = 0;
    private float spawnInterval = 4f;
    public int amountOfEnemiesInWave = 10;

    private GameManager2 gameManagerScript;
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager2>();
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval);
    }

    void SpawnRandomEnemy()
    {
            int enemyIndex = Random.Range(0, enemiesPrefabs.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), 0, zSpawnPosition);
            Instantiate(enemiesPrefabs[enemyIndex], spawnPosition, enemiesPrefabs[enemyIndex].transform.rotation);
        amountOfEnemiesInWave--;
        if (amountOfEnemiesInWave == 0)
        {
            CancelInvoke("SpawnRandomEnemy");

        }
    }
}
