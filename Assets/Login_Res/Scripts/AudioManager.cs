using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    public Sprite openAudio_Sprite;
    public Sprite closeAudio_Sprite;
    public AudioSource bgAudioSource;
    public AudioSource btnAudioSource;
    public AudioClip changeClothesClip;
    public AudioClip normalBtnClip;

    private Image audio_Image;
    private bool isClose = false;

    private void Start()
    {
        audio_Image = GameObject.Find("Btn_Audio").GetComponent<Image>();
    }

    public void OnCloseBgMusicBtnClick()
    {
        isClose = !isClose;
        if (isClose)
        {
            audio_Image.sprite = closeAudio_Sprite;
        }
        else
            audio_Image.sprite = openAudio_Sprite;
        bgAudioSource.enabled = !isClose;
        btnAudioSource.enabled = !isClose;
    }

    public void OnBtnClick()
    {
        btnAudioSource.clip = normalBtnClip;
        btnAudioSource.Play();
    }

    public void OnChangeClothesBtnClick()
    {
        btnAudioSource.clip = changeClothesClip;
        btnAudioSource.Play();
    }

}
