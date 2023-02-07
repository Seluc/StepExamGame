using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DamageSoundEffect : MonoBehaviour
{
    public AudioClip DamageClip;

    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = true;
        _audioSource.clip = DamageClip; 
        _audioSource.Play();
        Destroy(gameObject, DamageClip.length + 0.1f);
    }
}
