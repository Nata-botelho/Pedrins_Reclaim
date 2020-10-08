using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Animator animator;

    public float speed;
    private float inputX;
    private float inputY;
    private Vector3 movement;

    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        movement = new Vector3(inputX, inputY, 0);
        movement = movement.normalized * speed * Time.deltaTime;

        if(movement != Vector3.zero){
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", movement.x);
            animator.SetFloat("InputY", movement.y);
        }
        else{
            animator.SetBool("isWalking", false);
        }

        transform.position += movement; 
    }

}