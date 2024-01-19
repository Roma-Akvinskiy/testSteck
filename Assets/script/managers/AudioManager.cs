using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singeltone
    private static AudioManager _instanse;

    public static AudioManager Instanse
    {
        get
        {
            return _instanse;
        }
    }
    private void Awake()
    {
        _instanse = this;
    }
    #endregion

    public AudioSource audioPlayer;

    public void OnPlayClip(AudioClip clip, float volume =1f, bool destroyed=false)
    {
        audioPlayer.PlayOneShot(clip, volume);
    }
}
