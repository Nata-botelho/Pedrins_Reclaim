using UnityEngine.Audio;
using System;
using UnityEngine;

// https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys Audio no jogo

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    public Sound playingSound;

    void Awake() {

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("Start");
    }

    void Update() {
        if (playingSound.source.isPlaying == false) {
            switch (playingSound.name)
            {
                case("Playing"):
                    Play("Playing2");
                break;
                case("Playing2"):
                    Play("Playing3");
                break;
                case("Playing3"):
                    Play("Playing");
                break;
            }
        } 
    }

    public void Play(string name) {
        playingSound = Array.Find(sounds, sound => sound.name == name);
        Debug.Log(playingSound.name);
        if (playingSound == null) {
            Debug.Log("Sound: " + name + " not found");
            return;
        }
        playingSound.source.Play(0);
    }

    public void Pause() {
        playingSound.source.Pause();
    }

    public void Volume(float v) {
        playingSound.source.volume = v;
    }
}
