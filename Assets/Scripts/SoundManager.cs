using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource mainMusicSource;
    AudioSource takeFood;
    AudioSource gameState;
    [SerializeField] AudioClip mainMusic;
    [SerializeField] AudioClip putInBasket;
    [SerializeField] AudioClip boomSound;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip winSound;
    // Start is called before the first frame update

    private void Start()
    {
        mainMusicSource = gameObject.AddComponent<AudioSource>();
        takeFood = gameObject.AddComponent<AudioSource>();
        gameState = gameObject.AddComponent<AudioSource>();
        PlayAudioMain();
    }

    void PlayAudioMain()
    {
        mainMusicSource.clip = mainMusic;
        mainMusicSource.loop = true;
        mainMusicSource.Play();
    }

    public void GetFoodAudio()
    {
        takeFood.clip = putInBasket;
        takeFood.Play();
    }

    public void BoomSound()
    {
        takeFood.clip = boomSound;
        takeFood.Play();
    }

    public void GameOverSound()
    {
        gameState.clip = gameOverSound;
        gameState.Play();
    }
    public void WinSound()
    {
        gameState.clip = winSound;
        gameState.Play();
    }
}
