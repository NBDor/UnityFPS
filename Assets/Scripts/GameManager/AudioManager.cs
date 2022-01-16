using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] backgroundMusic;
    public Sound[] sounds;
    public static AudioManager Instance;

    private void Awake()
    {
        initSoundArray(backgroundMusic, true, true);
        initSoundArray(sounds, false, false);

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void initSoundArray(Sound[] soundArr, bool loop, bool onAwake)
    {
        foreach (Sound s in soundArr)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.volume = s.volume;
            s.audioSource.clip = s.clip;
            s.clipName = s.clip.name;
            s.audioSource.loop = loop;
            s.audioSource.playOnAwake = onAwake;
        }
    }
    private void Start()
    {
        backgroundMusic[Random.Range(0, backgroundMusic.Length)].audioSource.Play();
    }

    public void Play(string clipName)
    {
        System.Array.Find(sounds, wantedSound => wantedSound.clipName == clipName).audioSource.Play();
    }
}

