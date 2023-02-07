using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UiAudioMaster : MonoBehaviour
{
    public AudioClip Click;
    public AudioClip Exit;
    
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        PlayClip(Click);
    }

    public void ExitClick()
    {
        PlayClip(Exit);
    }

    private void PlayClip(AudioClip audioClip)
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        } 
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}
