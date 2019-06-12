using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AudioManager();
            }
            return _instance;
        }
    }

    private AudioSource audioSource;

    private void Awake()
    {
        _instance = this;
    }

    public AudioClip[] clips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
