using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager instance;
    public AudioSource audioSource;
    [SerializeField] private AudioClip jumpAudio, hurtAudio, cherryAudio,shotAudio,explodeAudio;
    // Update is called once per frame

    private void Awake() {
        instance = this;
    }
    public void jumpAudioPlay(){
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }

    public void hurtAudioPlay(){
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }
    public void cherryAudioPlay(){
        audioSource.clip = cherryAudio;
        audioSource.Play();
    }
    public void shotAudioPlay(){
        audioSource.clip = shotAudio;
        audioSource.Play();
    }
    public void explodeAudioPlay(){
        audioSource.clip = explodeAudio;
        audioSource.Play();
    } 
}
