using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionController : MonoBehaviour
{

    private void onTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerController.collectedAmount++;
            Destroy(gameObject);
        }
    }
}
