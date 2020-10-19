using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static int health = 100;
    private static int maxHealth = 100;
    // private static float moveSpeed = 5f;
    // private static float fireRate = 0.5f;

    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float moveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float fireRate { get => fireRate; set => fireRate = value; }

    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake() {

        if (instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Saude: " + health;
    }

    public static void DamagePlayer(int damage) {
        if (health > 0) health -= damage;
        if (health <= 0) {
            KillPlayer();
        }

    }

    public static void HealPlayer(int healAmount) {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    private static void KillPlayer() {

    }
}
