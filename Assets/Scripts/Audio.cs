using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class Audio : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundSource;

    public Slider sliderMusic;
    public Slider sliderSound;

    public AudioClip[] audios;

    public AudioClip soundMove;
    public AudioClip soundWin;
    public AudioClip soundClick;

    private void Start()
    {
        var music = PlayerPrefs.GetFloat("music");
        var sound = PlayerPrefs.GetFloat("sound");

        if (music != 0)
            sliderMusic.value = music;
        if (sound != 0)
            sliderSound.value = sound;

    }
    public void SliderMusicChange()
    {
        musicSource.volume = sliderMusic.value;
        PlayerPrefs.SetFloat("music", sliderMusic.value);
    }

    public void SliderSoundChange()
    {
        soundSource.volume = sliderSound.value;
        PlayerPrefs.SetFloat("sound", sliderSound.value);
    }

    void FixedUpdate()
    {
        if (!musicSource.isPlaying)
        {
            var randAudioInd = UnityEngine.Random.Range(0, audios.Length);
            musicSource.clip = audios[randAudioInd];
            musicSource.Play();
        }
    }
}
