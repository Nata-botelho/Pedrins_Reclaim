using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType {
    Rotational,
    Static,
}

public enum TrapMovement {
    Horizontal,
    Vertical,
    Static,
    Follows,
}


public class TrapController : MonoBehaviour
{
    public bool immunity = false;
    public float rotationSpeed = 2f;
    public TrapType type;
    public TrapMovement movement;
    public float speed = 2f;
    public float yAxis = -2.5f;
    public float xAxis =  100f;
    private bool chooseDir = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        switch (type)
        {
            case(TrapType.Rotational):
                this.transform.Rotate(new Vector3(0, 0, xAxis > 0 ? -speed:speed ));
            break;
            case(TrapType.Static):
            break;
        }
        switch(movement) {
            case(TrapMovement.Horizontal):
                HorizontalMove();
            break;
            case(TrapMovement.Vertical):

            break;
            case(TrapMovement.Static):

            break;
            case(TrapMovement.Follows):

            break;
        }
        
    }

    private IEnumerator HorizontalDirection() {
        // chooseDir = true;
        // yield return new WaitForSeconds(2f);
        // // Vector3 randomDir = new Vector3(0, 0, Random.Range(0, 360));
        // // Quaternion nextRotation = Quaternion.Euler(randomDir);
        // // transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, 0);
        // chooseDir = false;
        chooseDir = true;
        // StartCoroutine(HorizontalDirection());
        // transform.position += -transform.right * speed * Time.deltaTime;
        // Vector2 path = new Vector2(xAxis, yAxis);
        // transform.position = Vector2.MoveTowards(transform.position, path, speed*Time.deltaTime);
        yield return new WaitForSeconds(5.8f);
            
            
        // xAxis =  -xAxis;
        xAxis = -xAxis;
        chooseDir = false;
    }

    void HorizontalMove() {
        if (!chooseDir) {
            // Vector2 path = new Vector2(xAxis, yAxis);
            // transform.position = Vector2.MoveTowards(transform.position, path, speed*Time.deltaTime);
            // xAxis = -xAxis;
            StartCoroutine(HorizontalDirection());
            
        }
        Vector2 path = new Vector2(xAxis, yAxis);
        transform.position = Vector2.MoveTowards(transform.position, path, speed*Time.deltaTime);
        
        
    }

    private IEnumerator damagePlayer() {
        immunity = true;
        GameController.DamagePlayer(10);
        yield return new WaitForSeconds(3);
        immunity = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            // GameController.DamagePlayer(10);
            if (!immunity) {
                StartCoroutine(damagePlayer());
            }
        }
    }
}
