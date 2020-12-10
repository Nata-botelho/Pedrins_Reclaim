﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// https://www.youtube.com/watch?v=CE9VOZivb3I&t=2s&ab_channel=Brackeys => Video ensinando a fazer transicao entre scenes
// Onde aprendi os códigos aqui seguintes

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel() {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadDeath() {
        StartCoroutine(LoadLevel(3));
    }

    public void LoadWin() {
        StartCoroutine(LoadLevel(2));
    }

    IEnumerator LoadLevel(int levelIndex) {

        // Play animation
        transition.SetTrigger("Start");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(levelIndex);
    }
}
