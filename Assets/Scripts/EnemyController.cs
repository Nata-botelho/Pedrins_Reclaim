using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    Wander,
    Follow,
    Die,
    Attack
};

public class EnemyController : MonoBehaviour
{

    GameObject player;
    public EnemyState currState = EnemyState.Wander;
    public float range;
    public float speed;
    public float attackRange;
    private bool chooseDir = false;
    private bool coolDownAttack = false;
    public float coolDown;
    private Vector3 randomDir;

   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case(EnemyState.Wander):
                Wander();
            break;
            case(EnemyState.Follow):
                Follow();
            break;
            case(EnemyState.Die):
            break;
            case(EnemyState.Attack):
                Attack();
            break;
        }

        if (isPlayerInRange(range) && currState != EnemyState.Die) {
            currState = EnemyState.Follow;
        } else if (!isPlayerInRange(range) && currState != EnemyState.Die) {
            currState = EnemyState.Wander;
        }
        if (isPlayerInRange(attackRange)) {
            currState = EnemyState.Attack;
        } 
    }

    private bool isPlayerInRange(float range) {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection() {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, 0);
        chooseDir = false;
    }

    void Wander() {
        if (!chooseDir) {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;
        if (isPlayerInRange(range)) {
            currState = EnemyState.Follow;
        }

    }

    void Follow() {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack() {
        if (!coolDownAttack) {
            GameController.DamagePlayer(10);
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator CoolDown() {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    public void Death() {
        Destroy(gameObject);
    }
}
