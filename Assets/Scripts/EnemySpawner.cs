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

    public List<Enemy> enemies = new List<Enemy>();
    float totalWeight;

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
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
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
}
