using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://www.youtube.com/watch?v=Vrld13ypX_I&ab_channel=Brackeys => Fazendo Spawn de Waves (Parte 1 e 2)

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {
        SPAWNING,
        WAITING,
        COUNTING
    };

    [System.Serializable]
    public class Wave {
        public string name;
        // public Transform enemy;
        public Enemy[] enemies;
        public int count;
        public float rate;
    }

    [System.Serializable]
    public class Enemy {
        public GameObject gameObject;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public float searchCountdown = 1f;
    public GameObject LevelLoader;

    public SpawnState state = SpawnState.COUNTING;
    void Start() {
        waveCountdown = timeBetweenWaves;    
    }

    void Update() {
        if (state == SpawnState.WAITING) {
            // Check if enemies are still alive
            if (!EnemyIsAlive()) {
                // Begin a new round
                WaveCompleted();
            } else {
                return;
            }
        }

        if (waveCountdown <= 0) {
            if (state != SpawnState.SPAWNING) {
                // Start Spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        } else {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted() {

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave+1 > waves.Length-1) {
            // Load Next Level ?
            nextWave = 0;
            LevelLoader.GetComponent<LevelLoader>().LoadWin();

            Debug.Log("All waves completed, Looping");
        } else {
            nextWave++;
        }
    }

    bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f) {
            searchCountdown = 1f;
            int numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            int numberOfBosses = GameObject.FindGameObjectsWithTag("Boss").Length;
            if (numberOfEnemies == 0 & numberOfBosses == 0) {
                return false;
            }    
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        Debug.Log("Spawning a new wave");
        state = SpawnState.SPAWNING;
        
        // Spawn
        for (int i = 0; i < _wave.count; i++) {
            SpawnEnemy(_wave.enemies);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Enemy[] enemies) {
        // Spawn enemy
        int pos = Random.Range(0, enemies.Length-1);
        Debug.Log("Spawning Enemy...");
        if (spawnPoints.Length == 0) {
            Debug.LogError("No spawn points referenced");
        }
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemies[pos].gameObject, _sp.position, _sp.rotation);
    }
}
