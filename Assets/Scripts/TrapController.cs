using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType {
    Rotational,
    Static
}

public class TrapController : MonoBehaviour
{
    public float rotationSpeed = 5;
    public TrapType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        switch (type)
        {
            case(TrapType.Rotational):
                this.transform.Rotate(new Vector3(0, 0, rotationSpeed));
            break;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            GameController.DamagePlayer(10);
        }
    }
}
