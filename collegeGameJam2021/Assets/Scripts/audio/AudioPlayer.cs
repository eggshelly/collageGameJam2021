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

        StaticDelegates.Audio += PlayAudio;
    }

    private void OnDestroy()
    {
        StaticDelegates.Audio -= PlayAudio;
    }

    void PlayAudio(bool isCollision)
    {
        if (isCollision)
        {
            playCollisionAudio();
        }
        else
        {
            playSpriteChangeAudio();
        }
    }

    public void playCollisionAudio()
    {
        if(onCollisionClip != null)
            audioSource.PlayOneShot(onCollisionClip);
    }
     public void playSpriteChangeAudio()
    {
        if (onSpriteSwitchClip != null)
            audioSource.PlayOneShot(onSpriteSwitchClip);
    }

    
}