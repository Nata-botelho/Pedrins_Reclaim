using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DeathDelay() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Enemy") {
            col.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }
    }
}
