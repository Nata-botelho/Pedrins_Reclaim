using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float lifeTime;
    public bool isEnemyBullet = false;
    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
        if (!isEnemyBullet) {
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
        }
    }

    void Update() {
        if (isEnemyBullet) {
            curPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f*Time.deltaTime);
            // transform.rotation = Quaternion.Lerp(transform.rotation, , 0);
            if (curPos == lastPos) {
                Destroy(gameObject);
            }
            lastPos = curPos;
        }
    }

    public void GetPlayer(Transform player) {

        // Vector3 randomDir = new Vector3(0, 0, Random.Range(0, 360));
        // Quaternion nextRotation = Quaternion.Euler(randomDir);
        // Vector3 dir = player.transform.position - transform.position;
        // dir = player.transform.InverseTransformDirection(dir);
        // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // transform.eulerAngles.z = angle;
        

        
        playerPos = player.position;

    }

    IEnumerator DeathDelay() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Enemy" && !isEnemyBullet) {
            col.GetComponentInChildren<HealthBarController>().hp -= 25;
            if (col.GetComponentInChildren<HealthBarController>().hp < 1) col.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }
        if (col.tag == "Player" && isEnemyBullet) {
            GameController.DamagePlayer(5);
            Destroy(gameObject);
        }
    }
}
