using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioData audioData;
    [SerializeField] private AudioSource audioSource;

    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
            }

            return instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    private AudioClip GetAudioClip(AudioType audioType)
    {
        foreach(Audio audio in audioData.audios)
        {
            if (audio.AudioType == audioType)
            {
                return audio.AudioClip;
            }
        }

        return null;
    }

    public void PlayAudio(AudioType audioType)
    {
        AudioClip audioClip = GetAudioClip(audioType);
        audioSource.PlayOneShot(audioClip);
    }
}
