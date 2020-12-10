using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    Wander,
    Follow,
    Die,
    Attack
};

public enum EnemyType {
    Melee,
    Ranged
};

public enum AttackType {
    Guided,
    Normal
}

public class EnemyController : MonoBehaviour
{

    [System.Serializable]
    public struct EnemyAttack {
        public float weight;
        public AttackType atkType;
        public float numberOfRedirections;
        public float redirectionCoolDown;
        public float bulletSpeed;
        public Vector2 bulletInitialPos;
        public float coolDown;
        public GameObject bulletPrefab;
        // precisa ?
        // public Animator attackAnimation;
    }

    public List<EnemyAttack> listOfAttacks = new List<EnemyAttack>();
    float totalWeight;
    GameObject player;
    public EnemyState currState = EnemyState.Wander;
    public EnemyType enemyType;
    public float range;
    public float speed;
    public float attackRange;
    // public float bulletSpeed;
    // public Vector2 bulletInitialPos;
    private bool chooseDir = false;
    private bool coolDownAttack = false;
    // public float coolDown;
    private Vector3 randomDir;
    // public GameObject bulletPrefab;

    public Animator animator;
    public Vector2 direction;
    public bool animSpeed, isMoving, isAttacking;

   void Awake() {
        totalWeight = 0;
        foreach(var _enemyAttack in listOfAttacks) {
            totalWeight += _enemyAttack.weight;
        }
    }

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

        updateAnim();
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
        isMoving = true;
        isAttacking = false;

        if (!chooseDir) {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;

        if (isPlayerInRange(range)) {
            currState = EnemyState.Follow;
        }
    }

    void Follow() {
        isMoving = true;
        isAttacking = false;
        direction = (player.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack() {
        isMoving = false;
        isAttacking = true;
        if (!coolDownAttack) {
            // GameController.DamagePlayer(10);
            // StartCoroutine(CoolDown());
            switch (enemyType) {
                case(EnemyType.Melee):
                    GameController.DamagePlayer(10);
                    StartCoroutine(CoolDown(3));
                break;
                case(EnemyType.Ranged):

                    
                break;
            }
            
        }
    }

    private IEnumerator CoolDown(float cd) {
        coolDownAttack = true;
        yield return new WaitForSeconds(cd);
        coolDownAttack = false;
    }

    private IEnumerator RedirectionCoolDown(float _coolDown, GameObject projectile) {
        yield return new WaitForSeconds(_coolDown);
        Vector3 dir = player.transform.position - projectile.transform.position;
        dir = player.transform.InverseTransformDirection(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        projectile.transform.eulerAngles = new Vector3(projectile.transform.eulerAngles.x, projectile.transform.eulerAngles.y, angle-90);
        projectile.GetComponent<BulletController>().GetPlayer(player.transform);
    }

    public void Death() {
        isMoving = false;
        isAttacking = false;
        Destroy(gameObject);
    }

    private void updateAnim(){
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
        // animator.SetBool("moving", isMoving);
        // animator.SetBool("attacking", isAttacking);
    }

    public void Shoot(){
        int atkIndex = pickAttack();
        EnemyAttack choosedAtk = listOfAttacks[atkIndex];

        GameObject bullet = Instantiate(choosedAtk.bulletPrefab, (Vector2)this.transform.position + choosedAtk.bulletInitialPos, Quaternion.identity) as GameObject;

        Vector3 dir = player.transform.position - bullet.transform.position;
        dir = player.transform.InverseTransformDirection(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y, angle-90);

        bullet.GetComponent<BulletController>().isEnemyBullet = true;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<BulletController>().GetPlayer(player.transform);

        // if (choosedAtk.atkType == AttackType.Guided) {
        //     for (int i = 0; i < choosedAtk.numberOfRedirections; i++) {
        //         StartCoroutine(RedirectionCoolDown(choosedAtk.redirectionCoolDown*(i+1), bullet));
        //     }
        // }
        switch (choosedAtk.atkType)
        {
            case(AttackType.Guided):
                for (int i = 0; i < choosedAtk.numberOfRedirections; i++) {
                    StartCoroutine(RedirectionCoolDown(choosedAtk.redirectionCoolDown*(i+1), bullet));
                }
            break;
        }

        StartCoroutine(CoolDown(choosedAtk.coolDown));
        isAttacking = false;
    }

    // public void GuidedShoot() {
    //     int atkIndex = pickAttack();
    //     Attack choosedAtk = listOfAttacks[atkIndex];
    //     GameObject projectile = Instantiate(choosedAtk.bulletPrefab, (Vector2) this.transform.position + choosedAtk.bulletInitialPos, Quaternion.identity) as GameObject;
    //     projectile.GetComponent<BulletController>().isEnemyBullet = true;
    //     projectile.AddComponent<RigidBody2D>().gravityScale = 0;
    //     projectile.GetComponent<BulletController>().GetPlayer(player.transform);

    //     for (int i = 0; i < choosedAtk.numberOfRedirections; i++) {
    //         StartCoroutine(RedirectionCoolDown(choosedAtk.redirectionCoolDown*(i+1), projectile));
    //     }

    //     StartCoroutine(CoolDown());
    //     isAttacking = false;
    // }

    // Sorteia um ataque para ser utilizado, retorna seu indice no vetor de ataques
    private int pickAttack() {
        float pick = Random.value*totalWeight;
        int choosenIndex = 0;
        float cumulativeWeight = listOfAttacks[0].weight;

        while (pick > cumulativeWeight && choosenIndex < listOfAttacks.Count -1 ) {
            choosenIndex++;
            cumulativeWeight += listOfAttacks[choosenIndex].weight;
        }

        return choosenIndex;
    }
}
