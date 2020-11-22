﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static float health = 100;
    private static int maxHealth = 100;
    private static float moveSpeed = 5f; // Erro dizendo que já existe isso no GameController
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;

    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }

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

    public static void HealPlayer(float healAmount) {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed) {
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate) {
        fireRate -= rate;
    }

    public static void BulletSizeChange(float size) {
        bulletSize += size;
    }

    private static void KillPlayer() {

    }
}