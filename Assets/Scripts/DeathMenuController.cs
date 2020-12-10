using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenyController : MonoBehaviour
{
    public void Restart() {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadGame();
    }

    public void BackToMenu() {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadStart();
    }

    public void Quit() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
