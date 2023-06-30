using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    public AudioSource dotCollectionSound;
    [SerializeField]
    public AudioSource backGroundSound;
    [SerializeField]
    public AudioSource frightenModeSound;
    [SerializeField]
    public AudioSource respawnSound;
    [SerializeField]
    public AudioSource caughtSound;
    [SerializeField]
    public AudioSource butonSound;

    private void Awake()
    {
        instance = this;
    }

    public void DotCollectionSound(bool shouldPlay) { if (shouldPlay) { dotCollectionSound.Play(); } }
    public void BackGroundSound (bool shouldPlay){ if (shouldPlay){ backGroundSound.Play(); } }
    public void FrightenModeSound (bool shouldPlay) { if (shouldPlay) { frightenModeSound.Play(); } }
    public void RespawnSound (bool shouldPlay) { if (shouldPlay) { respawnSound.Play(); } }
    public void CaughtSound (bool shouldPlay) { if (shouldPlay) { caughtSound.Play(); } }
    public void ButtonSound(bool shouldPlay) { if (shouldPlay) { butonSound.Play(); } }

    public void DotCollectionVolume(float volume) { if (volume<=1 && volume >=0) { dotCollectionSound.volume = volume; } }
    public void BackGroundVolume(float volume) { if (volume<=1 && volume >=0) { backGroundSound.volume = volume; } }
    public void FrightenModeVolume(float volume) { if (volume<=1 && volume >=0) { frightenModeSound.volume = volume; } }
    public void RespawnVolume(float volume) { if (volume<=1 && volume >=0) { respawnSound.volume = volume; } }
    public void CaughtVolume(float volume) { if (volume<=1 && volume >=0) { caughtSound.volume = volume; } }
    public void ButtonVolume(float volume) { if (volume<=1 && volume >=0) { butonSound.volume = volume; } }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
