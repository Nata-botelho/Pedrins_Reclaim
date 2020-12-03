using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Animator animator;              //variavel que guarda o componente animator
    private Rigidbody2D p_rigidbody;        //variavel que guarda o rigidbody2d do pedrin

    public float speed;                     //velocidade de movimento
    public float inputX, inputY;
    private Vector3 movement;

    public Text collectedText;
    public static int collectedAmount = 0;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float lastFire;
    public float fireDelay;

    public bool isAttacking;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        p_rigidbody = this.GetComponent<Rigidbody2D>();
    }

    //fixed update eh chamado em um intervalo fixo que depende do frame rate da maquina, usado pra chamar funcoes que envolvem fisica
    void FixedUpdate()
    {
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;

        inputX = Input.GetAxisRaw("Horizontal");       //guarda inputs do eixo x ('a', 's', '<-' e '->')
        inputY = Input.GetAxisRaw("Vertical");         //guarda inputs do eixo y

        movement = new Vector3(inputX, inputY, 0);  //cria o vector3 movement correspondente aos inputs
        movement = movement.normalized * speed * Time.deltaTime;    //movement normalizado e ajustado pelo tempo e velocidade
        p_rigidbody.MovePosition(transform.position + movement);
        
        animator.SetFloat("InputX", inputX);
        animator.SetFloat("InputY", inputY);
        animator.SetFloat("Speed", inputX != 0 ? 1 : inputY != 0 ? 1 : 0);

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay) {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }

        collectedText.text = "Items Collected: " + collectedAmount;
    }

    public void Shoot(float horizontal, float vertical) {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (horizontal < 0) ? Mathf.Floor(horizontal) * bulletSpeed : Mathf.Ceil(horizontal) * bulletSpeed,
            (vertical < 0) ? Mathf.Floor(vertical) * bulletSpeed : Mathf.Ceil(vertical) * bulletSpeed,
            0
        );
    }
}