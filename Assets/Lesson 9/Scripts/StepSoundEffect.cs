using System.Collections;
using System.Collections.Generic;
using lesson2;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StepSoundEffect : MonoBehaviour
{
    public PlayerMovement PlayerMovementComponent;
    public AudioClip StepSound;
    public float TimeStep = 1f;
    
    private AudioSource _audioSource;
    private float _timer;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = StepSound;
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovementComponent.IsMoving == false)
        {
            return;
        }
        
        if (_timer >= TimeStep)
        {
            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }
            _timer = 0f;
        }
        
        _timer += Time.deltaTime;
    }
}
