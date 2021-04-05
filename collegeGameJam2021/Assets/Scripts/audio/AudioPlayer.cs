using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip onCollisionClip;
    [SerializeField] AudioClip onSpriteSwitchClip;
    private AudioSource audioSource;
    [SerializeField] ResultType result; 


    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void playCollisionAudio()
    {
        audioSource.PlayOneShot(onCollisionClip);
    }
     public void playSpriteChangeAudio()
    {
        audioSource.PlayOneShot(onSpriteSwitchClip);
    }
}