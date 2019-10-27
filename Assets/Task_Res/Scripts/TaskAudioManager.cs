using UnityEngine;

public class TaskAudioManager : MonoBehaviour 
{
    public AudioSource bgAudioSource;
    public AudioSource btnAudioSource;
    public AudioClip normalBtnClip;

    private bool isClose = false;

    public void OnCloseBgMusicBtnClick()
    {
        isClose = !isClose;
        bgAudioSource.enabled = !isClose;
        btnAudioSource.enabled = !isClose;
    }

    public void OnBtnClick()
    {
        btnAudioSource.clip = normalBtnClip;
        btnAudioSource.Play();
    }
}
