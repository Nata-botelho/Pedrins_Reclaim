using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable {
        public GameObject gameObject;
        public float weight;
    }

    public List<Spawnable> items = new List<Spawnable>();
    float totalWeight;

    void Awake() {
        totalWeight = 0;
        foreach(var spawnable in items) {
            totalWeight += spawnable.weight;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        float pick = Random.value*totalWeight;
        int choosenIndex = 0;
        float cumulativeWeight = items[0].weight;

        while (pick > cumulativeWeight && choosenIndex < items.Count -1 ) {
            choosenIndex++;
            cumulativeWeight += items[choosenIndex].weight;
        }

        GameObject i = Instantiate(items[choosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
