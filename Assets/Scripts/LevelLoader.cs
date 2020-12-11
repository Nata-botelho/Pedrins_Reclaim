using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// https://www.youtube.com/watch?v=CE9VOZivb3I&t=2s&ab_channel=Brackeys => Video ensinando a fazer transicao entre scenes
// Onde aprendi os códigos aqui seguintes

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    AudioManager audio = null;

    public void LoadNextLevel() {
        audio = FindObjectOfType<AudioManager>();
        audio.Pause();
        audio.Play("Playing");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadDeath() {
        StartCoroutine(LoadLevel(3));
    }

    public void LoadWin() {
        if (audio == null) {
            audio = FindObjectOfType<AudioManager>();
        }
        audio.Pause();
        audio.Play("Ending");
        audio.Volume(0.1f);
        StartCoroutine(LoadLevel(2));
    }

    public void LoadStart() {
        if (audio == null) {
            audio = FindObjectOfType<AudioManager>();
        }
        audio.Pause();
        audio.Play("Start");
        audio.Volume(0.7f);
        StartCoroutine(LoadLevel(0));
    }

    public void LoadGame() {
        StartCoroutine(LoadLevel(1));
    }

    // N funcionou por algum motivo
    public void LoadScene(string name) {
        StartCoroutine(LoadLevel(SceneManager.GetSceneByName(name).buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex) {

        // Play animation
        transition.SetTrigger("Start");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(levelIndex);
    }

    public void Quit() {
        Application.Quit();
    }
}
