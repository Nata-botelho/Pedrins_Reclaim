using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Animator animator;              //variavel que guarda o componente animator
    private Rigidbody2D p_rigidbody;        //variavel que guarda o rigidbody2d do pedrin

    public float speed;                     //velocidade de movimento
    private float inputX;
    private float inputY;
    private Vector3 movement;

    public string collectedText;
    public static int collectedAmount = 0;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float lastFire;
    public float fireDelay;

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

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay) {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }

        collectedText = "Items Collected: " + collectedAmount;

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

    void Shoot(float horizontal, float vertical) {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (horizontal < 0) ? Mathf.Floor(horizontal) * bulletSpeed : Mathf.Ceil(horizontal) * bulletSpeed,
            (vertical < 0) ? Mathf.Floor(vertical) * bulletSpeed : Mathf.Ceil(vertical) * bulletSpeed,
            0
        );
    }
}