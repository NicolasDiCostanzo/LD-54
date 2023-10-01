using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] randomSounds;
    public Sound[] musics;
    public Sound[] playerSounds;
    public AudioSource musicSource, randomSoundSource, playerSource;
    public static AudioManager Instance;

    public int minRandomSoundDelay;
    public int maxVariation;
    private float timeSinceLastSound;

    private int currentSoundTime;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSound = 0f;
        currentSoundTime = 5;
    }

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else{
            Destroy(gameObject);
        }
    }

    public void PlaySound(string name){
        Sound player = Array.Find(playerSounds, p => p.name == name);
        if(player != null) {
            playerSource.clip = player.clip;
            playerSource.Play();
        }
    }

    private void PlayMusic(string name) {
        Sound music = Array.Find(musics, music => music.name == name);
        if(music != null) {
            musicSource.clip = music.clip;
            musicSource.Play();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Play random sound
        if(currentSoundTime <= timeSinceLastSound){
            Sound selectedSound = randomSounds[UnityEngine.Random.Range(0,randomSounds.Length)];
            randomSoundSource.clip = selectedSound.clip;
            randomSoundSource.Play();
            timeSinceLastSound = 0;
            currentSoundTime = minRandomSoundDelay + UnityEngine.Random.Range(0,maxVariation);
        }else {timeSinceLastSound += Time.deltaTime;}
    }

}
