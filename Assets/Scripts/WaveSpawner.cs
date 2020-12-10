using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public Transform enemy;
        public int count;
        public float rate;
    }

    [System.Serializable]
    public class Enemy {
        public GameObject gameObject;
        public float weight;
    }

    public class Boss {
        
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

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
            Debug.Log("All waves completed, Looping");
        } else {
            nextWave++;
        }
    }

    bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f) {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("Boss") == null) {
                return false;
            }    
        }
        return false;
    }

    IEnumerator SpawnWave(Wave _wave) {
        Debug.Log("Spawning a new wave");
        state = SpawnState.SPAWNING;
        
        // Spawn
        for (int i = 0; i < _wave.count; i++) {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy) {
        // Spawn enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);
        if (spawnPoints.Length == 0) {
            Debug.LogError("No spawn points referenced");
        }
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
