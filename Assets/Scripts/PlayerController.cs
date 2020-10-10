using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator animator;              //variavel que guarda o componente animator
    private Rigidbody2D p_rigidbody;        //variavel que guarda o rigidbody2d do pedrin

    public float speed;                     //velocidade de movimento
    private float inputX;
    private float inputY;
    private Vector3 movement;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        p_rigidbody = this.GetComponent<Rigidbody2D>();
    }

    //fixed update eh chamado em um intervalo fixo que depende do frame rate da maquina, usado pra chamar funcoes que envolvem fisica
    void FixedUpdate()
    {
        inputX = Input.GetAxis("Horizontal");       //guarda inputs do eixo x ('a', 's', '<-' e '->')
        inputY = Input.GetAxis("Vertical");         //guarda inputs do eixo y

        movement = new Vector3(inputX, inputY, 0);  //cria o vector3 movement correspondente aos inputs
        movement = movement.normalized * speed * Time.deltaTime;    //movement normalizado e ajustado pelo tempo e velocidade

        //caso o vetor movement seja diferente de 0, o jogador esta se movimentando, variaiveis de animacao atualizadas
        if(movement != Vector3.zero){
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", movement.x);
            animator.SetFloat("InputY", movement.y);
        }
        else{
            animator.SetBool("isWalking", false);
        }

        transform.position += movement;             //posicao atualizada
        p_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;    //rotacao setada em 0 para evitar bugs nos colisores
    }

}