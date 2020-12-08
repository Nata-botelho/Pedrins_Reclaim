using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Enemy {
        public GameObject gameObject;
        public float weight;
    }

    [System.Serializable]
    public struct Bosses {
        public GameObject gameObject;
    }

    public List<Bosses> bosses = new List<Bosses>();
    public List<Enemy> enemies = new List<Enemy>();
    float totalWeight;

    public int totalEnemies = 3;
    public int HowManyWaves = 14;
    private int actualWave = 0;

    Vector2 whereToSpawn;
    public float spawnRate = 10f;
    float nextSpawn = 0f;

    void Awake() {
        totalWeight = 0;
        foreach(var enemy in enemies) {
            totalWeight += enemy.weight;
        }
    }

    void Start() {
        
    }

    void Update() {
        if (Time.time > nextSpawn && actualWave < HowManyWaves) {
            nextSpawn = Time.time + spawnRate;
            if (actualWave != 4 && actualWave != 9 && actualWave != 14) {
                for (int i = 0; i < totalEnemies; i++) {
                    SpawnEnemy();
                }
            } else {
                switch (actualWave)
                {
                    case(4):
                    SpawnBoss(0);
                    break;
                    case(9):
                    SpawnBoss(1);
                    break;
                    case(14):
                    SpawnBoss(2);
                    break;
                }
            }
            totalEnemies++;
            actualWave++;
        } else if (actualWave > HowManyWaves) {
            // Já foi a última wave
        }
    }

    private void SpawnEnemy() {
        float pick = Random.value*totalWeight;
        int choosenIndex = 0;
        float cumulativeWeight = enemies[0].weight;

        while (pick > cumulativeWeight && choosenIndex < enemies.Count -1 ) {
            choosenIndex++;
            cumulativeWeight += enemies[choosenIndex].weight;
        }

        whereToSpawn = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
        Instantiate(enemies[choosenIndex].gameObject, whereToSpawn, Quaternion.identity);
    }

    private void SpawnBoss(int bossToSpawn) {
        whereToSpawn = new Vector2(0, 0);

        Instantiate(bosses[bossToSpawn].gameObject, whereToSpawn, Quaternion.identity);
    }
}
