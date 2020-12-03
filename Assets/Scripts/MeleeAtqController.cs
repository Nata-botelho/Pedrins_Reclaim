using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtqController : MonoBehaviour
{
    public PlayerController player;
    public Vector2 direction;
    private int x, y;
    public Animator playerAnimator;
    public float attackTime, attackRange, defaultAttackTime;
    public LayerMask enemiesLayer;

    private void Start() {
        playerAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        getDirection();
        if(attackTime <= 0){
            if(Input.GetButton("Fire1")){
                playerAnimator.SetBool("isAttacking", true);
                RaycastHit2D hit = Physics2D.Raycast(player.transform.position, direction, attackRange, enemiesLayer);
                if(hit.collider){
                    hit.collider.GetComponent<EnemyController>().Death();
                }
                attackTime = defaultAttackTime;   
            }
        }
        else{
            attackTime -= Time.deltaTime;
            playerAnimator.SetBool("isAttacking", false);
        }
    }

    private void getDirection(){
        if(player.inputX != 0 || player.inputY != 0)
            direction = new Vector2(player.inputX, player.inputY);
    }
}
