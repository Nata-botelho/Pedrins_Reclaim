﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
 
// https://answers.unity.com/questions/1479729/load-scene-after-video-ended.html Fazer algo quando o video acaba

public class VideoController: MonoBehaviour
{ 
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public string SceneName;

    void Start() 
    {
          VideoPlayer.loopPointReached += LoadScene;
    }

    // https://docs.unity3d.com/Manual/class-VideoPlayer.html Dar play no video quando quiser
    public void PlayIntro() {
        GameObject.Find("Canvas").active = false;
        VideoPlayer.Play();
    }

    void LoadScene(VideoPlayer vp)
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel();
    }


}