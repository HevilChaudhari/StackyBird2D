using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBirdSpawner : MonoBehaviour
{

     
    [SerializeField] private GameObject enemyBirdPrefab;
    private float enemyBirdSpeed = 200f;
    private bool isSpawing;
    private float spawningRate = .4f;
    private float nextSpawntime;
    [SerializeField] private Transform[] spawnPoints;


    private void OnEnable()
    {
        GameManager.OnGameStateChanged += StartSpawning;
        GameManager.OnGameStateChanged += StopSpawning;
        StopBirdSpawningEvent.OnPlayerCross += StopSpawningWhenPlayercCrossLine;
    }

    private void StopSpawningWhenPlayercCrossLine()
    {
        isSpawing = false;
    }

    private void StartSpawning(GameManager.GameState state)
    {
        if (state == GameManager.GameState.playing)
        {
            isSpawing = true;
        }
    }

    private void StopSpawning(GameManager.GameState state)
    {
        if (state == GameManager.GameState.restart)
        {
            isSpawing = false;
        }
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= StartSpawning;
        GameManager.OnGameStateChanged -= StopSpawning;
    }



    private void Update()
    {
        if (isSpawing && Time.time >= nextSpawntime)
        {
            SpawnBird();
            nextSpawntime = Time.time + 1f / spawningRate; // Calculate next allowed fire time
        }
    }




    private void SpawnBird()
    {

        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].localPosition;



        GameObject enemyBird = Instantiate(enemyBirdPrefab, spawnPoint, Quaternion.identity);

            enemyBird.GetComponent<Rigidbody2D>().velocity = Vector3.left * enemyBirdSpeed * Time.deltaTime;

        
    }
}
