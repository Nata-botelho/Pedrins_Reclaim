using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
 
// https://answers.unity.com/questions/1479729/load-scene-after-video-ended.html Fazer algo quando o video acaba

public class VideoController: MonoBehaviour
{ 
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public GameObject canvas;
    public bool isEnd;
    
    void Start() 
    {
          if (isEnd) {
            //   GameObject.Find("Canvas").active = false;
              VideoPlayer.loopPointReached += LoadWin;
          } else {
              VideoPlayer.loopPointReached += LoadScene;
          }
    }

    // https://docs.unity3d.com/Manual/class-VideoPlayer.html Dar play no video quando quiser
    public void PlayIntro() {
        VideoPlayer.Play();
    }

    void LoadScene(VideoPlayer vp)
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel();
    }

    void  LoadWin(VideoPlayer vp) {
        GameObject.Find("Outro").active = false;
        canvas.active = true;
    }

}
