using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
    public string name;
    public string description;
    public Sprite itemImage;
}

public class CollectionController : MonoBehaviour
{

    public Item item;
    private bool catching = false;
    public float healthChange;
    public float moveSpeedChange;
    public float attackSpeedChange;
    public float bulletSizeChange;

    void Start() {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        // Destroy(GetComponent<CircleCollider2D>());
        // gameObject.AddComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        // Catching foi usado para evitar pegar o mesmo item duas vezes (tava com bug)
        if (collision.tag == "Player" && !catching) {
            catching = true;
            PlayerController.collectedAmount++;

            GameController.HealPlayer(healthChange);
            GameController.MoveSpeedChange(moveSpeedChange);
            GameController.FireRateChange(attackSpeedChange);
            GameController.BulletSizeChange(bulletSizeChange);

            Destroy(gameObject);
            catching = false;
        }
    }
}
